using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_SaleDetail:IEntity
    {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }


        [System.Obsolete("暂时不要用这个状态，没有同步，请使用主表的状态")]
        public int Status { get; set; }
        public int StockId { get; set; }
        public int SaleCount { get; set; }
        public decimal Price { get; set; }
        public Nullable<int> BackNumber { get; set; }
        public string ProdSaleCode { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> RemarkDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedUser { get; set; }

        public int?  OrderItemId { get; set; }

        public string SectionCode { get; set; }
    }
}
