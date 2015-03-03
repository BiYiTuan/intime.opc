using System.Collections.Generic;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder
{
    /// <summary>
    /// 订单接口
    /// </summary>
    public interface IOrderStore
    {
        /// <summary>
        /// 获取待处理的订单
        /// </summary>
        /// <returns></returns>
        IEnumerable<OrderModel> GetPendingOrders();
    }
}
