using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;
using System;
using System.Linq;
using System.Threading;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Synchronizers
{
    /// <summary>
    /// 商品同步
    /// </summary>
    public class ProductPicSynchronizer
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IRemoteRepository _remoteRepository;
        //private readonly string _lastUpdateDateTimeKey = string.Empty;
        //private readonly IUpdateDateStore _updateDateStore;
        private const int PageSize = 30;
        private readonly IProductPicProcessor _productPicProcessor;

        public ProductPicSynchronizer(IRemoteRepository remoteRepository, IProductPicProcessor productPicProcessor)
        {
            _remoteRepository = remoteRepository;
            //_updateDateStore = updateDateStore;
            _productPicProcessor = productPicProcessor;
            //_lastUpdateDateTimeKey = GetType().FullName;
        }

        public void Sync(DateTime benchTime)
        {
            var pageIndex = 1;
            var lastUpdateDateTime = benchTime;//_updateDateStore.GetLast(_lastUpdateDateTimeKey);

            while (true)
            {
                var products = _remoteRepository.GetProudctImages(pageIndex, PageSize, lastUpdateDateTime).ToList();
                Log.InfoFormat("Fetch images {0}, pageIndex {1}, lastUpdateDatetime {2}", products.Count, pageIndex, lastUpdateDateTime);
                if (products.Count == 0)
                {
                    Log.ErrorFormat("没有可同步的信息,pageIndex:{0},pageSize:{1},lastUpdateDatetime:{2}", pageIndex, PageSize, lastUpdateDateTime);
                    break;
                }

                TaskScheduler.UnobservedTaskException += (sender, args) => { Log.Error(args.Exception); args.SetObserved(); };

                Task<Resource>[] tasks = products.Select((p) => Task.Factory.StartNew(() => _productPicProcessor.Sync(p.ProductId, p.ColorId, p.Url, p.Id,
                        p.SeqNo, p.WriteTime), TaskCreationOptions.LongRunning)).ToArray();

                Task.WaitAll(tasks);

                // 进行下一页
                pageIndex += 1;
            }

        }

    }
}
