using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OutboundItem
    {
        public int Id { get; set; }
        public string OutboundNo { get; set; }
        public int ProductId { get; set; }
        public string ProductDesc { get; set; }
        public string StoreItemNo { get; set; }
        public string StoreItemDesc { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal ExtendPrice { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public decimal UnitPrice { get; set; }
        public Nullable<int> SizeId { get; set; }
        public Nullable<int> ColorId { get; set; }
    }
}
