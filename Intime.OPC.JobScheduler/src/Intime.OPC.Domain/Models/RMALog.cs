using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class RMALog
    {
        public int Id { get; set; }
        public string RMANo { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public string Operation { get; set; }
    }
}
