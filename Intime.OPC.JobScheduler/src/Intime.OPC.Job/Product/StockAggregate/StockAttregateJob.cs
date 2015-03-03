using Common.Logging;
using Intime.OPC.Domain.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Job.Product.StockAggregate
{
    [DisallowConcurrentExecution]
    public class StockAttregateJob : IJob
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private DateTime _benchTime = DateTime.Now.AddMinutes(-300);
        private void DoQuery(Action<IQueryable<OPC_SKU>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq = context.OPC_SKU.Where(x=>context.OPC_Stock.Any(t=>t.UpdatedDate > _benchTime && t.SkuId == x.Id)).AsQueryable(); 
                if (callback != null)
                    callback(linq);
            }
        }

        private static List<OPC_Stock> QueryStocks(int skuId) {
            using (var db = new YintaiHZhouContext()) {
                return db.OPC_Stock.Where(x => x.SkuId == skuId).ToList();
            }
        }

        private static void Process(OPC_SKU sku) {
            var list = new List<IStockAttregateProcessor>();
            list.Add(new Set4SaleProcessor());
            list.Add(new AggregateToInveontoryProcessor());
            var handler = new DefaultStockHandler(list);
            IStockAggregator aggregator = new DefaultStockAggregator();
            
            handler.Process(aggregator.Aggregate(QueryStocks(sku.Id)));
            
        }

        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            var totalCount = 0;
#if !DEBUG
            JobDataMap data = context.JobDetail.JobDataMap;
            var isRebuild = data.ContainsKey("isRebuild") ? data.GetBoolean("isRebuild") : false;
            var interval = data.ContainsKey("intervalOfMins") ? data.GetInt("intervalOfMins") : 5;
            _benchTime = DateTime.Now.AddMinutes(-5);
            if (isRebuild)
                _benchTime = _benchTime.AddMonths(-2);
#endif
            DoQuery(skus =>
            {
                totalCount = skus.Count();
            });
            int cursor = 0;
            int size = 20;
            while (cursor < totalCount)
            {
                List<OPC_SKU> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.Id).Skip(cursor).Take(size).ToList());
                foreach (var sku in oneTimeList) 
                {
                    Process(sku);
                }
                cursor += size;
            }
        }

        #endregion
    }
}
