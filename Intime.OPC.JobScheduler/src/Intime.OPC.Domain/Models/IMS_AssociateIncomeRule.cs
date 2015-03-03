using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateIncomeRule
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int RuleType { get; set; }
        public int Status { get; set; }
    }
}
