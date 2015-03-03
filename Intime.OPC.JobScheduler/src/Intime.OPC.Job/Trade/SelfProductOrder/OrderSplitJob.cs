using System.Data.Entity.Validation;
using System.Transactions;
using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Job.Trade.SelfProductOrder
{
    [DisallowConcurrentExecution]
    public class OrderSplitJob : IJob
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public List<Domain.Models.Order> QueryOrderNumberListUnSplited(DateTime bechTime)
        {
            using (var db = new YintaiHZhouContext())
            {
                return
                    db.Orders.Where(
                        o => o.CreateDate > bechTime && o.OrderProductType.HasValue && o.OrderProductType.Value == 2 &&
                             (o.PromotionFlag.HasValue && o.PromotionFlag.Value && o.Status == 11 ||
                              ((!o.PromotionFlag.HasValue || !o.PromotionFlag.Value) && o.Status == 1)) &&
                             !db.OPC_OrderSplitLog.Any(
                                 log => log.OrderNo == o.OrderNo && log.Status == -1) &&
                             (!db.OPC_OrderSplitLog.Any(l => o.OrderNo == l.OrderNo) ||
                              db.OPC_OrderSplitLog.Any(l => l.OrderNo == o.OrderNo && l.Status != 1))).Take(30).ToList();
            }
        }

        public void Execute(IJobExecutionContext context)
        {
            JobDataMap data = context.JobDetail.JobDataMap;
            var benchTime = data.ContainsKey("benchTime") ? data.GetDateTime("benchTime") : new DateTime(2014, 6, 13);
            var orderNos = QueryOrderNumberListUnSplited(benchTime);
            Log.ErrorFormat("开始自拍商品拆单，共获取订单数量{0}", orderNos.Count);
            foreach (var orderNo in orderNos)
            {
                try
                {
                    Split(orderNo);
                }
                catch (Exception ex)
                {
                    Log.ErrorFormat("拆单失败，订单号 = {0}", orderNo.OrderNo);
                    Log.Error(ex);
                }
            }
        }

        private void Split(Domain.Models.Order order)
        {
            using (var db = new YintaiHZhouContext())
            {
                using (var ts = new TransactionScope())
                {
                    var items =
                        db.OrderItems.Where(i => i.OrderNo == order.OrderNo)
                            .Join(db.Products, i => i.ProductId, p => p.Id, (i, p) => new { product = p, item = i })
                            .Join(db.IMS_Associate.Where(a => a.Status == 1), x => x.product.CreatedUser, a => a.UserId,
                                (x, a) => new { x.item, sectionid = a.SectionId }).GroupBy(x => x.sectionid);
                    int idx = 1;
                    foreach (var item in items)
                    {
                        
                        var saleOrderNo = string.Format("{0}-{1}", idx.ToString("D").PadLeft(3, '0'), order.OrderNo);
                        var sectionId = item.Key;
                        var section = db.Sections.FirstOrDefault(s => s.Id == sectionId);
                        //var sectionCode =
                        //    db.OPC_ChannelMap.FirstOrDefault(
                        //        x => x.MapType == (int)ChannelMapType.SectionId && x.InnerValue == sectionId);
                        decimal saleAmount = 0;
                        int salesCount = 0;
                        foreach (var p in item)
                        {
                            saleAmount += p.item.ItemPrice * p.item.Quantity;
                            salesCount += p.item.Quantity;
                            db.OPC_SaleDetail.Add(new OPC_SaleDetail()
                            {
                                SaleOrderNo = saleOrderNo,
                                CreatedDate = DateTime.Now,
                                CreatedUser = -10000,
                                OrderItemID = p.item.Id,
                                Price = p.item.ItemPrice,
                                ProdSaleCode = p.item.StoreSalesCode,
                                Remark = "自拍商品订单",
                                SaleCount = p.item.Quantity,
                                RemarkDate = DateTime.Now,
                                SectionCode = section == null ? string.Empty : section.SectionCode,
                                Status = 0,
                                StockId = -1,
                                UpdatedDate = DateTime.Now,
                                UpdatedUser = -10000,
                            });
                        }
                        db.OPC_Sale.Add(new OPC_Sale()
                        {
                            OrderNo = order.OrderNo,
                            SaleOrderNo = saleOrderNo,
                            SalesType = 0,
                            Status = 0,
                            SellDate = order.CreateDate,
                            SalesAmount = saleAmount,
                            SalesCount = salesCount,
                            SectionId = item.Key,
                            Remark = "自拍商品订单",
                            RemarkDate = DateTime.Now,
                            CreatedUser = -10000,
                            CreatedDate = DateTime.Now,
                            UpdatedUser = -10000,
                            UpdatedDate = DateTime.Now,

                        });
                        var splitLog = new OPC_OrderSplitLog
                        {
                            OrderNo = order.OrderNo,
                            Reason = "拆单成功",
                            Status = 1,
                            CreateDate = DateTime.Now
                        };
                        db.OPC_OrderSplitLog.Add(splitLog);
                        db.SaveChanges();
                        idx += 1;
                    }
                    ts.Complete();
                    Log.ErrorFormat("自拍商品拆单成功 OrderNo = {0}", order.OrderNo);
                }
            }
        }
    }
}
