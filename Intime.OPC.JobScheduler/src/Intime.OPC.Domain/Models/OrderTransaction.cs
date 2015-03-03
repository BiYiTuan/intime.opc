using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OrderTransaction
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string PaymentCode { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string TransNo { get; set; }
        public bool IsSynced { get; set; }
        public Nullable<System.DateTime> SyncDate { get; set; }
        public string OutsiteUId { get; set; }
        public Nullable<int> OutsiteType { get; set; }
        public Nullable<int> OrderType { get; set; }
        public Nullable<int> CanSync { get; set; }
    }
}
