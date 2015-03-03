using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Models
{
    /// <summary>
    /// 订单信息表
    /// </summary>
    public class OrderModel : Intime.OPC.Domain.Models.Order
    {
        private readonly IEnumerable<OrderItemModel> _items;

        public OrderModel(IEnumerable<OrderItemModel> items)
        {
            _items = items;
        }

        /// <summary>
        /// 订单的商品列表
        /// </summary>
        public IEnumerable<OrderItemModel> Items
        {
            get { return _items; }
        }
    }
}
