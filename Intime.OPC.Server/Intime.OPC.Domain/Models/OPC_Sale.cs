using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_Sale:IEntity
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string SaleOrderNo { get; set; }
        public int SalesType { get; set; }
        public Nullable<int> ShipViaId { get; set; }
        public int Status { get; set; }
        public string ShippingCode { get; set; }
        public Nullable<int> ShippingSaleId { get; set; }
        public Nullable<decimal> ShippingFee { get; set; }
        public Nullable<int> ShippingStatus { get; set; }
        public string ShippingRemark { get; set; }
        public System.DateTime SellDate { get; set; }
        public Nullable<bool> IfTrans { get; set; }
        public Nullable<int> TransStatus { get; set; }
        public decimal SalesAmount { get; set; }
        public int SalesCount { get; set; }
        public Nullable<int> CashStatus { get; set; }
        public string CashNum { get; set; }
        public Nullable<System.DateTime> CashDate { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<int> PrintTimes { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> RemarkDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedUser { get; set; }
    }
}
