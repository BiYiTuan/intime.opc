using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_GiftCardOrder
    {
        public int Id { get; set; }
        public string No { get; set; }
        public int GiftCardItemId { get; set; }
        public decimal Amount { get; set; }
        public int PurchaseUserId { get; set; }
        public int OwnerUserId { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}
