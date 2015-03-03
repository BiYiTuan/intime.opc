using Common.Logging;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Order.DTO;
using Intime.OPC.Job.Order.Repository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Job.Order.OrderStatusSync
{

    [DisallowConcurrentExecution]
    public class SaleOrderStatusSyncJob : IJob
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private static DateTime _benchTime = new DateTime(2014, 12, 23);
        private readonly IOrderRemoteRepository _remoteRepository = new OrderRemoteRepository();

        private void DoQuery(Action<IQueryable<OPC_Sale>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq =
                    context.OPC_Sale.Where(
                        t =>
                            t.UpdatedDate > _benchTime &&
                            ((t.Status == (int)EnumSaleOrderStatus.NotifyProduct && context.Orders.Any(x => x.OrderNo == t.OrderNo && x.OrderProductType == 2)) || //自拍商品订单
                             ((t.Status > 0 && t.Status < (int)EnumSaleOrderStatus.ShipInStorage &&     //系统商品订单
                               !context.Orders.Any(x => x.OrderNo == t.OrderNo && x.OrderProductType == 2)))));

                if (callback != null)
                    callback(linq);
            }
        }

        #region IJob 成员

        public void Execute(IJobExecutionContext context)
        {
            var totalCount = 0;
#if !DEBUG
            JobDataMap data = context.JobDetail.JobDataMap;
            var isRebuild = data.ContainsKey("isRebuild") && data.GetBoolean("isRebuild");
            var interval = data.ContainsKey("intervalOfDays") ? data.GetInt("intervalOfDays") : 5;
             _benchTime = DateTime.Now.AddDays(-interval);
             if (isRebuild)
                 _benchTime = _benchTime.AddMonths(-2);
#endif
            DoQuery(saleOrders =>
            {
                totalCount = saleOrders.Count();
            });
            int cursor = 0;
            int size = 20;
            while (cursor < totalCount)
            {
                List<OPC_Sale> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.OrderNo).Skip(cursor).Take(size).ToList());
                foreach (var opc_sale in oneTimeList)
                {
                    Process(opc_sale);          // 同步状态到单品系统
                }
                cursor += size;
            }
        }
        private void Process(OPC_Sale opc_Sale)
        {
            OrderStatusResultDto saleStatus = null;
            try
            {
                saleStatus = _remoteRepository.GetOrderStatusById(opc_Sale);
                ProcessSaleOrderStatus(opc_Sale, saleStatus);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void ProcessSaleOrderStatus(OPC_Sale saleOrder, OrderStatusResultDto saleStatus)
        {
            if (saleStatus == null)
            {
                Log.Error("Sale Order has no return info!");
                return;
            }
            var processor = SaleOrderStatusProcessorFactory.Create(int.Parse(saleStatus.Status));
            processor.Process(saleOrder.SaleOrderNo, saleStatus);
        }

        #endregion
    }
}
