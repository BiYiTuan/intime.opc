using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_RMADetail
    {
        public int Id { get; set; }
        public string RMANo { get; set; }
        public int OrderItemId { get; set; }
        public string CashNum { get; set; }
        public string SectionCode { get; set; }
        public Nullable<int> StockId { get; set; }
        public int Status { get; set; }
        public int BackCount { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string ProdSaleCode { get; set; }
        public Nullable<bool> SalesPersonConfirm { get; set; }
        public System.DateTime RefundDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
    }
}
