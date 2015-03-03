using System.Collections.Generic;

using Common.Logging;
using Intime.OPC.Job.Trade.SplitOrder.Supports.Strategys;
using Quartz;

namespace Intime.OPC.Job.Trade.SplitOrder
{
    /// <summary>
    /// 拆单Job
    /// </summary>
    [DisallowConcurrentExecution]
    public class SplitOrderJob : IJob
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public void Execute(IJobExecutionContext context)
        {
            Log.Info("开始分批拆单");

            var processor = new SplitOrderProcessor(new List<ISplitStrategy>()
            {
                new DefaultSplitStrategy(),
                new StockShortageSplitStrategy()
            });

            processor.Process();

            Log.Info("完成分批拆单");
        }
    }
}

