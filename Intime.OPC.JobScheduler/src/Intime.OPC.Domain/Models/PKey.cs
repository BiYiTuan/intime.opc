using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class PKey
    {
        public int Id { get; set; }
        public string PKey1 { get; set; }
        public string Channel { get; set; }
        public int Status { get; set; }
    }
}
