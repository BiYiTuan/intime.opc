using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateIncomeRuleFix
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public decimal FixAmount { get; set; }
    }
}
