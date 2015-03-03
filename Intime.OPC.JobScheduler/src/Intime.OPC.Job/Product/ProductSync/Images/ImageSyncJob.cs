using Common.Logging;
using Intime.OPC.Domain.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Job.Product.ProductSync.Images
{
    [DisallowConcurrentExecution]
    public class ImageSyncJob : IJob
    {
        private DateTime benchTime = DateTime.Now.AddMinutes(-5);
        private readonly ILog logger = LogManager.GetCurrentClassLogger();
        private void DoQuery(Action<IQueryable<Domain.Models.Product>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq = context.Products.Where(x => !x.IsHasImage && context.Resources.Any(t => t.UpdatedDate > benchTime && t.SourceId == x.Id && t.SourceType == 1)).AsQueryable();
                if (callback != null)
                    callback(linq);
            }
        }

        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap data = context.JobDetail.JobDataMap;
            var isRebuild = data.ContainsKey("isRebuild") && data.GetBoolean("isRebuild");
            var interval = data.ContainsKey("intervalOfSecs") ? data.GetInt("intervalOfSecs") : 5 * 60;
            benchTime = DateTime.Now.AddSeconds(-interval); 
            var totalCount = 0;

            if (!isRebuild)
                benchTime = data.GetDateTime("benchtime");

            DoQuery(skus =>
            {
                totalCount = skus.Count();
            });
            int cursor = 0;
            int size = 20;
            while (cursor < totalCount)
            {
                List<Domain.Models.Product> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.Id).Skip(cursor).Take(size).ToList());
                foreach (var sku in oneTimeList)
                {
                    try
                    {
                        Process(sku);
                    }
                    catch (Exception ex)
                    {
                       logger.Error(ex); 
                    }
                }
                cursor += size;
            }
        }

        private void Process(Domain.Models.Product sku)
        {
            using (var db = new YintaiHZhouContext()) {
                var p = db.Products.FirstOrDefault(t => t.Id == sku.Id);
                p.IsHasImage = true;
                p.UpdatedDate = DateTime.Now;
                p.UpdatedUser = SystemDefine.SystemUser;
                db.SaveChanges();
            }
        }

        #endregion
    }

    [DisallowConcurrentExecution]
    public class InventoryIs4SaleSyncJob : IJob    
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private DateTime benchTime = DateTime.Now.AddMinutes(-15);
        private void DoQuery(Action<IQueryable<Intime.OPC.Domain.Models.Product>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq = context.Products.Where(x => (!x.Is4Sale.HasValue || !x.Is4Sale.Value) && context.Inventories.Any(t => t.UpdateDate > benchTime && t.ProductId == x.Id )).AsQueryable();
                if (callback != null)
                    callback(linq);
            }
        }

        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap data = context.JobDetail.JobDataMap;
            var isRebuild = data.ContainsKey("isRebuild") ? data.GetBoolean("isRebuild") : false;
            var interval = data.ContainsKey("intervalOfMins") ? data.GetInt("intervalOfMins") : 15 * 60;
            var totalCount = 0;

            if (!isRebuild)
                benchTime = data.GetDateTime("benchtime");

            DoQuery(skus =>
            {
                totalCount = skus.Count();
            });
            int cursor = 0;
            int size = 20;
            while (cursor < totalCount)
            {
                List<Intime.OPC.Domain.Models.Product> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.Id).Skip(cursor).Take(size).ToList());
                foreach (var sku in oneTimeList)
                {
                    Process(sku);
                }
                cursor += size;
            }
        }

        private void Process(Domain.Models.Product sku)
        {
            using (var db = new YintaiHZhouContext())
            {
                var p = db.Products.FirstOrDefault(t => t.Id == sku.Id);
                p.Is4Sale = true;
                p.UpdatedDate = DateTime.Now;
                p.UpdatedUser = SystemDefine.SystemUser;
                db.SaveChanges();
                
            }
        }

        #endregion
    }
}
