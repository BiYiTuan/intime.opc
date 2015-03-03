using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateIncomeHistory
    {
        public int Id { get; set; }
        public int SourceType { get; set; }
        public string SourceNo { get; set; }
        public int AssociateUserId { get; set; }
        public decimal AssociateIncome { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
