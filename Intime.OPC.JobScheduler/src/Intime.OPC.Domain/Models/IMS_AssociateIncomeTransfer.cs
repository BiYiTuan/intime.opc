using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateIncomeTransfer
    {
        public int Id { get; set; }
        public int PackageId { get; set; }
        public int TotalCount { get; set; }
        public int TotalFee { get; set; }
        public bool IsSuccess { get; set; }
        public string TransferRetCode { get; set; }
        public string TransferRetMsg { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int Status { get; set; }
        public string QueryRetCode { get; set; }
        public string QueryRetMsg { get; set; }
        public int SerialNo { get; set; }
    }
}
