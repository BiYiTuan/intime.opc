using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateIncomeRuleFlatten
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public decimal Percentage { get; set; }
    }
}
