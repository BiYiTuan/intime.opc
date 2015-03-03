using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class RMAReason
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
    }
}
