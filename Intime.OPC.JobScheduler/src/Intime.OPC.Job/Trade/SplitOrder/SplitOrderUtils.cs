using System;
using System.Linq;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder
{
    public static class SplitOrderUtils
    {
        public static void CheckArgument(OrderModel order, SectionStocksModel sectionStocks)
        {
            if (order == null)
            {
                throw new ArgumentNullException("order");
            }

            if (sectionStocks == null)
            {
                throw new ArgumentNullException("sectionStocks");
            }

            if (order.Items == null)
            {
                throw new ArgumentNullException("items");
            }
        }

        public static bool AllSkuHasStockForOrderPrice(OrderModel order, SectionStocksModel stocks)
        {
            // 没有库存信息，订单中的所有商品库存不足，不支持
            if (stocks.Count == 0)
            {
                return false;
            }


            /**
             * 遍历所有的商品检查库存是否充足
             */
            foreach (var item in order.Items)
            {
                /**
                 * 根据skuId获取所有的商品供应商
                 */
                if (!stocks.ContainsKey(item.SkuId))
                {
                    return false;
                }

                var skuStocks = stocks[item.SkuId];

                /**
                 * 如果某个商品所有的专柜都没有提供，返回false
                 */
                if (skuStocks.Count == 0)
                {
                    return false;
                }

                /**
                 * 判断当前商品在所有的专柜是否存在
                 * 如果价格为订单中的价格的商品小于默认专柜的库存数量，返回false
                 */
                var allStock = stocks[item.SkuId].Where(p => p.Price == item.ItemPrice).Sum(c => c.Count);

                if (allStock < item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }

        public static DateTime GetDefaultDateTime()
        {
            return new DateTime(1973, 1, 1);
        }
    }
}
