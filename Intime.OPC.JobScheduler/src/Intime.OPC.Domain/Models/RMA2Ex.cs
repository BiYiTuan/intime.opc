using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class RMA2Ex
    {
        public int Id { get; set; }
        public string RMANo { get; set; }
        public string ExRMA { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
