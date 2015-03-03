using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class RMA:IEntity
    {
        public int Id { get; set; }
        public string RMANo { get; set; }
        public string OrderNo { get; set; }
        public int RMAType { get; set; }
        public int Status { get; set; }
        public string Reason { get; set; }
        public decimal RMAAmount { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int UpdateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string BankCard { get; set; }
        public string RejectReason { get; set; }
        public Nullable<decimal> rebatepostfee { get; set; }
        public Nullable<decimal> chargepostfee { get; set; }
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
    }
}
