using System.ComponentModel;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Transactions;
using Common.Logging;
using Intime.O2O.ApiClient;
using Intime.O2O.ApiClient.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    [DisallowConcurrentExecution]
    public class OrderNotifyJob : IJob
    {
        private const int JobUserId = -10000;
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        private void DoQuery(Action<IQueryable<OPC_Sale>> callback, int orderStatus, SaleOrderNotificationStatus status)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq =
                    context.OPC_Sale.Where(
                        t => t.Status == orderStatus &&
                             !context.OPC_SaleOrderNotificationLog.Any(
                                 x =>
                                     x.SaleOrderNo == t.SaleOrderNo && (
                                     x.Status == (int)status ||
                                     x.Status == (int)SaleOrderNotificationStatus.CreateFailed ||
                                     x.Status == (int)SaleOrderNotificationStatus.PaidFailed ||
                                     x.Status == (int)SaleOrderNotificationStatus.ExceptionThrow))
                                     );

                if (callback != null)
                    callback(linq);
            }
        }

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap data = context.JobDetail.JobDataMap;
            var cashingBeginTime = data.ContainsKey("cashingBeginTime")
                ? data.GetTimeSpanValue("cashingBeginTime")
                : TimeSpan.MinValue;

            var cashingEndTime = data.ContainsKey("cashingEndTime") ?
                data.GetTimeSpanValue("cashingEndTime") : TimeSpan.MaxValue;

            var enableCashingTimeRange = data.ContainsKey("enableCashingTimeRange") &&
                                         data.GetBooleanValue("enableCashingTimeRange");

            var totalCount = 0;
            DoQuery(skus =>
            {
                totalCount = skus.Count();
            }, 0, SaleOrderNotificationStatus.Create);

            int cursor = 0;
            int size = 20;
            while (cursor < totalCount)
            {
                List<OPC_Sale> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.OrderNo).Skip(cursor).Take(size).ToList(), 0,
                    SaleOrderNotificationStatus.Create);
                foreach (var saleOrder in oneTimeList)
                {
                    NotifyCreate(saleOrder);
                }
                cursor += size;
            }

            if (enableCashingTimeRange && !IsInCashingTimeRange(cashingBeginTime, cashingEndTime))
            {
                return;
            }

            totalCount = 0;
            cursor = 0;

            DoQuery(orders =>
            {
                totalCount = orders.Count();
            }, 1, SaleOrderNotificationStatus.Paid);

            while (cursor < totalCount)
            {
                List<OPC_Sale> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.OrderNo).Skip(cursor).Take(size).ToList(), 1, SaleOrderNotificationStatus.Paid);
                foreach (var saleOrder in oneTimeList)
                {
                    try
                    {
                        NotifyPaid(saleOrder, enableCashingTimeRange, cashingBeginTime, cashingEndTime);
                    }
                    catch (OrderNotificationException ex)
                    {
                        Logger.Error(ex);
                    }
                }
                cursor += size;
            }
        }


        private bool IsInCashingTimeRange(TimeSpan begin, TimeSpan end)
        {
            return DateTime.Now.TimeOfDay > begin && DateTime.Now.TimeOfDay < end;
        }

        public void NotifyCreate(OPC_Sale saleOrder)
        {
            var entity = new CreateOrderNotificationEntity(saleOrder).CreateNotifiedEntity();
            var apiClient = new DefaultApiClient();
            try
            {
                var rsp = apiClient.Post(new OrderNotifyRequest()
                {
                    Data = entity
                }, true);
                if (rsp == null)
                {
                    Logger.Error("通知订单失败，信息部返回NULL");
                    NotifyFailed(saleOrder, SaleOrderNotificationStatus.CreateFailed, apiClient.ErrorList());
                    return;
                }
                if (!rsp.Status)
                {
                    Logger.Error(rsp.Data);
                    Logger.Error(rsp.Message);
                    var errors = new List<string> { rsp.Data, rsp.Message };
                    errors.AddRange(apiClient.ErrorList().Where(x => !string.IsNullOrEmpty(x)));
                    NotifyFailed(saleOrder, SaleOrderNotificationStatus.CreateFailed, errors);
                    return;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                NotifyFailed(saleOrder, SaleOrderNotificationStatus.ExceptionThrow, new[] { e.Message });
                return;
            }

            SaleOrderNotified(saleOrder, SaleOrderNotificationStatus.Create);
        }

        private void NotifyFailed(OPC_Sale saleOrder, SaleOrderNotificationStatus create, IEnumerable<string> errorList)
        {
            using (var db = new YintaiHZhouContext())
            {
                using (var scope = new TransactionScope())
                {
                    db.OPC_SaleOrderNotificationLog.Add(new OPC_SaleOrderNotificationLog
                    {
                        CreateDate = DateTime.Now,
                        CreateUser = JobUserId,
                        SaleOrderNo = saleOrder.SaleOrderNo,
                        Status = (int)create,
                        Message = string.Join(";", errorList)
                    });

                    var trade = db.OPC_Sale.FirstOrDefault(x => x.SaleOrderNo == saleOrder.SaleOrderNo);
                    if (trade != null)
                    {
                        trade.CashStatus = (int)EnumCashStatus.CashingFailed;
                        trade.CashDate = DateTime.Now;
                        trade.UpdatedDate = DateTime.Now;
                        trade.UpdatedUser = JobUserId;
                        db.Entry(trade).State = EntityState.Modified;
                    }
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }

        public void NotifyPaid(OPC_Sale saleOrder, bool enbaleCashingTimeRange, TimeSpan bein, TimeSpan end)
        {
            if (enbaleCashingTimeRange && !IsInCashingTimeRange(bein, end)) return;
            Logger.InfoFormat("Notify paid to it sale order no:{0}", saleOrder.SaleOrderNo);
            var apiClient = new DefaultApiClient();
            var rsp = apiClient.Post(new OrderNotifyRequest()
            {
                Data = new PaidOrderNotificationEntity(saleOrder).CreateNotifiedEntity()
            }, true);
            if (rsp == null)
            {
                NotifyFailed(saleOrder, SaleOrderNotificationStatus.PaidFailed, apiClient.ErrorList());
                return;
            }
            if (!rsp.Status)
            {
                Logger.Error(rsp.Data);
                Logger.Error(rsp.Message);
                var errors = new List<string> { rsp.Data, rsp.Message };
                errors.AddRange(apiClient.ErrorList().Where(x => !string.IsNullOrEmpty(x)));
                NotifyFailed(saleOrder, SaleOrderNotificationStatus.PaidFailed, errors);
                return;
            }
            SaleOrderNotified(saleOrder, SaleOrderNotificationStatus.Paid);
        }

        private void SaleOrderNotified(OPC_Sale saleOrder, SaleOrderNotificationStatus status)
        {
            using (var scope = new TransactionScope())
            {
                using (var db = new YintaiHZhouContext())
                {
                    var order = db.OPC_Sale.FirstOrDefault(x => x.Id == saleOrder.Id);
                    if (order == null)
                    {
                        Logger.Error(string.Format("Invalid order ({0})", saleOrder.OrderNo));
                        return;
                    }

                    if (order.Status != (int)EnumSaleOrderStatus.NotifyProduct && order.Status != (int)EnumSaleOrderStatus.None)
                    {
                        Logger.Error(string.Format("Invalid order status ({0})", saleOrder.OrderNo));
                        return;
                    }

                    if (order.Status == (int)EnumSaleOrderStatus.None)
                    {
                        order.Status = (int)EnumSaleOrderStatus.NotifyProduct;
                        order.UpdatedDate = DateTime.Now;
                        order.UpdatedUser = JobUserId;
                        db.Entry(order).State = EntityState.Modified;
                    }

                    db.OPC_SaleOrderNotificationLog.Add(new OPC_SaleOrderNotificationLog
                    {
                        CreateDate = DateTime.Now,
                        CreateUser = JobUserId,
                        SaleOrderNo = saleOrder.SaleOrderNo,
                        Status = (int)status,
                        Message = string.Empty,
                    });
                    db.SaveChanges();
                    scope.Complete();
                }
            }
        }
    }
}
