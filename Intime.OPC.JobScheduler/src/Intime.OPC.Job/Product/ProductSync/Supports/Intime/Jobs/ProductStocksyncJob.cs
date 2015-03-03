using System.Threading;
using System.Web.UI;
using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Mapper;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO;
using Quartz;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Jobs
{
    public class ProductStockSyncJob:IJob
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IRemoteRepository _remoteRepository;
        private IChannelMapper _channelMapper;
        private IProductPropertySyncHandler _productPropertySyncProcessor;
        private IInventorySyncProcessor _inventorySyncProcessor;
        public ProductStockSyncJob ()
        {
            _remoteRepository = new RemoteRepository();
            _channelMapper = new ChannelMapper();
            _productPropertySyncProcessor = new ProductPropertySyncHandler(_channelMapper);
            _inventorySyncProcessor = new InventorySyncProcessor();
        }

        public void Execute(IJobExecutionContext context)
        {
            Log.InfoFormat("开始同步{0}", DateTime.Now);
            JobDataMap data = context.JobDetail.JobDataMap;
            var interval = data.ContainsKey("intervalOfMins") ? data.GetInt("intervalOfMins") : 15;
            var pageSize = data.ContainsKey("pageSize") ? data.GetInt("pageSize") : 200;
            pageSize = Math.Min(pageSize, 200);
            var benchTime = DateTime.Now.AddMinutes(-interval);
            var pageIndex = 1;
            Log.InfoFormat("Stock sync: Bench datetime is {0}", benchTime);
            int succeedCount = 0;

            while (true)
            {
                var products = _remoteRepository.GetProductList(pageIndex, pageSize, benchTime).ToList();
                if (products.Count == 0)
                {
                    Log.ErrorFormat("没有可同步的信息,pageIndex:{0},pageSize:{1},lastUpdateDatetime:{2}", pageIndex, pageSize, benchTime);
                    break;
                }

                TaskScheduler.UnobservedTaskException += (sender, args) => { Log.Error(args.Exception); args.SetObserved(); };

                Task<int>[] tasks = products.Select((p) => Task.Factory.StartNew(() => Sync(p), TaskCreationOptions.LongRunning)).ToArray();

                Task.WaitAll(tasks);
                succeedCount += tasks.Count(x => x.Result == 1);
                pageIndex += 1;

                Thread.Sleep(1000);
            }

            Log.InfoFormat("完成同步{0},共同步sku{1}", DateTime.Now,succeedCount);
        }

        private int Sync(ProductDto product)
        {
            using (var db = new YintaiHZhouContext())
            {
                var brandMapExt = _channelMapper.GetMapByChannelValue(product.BrandId, ChannelMapType.Brand);
                if (brandMapExt == null)
                {
                    return 0;
                }
                var brand = db.Brands.FirstOrDefault(b => b.Id == brandMapExt.LocalId);
                var mapKey = string.Format("{1}-{0}", product.ProductCode, brand.Id);
                var productCodeMap = _channelMapper.GetMapByChannelValue(mapKey, ChannelMapType.ProductCode);
                if (productCodeMap == null)
                {
                    return 0;
                }
                var p = db.Products.FirstOrDefault(x => x.Id == productCodeMap.LocalId);
                if (p == null)
                {
                    return 0;
                }

                var color = _productPropertySyncProcessor.SyncColor(p.Id,product);
                if (color == null)
                {
                    return 0;
                }

                var size = _productPropertySyncProcessor.SyncSize(p.Id, product);

                if (size == null)
                {
                    return 0;
                }

                var sku =
                    db.OPC_SKU.FirstOrDefault(
                        x => x.ColorValueId == color.Id && x.SizeValueId == size.Id && x.ProductId == p.Id);

                if (sku == null)
                {
                    return 0;
                }
                var stock =
                    db.OPC_Stock.FirstOrDefault(
                        x => x.SkuId == sku.Id && x.SourceStockId == product.ProductId && x.StoreCode == product.StoreNo);

                if (stock == null)
                {
                    return 0;
                }

                stock.Count = Convert.ToInt32(decimal.Floor(product.Stock ?? 0));
                stock.UpdatedDate = DateTime.Now;
                stock.UpdatedUser = SystemDefine.SystemUser;
                db.Entry(stock).State = EntityState.Modified;
                p.Is4Sale = true;
                p.Price = product.CurrentPrice;
                p.UnitPrice = product.LabelPrice;
                p.UpdatedDate = DateTime.Now;
                p.UpdatedUser = SystemDefine.SystemUser;
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                var inventory = _inventorySyncProcessor.Sync(sku);
                if (inventory == null)
                {
                    return 0;
                }
                return 1;
            }
        }
    }
}
