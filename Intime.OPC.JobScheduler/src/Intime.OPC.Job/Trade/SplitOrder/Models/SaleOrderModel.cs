using System.Collections.Generic;

using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Models
{
    /// <summary>
    /// 销售单数据模型
    /// </summary>
    public class SaleOrderModel : OPC_Sale
    {
        private readonly IEnumerable<OPC_SaleDetail> _items;

        public SaleOrderModel(IEnumerable<OPC_SaleDetail> items)
        {
            _items = items;
        }

        /// <summary>
        /// 销售单明细
        /// </summary>
        public IEnumerable<OPC_SaleDetail> Items
        {
            get { return _items; }
        }
    }
}
