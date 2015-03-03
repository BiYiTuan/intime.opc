using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateIncomeRuleFlex
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public decimal BenchFrom { get; set; }
        public decimal BenchTo { get; set; }
        public Nullable<decimal> Percentage { get; set; }
    }
}
