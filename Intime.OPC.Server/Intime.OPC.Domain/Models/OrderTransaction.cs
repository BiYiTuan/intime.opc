using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    [System.Obsolete("<----------逻辑调整为：按照支付额降序，然后按code降序，取top 1------------->")]
    public partial class OrderTransaction:IEntity
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
