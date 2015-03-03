using Common.Logging;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Supports.Strategys
{
    /// <summary>
    /// 库存不足拆单处理逻辑
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class StockShortageSplitStrategy : ISplitStrategy
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public bool Support(OrderModel order, SectionStocksModel sectionStocks)
        {
            /**=========================================
            * 参数检查
            =========================================*/

            SplitOrderUtils.CheckArgument(order, sectionStocks);

            /**=========================================
             * 需要判断订单中的商品库存充足
             =========================================*/
            return !SplitOrderUtils.AllSkuHasStockForOrderPrice(order, sectionStocks);
        }

        public SpliltResultModel Split(OrderModel order, SectionStocksModel sectionStocks)
        {
            return new SpliltResultModel(null, new StockShortageException("订单库存不足，订单中的价格商品库存不足!"));
        }
    }
}
