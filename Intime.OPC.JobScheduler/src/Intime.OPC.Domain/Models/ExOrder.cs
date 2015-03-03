using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ExOrder
    {
        public int Id { get; set; }
        public string ExOrderNo { get; set; }
        public decimal Amount { get; set; }
        public string PaymentCode { get; set; }
        public System.DateTime PaidDate { get; set; }
        public Nullable<bool> IsShipped { get; set; }
        public Nullable<System.DateTime> ShipDate { get; set; }
        public Nullable<int> OrderType { get; set; }
    }
}
