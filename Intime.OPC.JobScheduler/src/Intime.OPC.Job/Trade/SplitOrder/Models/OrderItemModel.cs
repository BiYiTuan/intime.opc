using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Models
{
    /// <summary>
    /// 订单项数据模型
    /// </summary>
    public class OrderItemModel : OrderItem
    {
        /// <summary>
        /// 单品id
        /// </summary>
        public int SkuId { get; set; }
    }
}
