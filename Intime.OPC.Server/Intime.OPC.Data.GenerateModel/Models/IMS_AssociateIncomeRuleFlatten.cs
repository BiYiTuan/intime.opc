using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class IMS_AssociateIncomeRuleFlatten
    {
        public int Id { get; set; }
        public int RuleId { get; set; }
        public decimal Percentage { get; set; }
    }
}
