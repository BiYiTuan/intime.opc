using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Order2Ex
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public string ExOrderNo { get; set; }
        public System.DateTime UpdateTime { get; set; }
    }
}
