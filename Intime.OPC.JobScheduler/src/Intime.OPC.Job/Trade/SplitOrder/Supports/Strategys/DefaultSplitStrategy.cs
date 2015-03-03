using System;
using System.Collections.Generic;
using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Supports.Strategys
{
    /// <summary>
    /// 根据专柜的优先级别进行订单的拆分，优先使用级别高的专柜的商品
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class DefaultSplitStrategy : AbstractSplitStrategy
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public override bool Support(OrderModel order, SectionStocksModel sectionStocks)
        {
            /**=========================================
            * 参数检查
            =========================================*/

            SplitOrderUtils.CheckArgument(order, sectionStocks);

            /**=========================================
             * 需要判断订单中的商品价格和是否库存充足
             =========================================*/
            return SplitOrderUtils.AllSkuHasStockForOrderPrice(order, sectionStocks);
        }

        protected override IList<SectionSaleDetailInfo> ProcessOrderItem2SectionSaleDetail(OrderItemModel orderItem, IEnumerable<OPC_Stock> stocks)
        {
            // 单品数量
            var quantity = orderItem.Quantity;
            var result = new List<SectionSaleDetailInfo>();

            /**
             * 处理订单中的单品
             */

            foreach (var stock in stocks)
            {
                if (!stock.Count.HasValue)
                {
                    continue;
                }

                /**
                 * 计算销售的数量
                 */
                var count = quantity <= stock.Count ? quantity : stock.Count.Value;

                // 生产销售清单
                var detail = new SectionSaleDetailInfo(
                    stock.SectionId, new OPC_SaleDetail()
                    {
                        SaleOrderNo = string.Empty,// SaleOrderNo = 在销售单统一处理
                        SaleCount = count,
                        Status = 0,
                        StockId = stock.Id,
                        Price = stock.Price,
                        OrderItemID= orderItem.Id,
                        CreatedDate = DateTime.Now,
                        CreatedUser = SystemDefine.SysUserId,
                        SectionCode = stock.SectionCode
                    });

                result.Add(detail);

                if (count >= quantity)
                {
                    break;
                }

                quantity -= count;
            }

            return result;
        }
    }
}
