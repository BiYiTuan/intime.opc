using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_StockPropertyValueRaw
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string SourceStockId { get; set; }
        public string PropertyData { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string Channel { get; set; }
        public string BrandSizeName { get; set; }
        public string BrandSizeCode { get; set; }
    }
}
