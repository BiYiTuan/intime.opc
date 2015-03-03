using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder
{
    /// <summary>
    /// 拆单策略接口 (拆单的执行顺序按照添加的先后顺序进行 )
    /// </summary>
    public interface ISplitStrategy
    {
        /// <summary>
        /// 订单拆分
        /// </summary>
        /// <param name="order">订单数据</param>
        /// <param name="sectionStocks">专柜库存数据</param>
        /// <returns>拆分后的列表</returns>
        SpliltResultModel Split(OrderModel order, SectionStocksModel sectionStocks);

        /// <summary>
        /// 是否支持本拆单策略
        /// </summary>
        /// <param name="order">订单信息</param>
        /// <param name="sectionStocks">专柜库存信息</param>
        /// <returns>此策略的前置条件</returns>
        bool Support(OrderModel order, SectionStocksModel sectionStocks);
    }
}
