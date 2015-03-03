using System;
using Common.Logging;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Mapper;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Synchronizers;
using Quartz;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Jobs
{
     [DisallowConcurrentExecution]
    public class ProductPicSyncJob : IJob
    {
        private readonly ProductPicSynchronizer _productPicSynchronizer;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public ProductPicSyncJob()
        {
            _productPicSynchronizer = new ProductPicSynchronizer(new RemoteRepository(), new ProductPicProcessor(new ChannelMapper()));
        }

        public void Execute(IJobExecutionContext context)
        {
            Log.Info("开始同步");
            JobDataMap data = context.JobDetail.JobDataMap;
            var interval = data.ContainsKey("intervalOfMins") ? data.GetInt("intervalOfMins") : 30;
            var benchTime = DateTime.Now.AddMinutes(-interval);
            _productPicSynchronizer.Sync(benchTime);

            Log.Info("完成同步");
        }
    }
}
