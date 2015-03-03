using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OrderLog
    {
        public int Id { get; set; }
        public string OrderNo { get; set; }
        public int CustomerId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public string Operation { get; set; }
        public int Type { get; set; }
    }
}
