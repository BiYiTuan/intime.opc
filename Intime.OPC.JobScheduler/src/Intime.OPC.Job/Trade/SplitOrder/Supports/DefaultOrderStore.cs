using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Supports
{
    /// <summary>
    /// 默认订单数据存储实现
    /// </summary>
    public class DefaultOrderStore : IOrderStore
    {
        public IEnumerable<OrderModel> GetPendingOrders()
        {
            var result = new List<OrderModel>();

            //获取待处理的订单
            using (var db = new YintaiHZhouContext())
            {
                // 只是获取支付成功的订单
                var orders =
                    db.Orders.Where(
                        x =>
                            x.Status == 1 &&
                            (!x.OrderProductType.HasValue || x.OrderProductType.Value == 1) &&
                            !db.OPC_OrderSplitLog.Any(log => log.OrderNo == x.OrderNo && log.Status == -1) &&
                            (!db.OPC_OrderSplitLog.Any(l => x.OrderNo == l.OrderNo) ||
                             db.OPC_OrderSplitLog.Any(l => l.OrderNo == x.OrderNo && l.Status != 1))).Take(20).ToList();

                result.AddRange(from order in orders
                                let items = (from item in db.OrderItems
                                             from sku in db.OPC_SKU
                                             where (item.OrderNo == order.OrderNo && item.ProductId == sku.ProductId && item.ColorValueId == sku.ColorValueId && item.SizeValueId == sku.SizeValueId)
                                             select new OrderItemModel()
                                             {
                                                 SkuId = sku.Id,
                                                 Id = item.Id,
                                                 BrandId = item.BrandId,
                                                 OrderNo = item.OrderNo,
                                                 Quantity = item.Quantity,
                                                 SizeId = item.SizeId,
                                                 SalesPerson = item.SalesPerson,
                                                 StoreId = item.StoreId,
                                                 ProductId = item.ProductId,
                                                 ProductName = item.ProductDesc,
                                                 ItemPrice = item.ItemPrice
                                             }).ToList()
                                select new OrderModel(items)
                                {
                                    Id = order.Id,
                                    CreateDate = order.CreateDate,
                                    OrderNo = order.OrderNo,
                                    Status = order.Status,
                                    Memo = order.Memo
                                });
            }

            return result;
        }
    }
}
