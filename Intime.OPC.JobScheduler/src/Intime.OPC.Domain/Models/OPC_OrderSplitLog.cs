using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_OrderSplitLog
    {
        public int ID { get; set; }
        public string OrderNo { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
