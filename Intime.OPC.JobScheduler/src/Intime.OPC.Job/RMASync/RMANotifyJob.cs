using Common.Logging;
using Intime.O2O.ApiClient;
using Intime.O2O.ApiClient.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Job.RMASync
{
    [DisallowConcurrentExecution]
    public class RMANotifyJob : IJob
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        private void DoQuery(Action<IQueryable<OPC_RMA>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq =
                    context.OPC_RMA.Where(
                        t =>
                            !context.Orders.Any(x => x.OrderNo == t.OrderNo && x.OrderProductType.Value == 2) &&
                            t.Status == (int) EnumRMAStatus.ShipInStorage &&
                            !context.OPC_RMANotificationLog.Any(x => x.RMANo == t.RMANo && x.Status == (int)NotificationStatus.Create));

                if (callback != null)
                    callback(linq);
            }
        }

        private void DoQueryNotified(Action<IQueryable<OPC_RMA>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq =
                    context.OPC_RMA.Where(
                        t =>
                            !context.Orders.Any(x => x.OrderNo == t.OrderNo && x.OrderProductType.Value == 2) &&
                            t.Status == (int)EnumRMAStatus.NotifyProduct &&
                            !context.OPC_RMANotificationLog.Any(x => x.RMANo == t.RMANo && x.Status == (int)NotificationStatus.Paid));

                if (callback != null)
                    callback(linq);
            }
        }

        public void Execute(IJobExecutionContext context)
        {
            var totalCount = 0;
            DoQuery(skus =>
            {
                totalCount = skus.Count();
            });
            Logger.ErrorFormat("rma notification job, fetch rma order {0}",totalCount);

            int cursor = 0;
            int size = 20;
            while (cursor < totalCount)
            {
                List<OPC_RMA> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.Id).Skip(cursor).Take(size).ToList());
                foreach (var saleRMA in oneTimeList)
                {
                    try
                    {
                        NotifyCreate(saleRMA);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("failed to ntoify rma order {0}",saleRMA.RMANo);
                        Logger.Error(ex);
                    }
                }
                cursor += size;
            }

            //totalCount = 0;
            //cursor = 0;

            //DoQueryNotified(rmas =>
            //{
            //    totalCount = rmas.Count();
            //});

            //while (cursor < totalCount)
            //{
            //    List<OPC_RMA> oneTimeList = null;
            //    DoQueryNotified(r => oneTimeList = r.OrderBy(t => t.RMANo).Skip(cursor).Take(size).ToList());
            //    foreach (var saleRMA in oneTimeList)
            //    {
            //        try
            //        {
            //            NotifyPaid(saleRMA);
            //        }
            //        catch (Exception ex)
            //        {
            //            Logger.ErrorFormat("通知退货付款失败,退货单号{0}", saleRMA.RMANo);
            //            Logger.Error(ex);
            //        }
            //    }
            //    cursor += size;
            //}

        }

        public void NotifyCreate(OPC_RMA saleRMA)
        {
            var entity = new CreateRMANotificationEntity(saleRMA).CreateNotifiedEntity();
            var apiClient = new DefaultApiClient();
            var rsp = apiClient.Post(new OrderNotifyRequest
            {
                Data = entity
            });
            if (!rsp.Status)
            {
                Logger.Error(rsp.Data);
                Logger.Error(rsp.Message);
                return;
            }

            SaleRMANotified(saleRMA);
        }

        public void NotifyPaid(OPC_RMA saleRMA)
        {
            var apiClient = new DefaultApiClient();
            var rsp = apiClient.Post(new OrderNotifyRequest
            {
                Data = new PaidRMANotificationEntity(saleRMA).CreateNotifiedEntity()
            });
            if (!rsp.Status)
            {
                Logger.Error(rsp.Data);
                Logger.Error(rsp.Message);
                return;
            }
            SaleRMAPaidNotified(saleRMA);
        }

        private void SaleRMANotified(OPC_RMA saleRMA)
        {
            using (var db = new YintaiHZhouContext())
            {
                var opcRMA = db.OPC_RMA.FirstOrDefault(x => x.Id == saleRMA.Id);
                if (opcRMA == null)
                {
                    Logger.Error(string.Format("Invalid RMA ({0})", saleRMA.RMANo));
                    return;
                }

                if (saleRMA.Status != (int)EnumRMAStatus.ShipInStorage)
                {
                    Logger.ErrorFormat("invalid rma status {0}", saleRMA.Status);
                    return;
                }

                opcRMA.Status = (int)EnumRMAStatus.NotifyProduct;
                opcRMA.UpdatedDate = DateTime.Now;
                opcRMA.UpdatedUser = -10000;
                db.SaveChanges();

                db.OPC_RMANotificationLog.Add(new OPC_RMANotificationLog
                {
                    CreateDate = DateTime.Now,
                    CreateUser = -10000,
                    RMANo = saleRMA.RMANo,
                    Status = (int)NotificationStatus.Create,
                    Message = string.Empty,
                });
                db.SaveChanges();
            }
        }

        private void SaleRMAPaidNotified(OPC_RMA rma)
        {
            using (var db = new YintaiHZhouContext())
            {
                var opcRMA = db.OPC_RMA.FirstOrDefault(x => x.Id == rma.Id);
                if (opcRMA == null)
                {
                    Logger.Error(string.Format("Invalid RMA ({0})", rma.RMANo));
                    return;
                }

                if (rma.Status != (int)EnumRMAStatus.NotifyProduct)
                {
                    Logger.ErrorFormat("invalid rma status {0}", rma.Status);
                    return;
                }

                opcRMA.UpdatedDate = DateTime.Now;
                opcRMA.RMACashStatus = 5;//5表示已送收银
                opcRMA.UpdatedUser = -10000;
                db.SaveChanges();

                db.OPC_RMANotificationLog.Add(new OPC_RMANotificationLog
                {
                    CreateDate = DateTime.Now,
                    CreateUser = -10000,
                    RMANo = rma.RMANo,
                    Status = (int)NotificationStatus.Paid,
                    Message = string.Empty,
                });
                db.SaveChanges();
            }
        }
    }

    public class OrderNotificationException : Exception
    {
        public OrderNotificationException(string message)
            : base(message)
        {
        }
    }

    public enum NotificationStatus
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        Create = 1,


        /// <summary>
        /// 支付
        /// </summary>
        Paid = 3,

        ///<summary>
        /// Sync2Yintai
        /// </summary>
        Sync2Yintai = 7,
    }

    public enum NotificationType
    {
        /// <summary>
        /// 销售单
        /// </summary>
        Create = 0,

        /// <summary>
        /// 退货单
        /// </summary>
        RMA = 1,
    }

}
