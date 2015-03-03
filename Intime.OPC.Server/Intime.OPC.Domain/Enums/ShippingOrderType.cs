using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Enums
{
    public enum ShippingOrderType : int
    {
        None = 0,
        /// <summary>
        /// 出库单
        /// </summary>
        DeliveryOrder = 1,

        /// <summary>
        /// 快递单
        /// </summary>
        ExpressOrder = 2,
    }
}
