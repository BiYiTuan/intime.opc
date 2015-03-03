using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_Stock:IEntity
    {
        public int Id { get; set; }
        public int SkuId { get; set; }
        public string SourceStockId { get; set; }
        public int SectionId { get; set; }
        public Nullable<int> Count { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public Nullable<bool> IsDel { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public string ProdSaleCode { get; set; }

        public string StoreCode { get; set; }

        public string SectionCode { get; set; }
        public string ProductName { get; set; }

        public string ProductCode { get; set; }
    }
}
