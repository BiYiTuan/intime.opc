using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class IMS_AssociateIncomeRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BankName { get; set; }
        public string BankNo { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string BankCode { get; set; }
        public string BankAccountName { get; set; }
        public string TransferErrorCode { get; set; }
        public string TransferErrorMsg { get; set; }
        public Nullable<decimal> TransferFee { get; set; }
    }
}
