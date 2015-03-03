using System;
using Common.Logging;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Synchronizers;
using Quartz;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Jobs
{
    /// <summary>
    /// 同步所有信息Job
    /// </summary>
    [DisallowConcurrentExecution]
    public class AllSyncJob : IJob
    {
        private readonly AllSynchronizer _allSynchronizer;
        //private readonly ProductPicSynchronizer _productPicSynchronizer;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public AllSyncJob()
        {
            _allSynchronizer = new AllSynchronizer(new RemoteRepository());
        }

        public void Execute(IJobExecutionContext context)
        {
            Log.InfoFormat("开始同步{0}",DateTime.Now);
            JobDataMap data = context.JobDetail.JobDataMap;
            var interval = data.ContainsKey("intervalOfMins") ? data.GetInt("intervalOfMins") : 15;
            var benchTime = DateTime.Now.AddMinutes(-interval);
            Log.InfoFormat("Product sync: Bench datetime is {0}",benchTime);
            _allSynchronizer.Sync(benchTime);

            Log.InfoFormat("完成同步{0}",DateTime.Now);
        }
    }
}
