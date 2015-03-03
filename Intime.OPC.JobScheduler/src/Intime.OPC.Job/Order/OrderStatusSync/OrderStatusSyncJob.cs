using System.Text;
using System.Transactions;
using Common.Logging;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    /// <summary>
    /// 根据销售单状态反向写入订单状态
    /// </summary>

    [DisallowConcurrentExecution]
    public class OrderStatusSyncJob : IJob
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private DateTime _benchTime = DateTime.Now.AddDays(-1);
        
        public void DoQuery(Action<IQueryable<Domain.Models.Order>> callback)
        {
            using (var context = new YintaiHZhouContext())
            {
                var linq =
                    context.Orders.Where(
                        o => o.Status != 0 && o.Status != -10 && o.Status != 10 && o.Status != 19 && o.Status != 18 && o.CreateDate >= _benchTime);
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
            var interval = data.ContainsKey("intervalOfMins") ? data.GetInt("intervalOfMins") : 5 * 60;
             _benchTime = DateTime.Now.AddMinutes(-interval);

            if (isRebuild)
                _benchTime = _benchTime.AddMonths(-3);
#endif
            DoQuery(skus =>
            {
                totalCount = skus.Count();
            });
            int cursor = 0;
            int size = 20;
            while (cursor < totalCount)
            {
                List<Domain.Models.Order> oneTimeList = null;
                DoQuery(r => oneTimeList = r.OrderBy(t => t.OrderNo).Skip(cursor).Take(size).ToList());
                foreach (var order in oneTimeList)
                {
                    SetOrderStatus(order);
                }
                cursor += size;
            }
        }

        /// <summary>
        /// 根据销售单状态反向写入订单状态，由同一订单下所有发货单状态来决策出订单状态
        /// 订单如果发货了，则需要同步发货状态及发货包裹,插入发货日志
        /// 目前实现有很大的问题 wxh备注 2014-04-20 0:17:43
        /// </summary>
        /// <param name="opcSale"></param>
        private void SetOrderStatus(Domain.Models.Order order)
        {
            const int PrepareShip = 7;
            const int Shipping = 8;
            using (var db = new YintaiHZhouContext())
            {
                using (var ts = new TransactionScope())
                {
                    var p = db.Orders.FirstOrDefault(t => t.OrderNo == order.OrderNo);
                    var saleOrders = db.OPC_Sale.Where(sa => sa.OrderNo == p.OrderNo).ToList();
                    var status = CheckStatus(saleOrders);
                    if (status == order.Status || status == NoStatus)
                        return;
                    p.UpdateDate = DateTime.Now;
                    p.Status = status;
                    p.UpdateUser = SystemDefine.SystemUser;
                    if (status == (int) EnumOrderStatus.PreparePack)
                    {
                        db.OrderLogs.Add(new OrderLog()
                        {
                            CreateDate = DateTime.Now,
                            CreateUser = 0,
                            CustomerId = order.CustomerId,
                            Operation = order.Memo ?? "准备发货",
                            OrderNo = order.OrderNo,
                            Type = PrepareShip,
                        });
                    }
                    if (status == (int) EnumOrderStatus.Shipped)
                    {
                        db.OrderLogs.Add(new OrderLog()
                        {
                            CreateDate = DateTime.Now,
                            CreateUser = 0,
                            CustomerId = order.CustomerId,
                            Operation = "已发货",
                            OrderNo = order.OrderNo,
                            Type = Shipping,
                        });

                        foreach (int shippingId in saleOrders.Select(x=>x.ShippingSaleId).Distinct().Where(x=>x.HasValue))
                        {
                            var shipping = db.OPC_ShippingSale.FirstOrDefault(x => x.Id == shippingId);
                            var shippingItems =
                                db.OPC_Sale.Where(
                                    x => x.ShippingSaleId.HasValue && x.ShippingSaleId.Value == shipping.Id)
                                    .Join(db.OPC_SaleDetail, s => s.SaleOrderNo, d => d.SaleOrderNo, (s, d) => d)
                                    .Join(db.OrderItems, d => d.OrderItemID, x => x.Id,
                                        (detail, item) => new {detail, item});
                            if(shipping == null) continue;

                            var outboundNo = CreateOutBoundNo(shipping.StoreId.HasValue ? shipping.StoreId.Value : 0);
                            db.Outbounds.Add(new Outbound()
                            {
                                OutboundNo = outboundNo,
                                SourceNo = shipping.OrderNo,
                                SourceType = 1,
                                ShippingContactPerson = shipping.ShippingContactPerson,
                                CreateDate = DateTime.Now,
                                CreateUser = 0,
                                Status = 1,
                                ShippingVia = shipping.ShipViaId,
                                ShippingNo = shipping.ShippingCode,
                                ShippingAddress = shipping.ShippingAddress,
                                UpdateDate = DateTime.Now,
                                UpdateUser = 0
                            });

                            foreach (var di in shippingItems)
                            {
                                db.OutboundItems.Add(new OutboundItem()
                                {
                                    CreateDate = DateTime.Now,
                                    ColorId = di.item.ColorValueId,
                                    ItemPrice = di.item.ItemPrice,
                                    UnitPrice = di.item.UnitPrice.Value,
                                    ExtendPrice = di.item.ExtendPrice,
                                    OutboundNo = outboundNo,
                                    ProductId = di.item.ProductId,
                                    Quantity = di.detail.SaleCount,
                                    SizeId = di.item.SizeValueId,
                                    UpdateDate = DateTime.Now
                                });
                            }
                        } 
                    }
                    db.SaveChanges();
                    ts.Complete();
                    Log.InfoFormat("完成订单状态更新,orderNo:{0},status:{1}", p.OrderNo, status);
                }
            }
        }

        private const int NoStatus = -10000;

        private int CheckStatus(IEnumerable<OPC_Sale> saleOrders)
        {
            if (saleOrders == null || !saleOrders.Any())
            {
                return NoStatus;
            }
            //已发货
            if (saleOrders.All(x => x.Status >= (int)EnumSaleOrderStatus.Shipped))
            {
                return (int)EnumOrderStatus.Shipped;
            }
            if (saleOrders.Any(x => x.Status == 35))
            {
                return (int)EnumOrderStatus.PreparePack;
            }

            if (saleOrders.Any(x => x.Status == 30 || x.Status == 25 || x.Status == 20 || x.Status == 15))
            {
                return (int)EnumOrderStatus.OrderPrinted;
            }

            //if (saleOrders.Any(x => x.Status == 2 || x.Status == 21))
            //{
            //    return (int)EnumOrderStatus.AgentConfirmed;
            //}
            return (int)EnumOrderStatus.PassConfirmed;
        }

        private string CreateOutBoundNo(int storeId)
        {
            using (var db = new YintaiHZhouContext())
            {
                var code = string.Concat(string.Format("O{0}{1}", storeId.ToString().PadLeft(3, '0'), DateTime.Now.ToString("yyMMdd"))
                   , DateTime.UtcNow.Ticks.ToString().Reverse().Take(5)
                       .Aggregate(new StringBuilder(), (s, e) => s.Append(e), s => s.ToString())
                       .PadRight(5, '0'));
                var existingCodes =
                    db.Outbounds.Count(c => c.OutboundNo == code && c.CreateDate >= DateTime.Today);
                if (existingCodes > 0)
                    code = string.Concat(code, (existingCodes + 1).ToString());
                return code;
            }
        }

        #endregion
    }
}
