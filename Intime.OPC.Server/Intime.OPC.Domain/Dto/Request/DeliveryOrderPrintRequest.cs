using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto.Request
{
    /// <summary>
    /// 出库单 打印
    /// </summary>
    public class DeliveryOrderPrintRequest
    {
        public int? Type { get; set; }

        public int? Times { get; set; }

        public int? DeliveryOrderId { get; set; }

        public Intime.OPC.Domain.Enums.ShippingOrderType? ShippingOrderType
        {
            get { return (Intime.OPC.Domain.Enums.ShippingOrderType)Type; }

            set { }
        }
    }
}
