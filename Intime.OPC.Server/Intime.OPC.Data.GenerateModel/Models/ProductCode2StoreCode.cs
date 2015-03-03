using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class ProductCode2StoreCode
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string StoreProductCode { get; set; }
        public int StoreId { get; set; }
        public int Status { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
        public Nullable<int> SectionId { get; set; }
    }
}
