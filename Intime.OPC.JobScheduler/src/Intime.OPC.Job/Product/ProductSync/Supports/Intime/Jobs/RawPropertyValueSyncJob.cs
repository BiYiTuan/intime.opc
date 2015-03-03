using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using Intime.O2O.ApiClient;
using Intime.O2O.ApiClient.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;
using Newtonsoft.Json;
using Quartz;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Jobs
{
    [DisallowConcurrentExecution]
    public class RawPropertyValueSyncJob : IJob
    {
        private const int PageSize = 200;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly RemoteRepository _remoteRepository = new RemoteRepository(new DefaultApiClient());
        public void Execute(IJobExecutionContext context)
        {
#if !DEBUG
            JobDataMap data = context.JobDetail.JobDataMap;
            var interval = data.ContainsKey("intervalofhrs") ? data.GetInt("intervalofhrs") : 2;

            var benchTime = data.GetDateTime("benchdate");
            var lastUpdateDateTime = DateTime.Now.AddHours(-interval);
            this.Sync(benchTime, lastUpdateDateTime);
#else
            this.Sync(new DateTime(2014, 4, 22), DateTime.Now.AddDays(-3));
#endif
        }

        public void Sync(DateTime benchDate, DateTime lastUpdateDateTime)
        {
            var pageIndex = 1;

            while (true)
            {
                var properties =
                    _remoteRepository.GetPropertyValueRaws(pageIndex, PageSize, lastUpdateDateTime);

                if (properties == null || !properties.Any())
                {
                    Log.ErrorFormat("没有可同步的信息,pageIndex:{0},pageSize:{1},lastUpdateDatetime:{2}", pageIndex, PageSize, lastUpdateDateTime);
                    break;
                }
                foreach (var p in properties)
                {
                    try
                    {
                        this.Process(p);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.InnerException);
                        Log.Error(ex);
                    }
                }
                pageIndex += 1;
                Thread.Sleep(50);
            }
        }

        private void Process(PropertyValueRaw p)
        {
            using (var db = new YintaiHZhouContext())
            {
                var s = db.OPC_Stock.Where(x => x.SourceStockId == p.ID)
                    .Join(db.OPC_SKU, stock => stock.SkuId, sku => sku.Id, (stock, sku) => sku).FirstOrDefault();

                if (s == null)
                {
                    Log.InfoFormat("单品未同步 {0}", p.ID);
                    return;
                }

                var inventory =
                    db.Inventories.FirstOrDefault(
                        x => x.ProductId == s.ProductId && x.PColorId == s.ColorValueId && x.PSizeId == s.SizeValueId);

                if (inventory == null)
                {
                    Log.InfoFormat("销售属性未同步 {0}", p.ID);
                    return;
                }

                var propertyExt = db.OPC_StockPropertyValueRaw.FirstOrDefault(x => x.SourceStockId == p.ID && x.Channel == SystemDefine.IntimeChannel);
                if (propertyExt == null)
                {
                    db.OPC_StockPropertyValueRaw.Add(new OPC_StockPropertyValueRaw()
                    {
                        InventoryId = inventory.Id,
                        Channel = SystemDefine.IntimeChannel,
                        PropertyData = JsonConvert.SerializeObject(p),
                        SourceStockId = p.ID,
                        UpdateDate = p.LastUpdate
                    });
                    db.SaveChanges();
                }
                else if(propertyExt.UpdateDate < p.LastUpdate || propertyExt.InventoryId != inventory.Id)
                {
                    propertyExt.InventoryId = inventory.Id;
                    propertyExt.PropertyData = JsonConvert.SerializeObject(p);
                    propertyExt.UpdateDate = p.LastUpdate;
                    db.SaveChanges();
                }
                var product = db.Products.FirstOrDefault(x => x.Id == inventory.ProductId);
                if (product != null)
                {
                    //商品属性更新后更新商品时间，以重新索引商品
                    product.UpdatedDate = DateTime.Now;
                    product.UpdatedUser = SystemDefine.SystemUser;
                    db.SaveChanges();
                }
            }
        }
    }
}
