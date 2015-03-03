using Common.Logging;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Order.DTO;
using Intime.OPC.Job.Order.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Job.RMASync
{
    [DisallowConcurrentExecution]
    public class RMAStatusSyncJob : IJob
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly IOrderRemoteRepository _remoteRepository = new OrderRemoteRepository();

        private void DoQuery(Action<IQueryable<OPC_RMA>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq =
                    context.OPC_RMA.Where(
                        t =>
                            t.Status == (int) EnumRMAStatus.NotifyProduct ||
                            t.Status == (int) EnumRMAStatus.PrintRMA &&
                            context.OPC_RMANotificationLog.Any(
                                r => r.RMANo == t.RMANo && r.Status == (int) NotificationStatus.Create) &&
                            !context.OPC_RMANotificationLog.Any(
                                x => x.RMANo == t.RMANo && x.Status == (int) NotificationStatus.Paid));
                if (callback != null)
                    callback(linq);
            }
        }

        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            var totalCount = 0;
            DoQuery(saleRMA =>
            {
                totalCount = saleRMA.Count();
            });
            int cursor = 0;
            const int size = 20;
            while (cursor < totalCount)
            {
                List<OPC_RMA> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.RMANo).Skip(cursor).Take(size).ToList());
                foreach (var opc_saleRMA in oneTimeList)
                {
                    Process(opc_saleRMA);
                }
                cursor += size;
            }
        }
        private void Process(OPC_RMA opc_SaleRMA)
        {
            try
            {
                OrderStatusResultDto saleStatus = _remoteRepository.GetRMAStatusById(opc_SaleRMA);
                if (saleStatus != null)
                {
                    ProcessSaleRMAStatus(opc_SaleRMA, saleStatus);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void ProcessSaleRMAStatus(OPC_RMA saleRMA, OrderStatusResultDto saleStatus)
        {
            var processor = RMAStatusProcessorFactory.Create(int.Parse(saleStatus.Status));
            processor.Process(saleRMA.RMANo, saleStatus);
        }

        #endregion
    }
}
