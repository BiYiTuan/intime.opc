using System.Collections.Generic;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Customer;

namespace Intime.OPC.Modules.GoodsReturn.Print
{
    public class PrintRMAModel
    {
        public IList<OPC_RMA> RmaDT { get; set; }
        public IList<RmaDetail> RMADetailDT { get; set; }
        public IList<Order> OrderDT { get; set; }
    }
    public class ReturnGoodsPrintModel
    {
        public IList<RMADto> RmaDT { get; set; }
        public IList<RmaDetail> RMADetailDT { get; set; }
        public IList<OrderDto> OrderDT { get; set; }

    }
}