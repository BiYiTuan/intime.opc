using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class SpecialTopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string TargetValue { get; set; }
    }
}
