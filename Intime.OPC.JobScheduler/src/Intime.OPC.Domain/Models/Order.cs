using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<decimal> RecAmount { get; set; }
        public int Status { get; set; }
        public string PaymentMethodCode { get; set; }
        public string PaymentMethodName { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingContactPerson { get; set; }
        public string ShippingContactPhone { get; set; }
        public Nullable<bool> NeedInvoice { get; set; }
        public string InvoiceSubject { get; set; }
        public string InvoiceDetail { get; set; }
        public Nullable<decimal> ShippingFee { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
        public string ShippingNo { get; set; }
        public Nullable<int> ShippingVia { get; set; }
        public int StoreId { get; set; }
        public int BrandId { get; set; }
        public string Memo { get; set; }
        public Nullable<decimal> InvoiceAmount { get; set; }
        public Nullable<int> TotalPoints { get; set; }
        public string OrderSource { get; set; }
        public Nullable<int> OrderProductType { get; set; }
        public Nullable<bool> PromotionFlag { get; set; }
        public string PromotionDesc { get; set; }
        public string PromotionRules { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
    }
}
