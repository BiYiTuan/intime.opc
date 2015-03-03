using System.Web;
using Common.Logging;
using Intime.O2O.ApiClient.Yintai;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using Quartz.Impl.Matchers;

namespace Intime.OPC.Job.RMASync
{
    [DisallowConcurrentExecution]
    public class RMA2YintaiJob : IJob
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        //private DateTime _benchTime = DateTime.Now.AddDays(-20);
        public void Execute(IJobExecutionContext context)
        {
            //#if !DEBUG
            //            JobDataMap data = context.JobDetail.JobDataMap;
            //            var interval = data.ContainsKey("intervalofmins") ? data.GetInt("intervalofmins") : 60;
            //            _benchTime = DateTime.Now.AddMinutes(-interval);
            //#endif

            var totalCount = 0;
            DoQuery(skus =>
            {
                totalCount = skus.Count();
            });

            int cursor = 0;
            int size = 20;
            while (cursor < totalCount)
            {
                List<OPC_RMA> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.RMANo).Skip(cursor).Take(size).ToList());
                foreach (var saleRMA in oneTimeList)
                {
                    try
                    {
                        Notify2Yintai(saleRMA);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("推送退货信息失败,异常： {0}", ex);
                    }
                }
                cursor += size;
            }
        }

        private void Notify2Yintai(OPC_RMA rma)
        {
            Logger.ErrorFormat("Notify to yintai: {0}", rma.RMANo);
            using (var context = new YintaiHZhouContext())
            {
                var linq = from detail in context.OPC_RMADetail
                           from item in context.OrderItems
                           from stock in context.Inventories
                           where
                               detail.OrderItemId == item.Id && item.ProductId == stock.ProductId &&
                               item.ColorValueId == stock.PColorId && item.SizeValueId == stock.PSizeId && detail.RMANo == rma.RMANo
                           select new
                           {
                               ItemCode = stock.Id,
                               RMAQuantity = detail.BackCount,
                               rma.Reason
                           };


                dynamic data = new
                {
                    OPCSONumber = rma.OrderNo,
                    Status = rma.Status == (int)EnumRMAStatus.ShipVerifyNotPass ? 300 : 800,
                    RMAType = 1,
                    OperaterFrom = 5,
                    OPCRMATrancaction = linq,
                    CacelReason = rma.Status == (int)EnumRMAStatus.ShipVerifyNotPass ? "物流审核未通过" : "退货",
                    rma.Reason,
                };

                var parameter = JsonConvert.SerializeObject(data);
                var rmaInfos = new Dictionary<string, string>
                {
                    {"Data", parameter},
                };

                var client = new YintaiApiClient();
                var rsp = client.Post(rmaInfos, "Yintai.OpenApi.Vendor.AddRMAByOPC");
                if (rsp == null)
                {
                    Logger.ErrorFormat("调用银泰网接口返回NULL 退货单号： {0} 参数明细 {1}", rma.RMANo, parameter);
                    return;
                }
                if (rsp.IsSuccessful)
                {
                    context.OPC_RMANotificationLog.Add(new OPC_RMANotificationLog
                    {
                        CreateDate = DateTime.Now,
                        CreateUser = -10000,
                        RMANo = rma.RMANo,
                        Status = (int)NotificationStatus.Sync2Yintai,
                        Message = "Success"
                    });
                    context.SaveChanges();
                    Logger.ErrorFormat("通知银泰网RMA订单成功,RMANO {0}", rma.RMANo);
                }
                else
                {
                    Logger.ErrorFormat("推送退货单至RMA失败：RMANO:{0},银泰网返回错误:{1}", rma.RMAReason, rsp.Description);
                }
            }
        }

        private void DoQuery(Action<IQueryable<OPC_RMA>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var rmas =
                    context.OPC_RMA.Where(
                        t => context.Map4Order.Any(m => m.OrderNo == t.OrderNo && m.Channel == "yintai") &&
                            (!string.IsNullOrEmpty(t.RMACashNum) || t.Status == (int)EnumRMAStatus.ShipVerifyNotPass) &&
                            !context.OPC_RMANotificationLog.Any(x => x.RMANo == t.RMANo && x.Status == (int)NotificationStatus.Sync2Yintai));

                if (callback != null)
                    callback(rmas);
            }
        }
    }
}
