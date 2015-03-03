using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_SupplierInfo
    {
        public int Id { get; set; }
        public string SupplierNo { get; set; }
        public string SupplierName { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string Corporate { get; set; }
        public string Contact { get; set; }
        public string Contract { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string FaxNo { get; set; }
        public string TaxNo { get; set; }
        public int Status { get; set; }
        public string Memo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
    }
}
