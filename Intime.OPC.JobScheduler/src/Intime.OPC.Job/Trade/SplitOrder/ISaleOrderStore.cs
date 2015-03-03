using System.Collections.Generic;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder
{
    /// <summary>
    /// 销售单存储接口
    /// </summary>
    public interface ISaleOrderStore
    {
        /// <summary>
        /// 保存销售单
        /// </summary>
        /// <param name="saleOrders">销售单列表</param>
        bool Save(IEnumerable<SaleOrderModel> saleOrders);

        /// <summary>
        /// 保存拆单失败的订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="ErrorReason"></param>
        /// <returns></returns>
        bool SaveSplitOrderLog(string orderNo, string ErrorReason,int status);
    }
}
