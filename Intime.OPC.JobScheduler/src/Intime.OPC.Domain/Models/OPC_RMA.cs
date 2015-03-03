using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_RMA
    {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }
        public string RMANo { get; set; }
        public Nullable<bool> IsInquirer { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> Count { get; set; }
        public decimal RefundAmount { get; set; }
        public Nullable<bool> IsShipping { get; set; }
        public Nullable<bool> IsPackage { get; set; }
        public string OrderNo { get; set; }
        public int RMAType { get; set; }
        public int Status { get; set; }
        public decimal RMAAmount { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string BankCard { get; set; }
        public string RejectReason { get; set; }
        public Nullable<decimal> RebatePostfee { get; set; }
        public Nullable<decimal> Chargepostfee { get; set; }
        public Nullable<decimal> ActualAmount { get; set; }
        public string GiftReason { get; set; }
        public string InvoiceReason { get; set; }
        public string RebatePointReason { get; set; }
        public string PostalFeeReason { get; set; }
        public Nullable<decimal> ChargeGiftFee { get; set; }
        public string ContactPhone { get; set; }
        public Nullable<int> ShipviaId { get; set; }
        public string ShipNo { get; set; }
        public Nullable<int> UserId { get; set; }
        public string MailAddress { get; set; }
        public Nullable<int> RMAReason { get; set; }
        public string ContactPerson { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public string RMACashNum { get; set; }
        public Nullable<System.DateTime> RMACashDate { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> BackDate { get; set; }
        public Nullable<System.DateTime> CustomerAuthDate { get; set; }
        public Nullable<decimal> StoreFee { get; set; }
        public Nullable<decimal> CustomFee { get; set; }
        public Nullable<decimal> CompensationFee { get; set; }
        public string SaleRMASource { get; set; }
        public Nullable<int> RMAStatus { get; set; }
        public Nullable<int> RMACashStatus { get; set; }
        public Nullable<decimal> RealRMASumMoney { get; set; }
        public Nullable<decimal> RecoverableSumMoney { get; set; }
    }
}
