using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class InboundPackage
    {
        public int Id { get; set; }
        public string SourceNo { get; set; }
        public int SourceType { get; set; }
        public int ShippingVia { get; set; }
        public string ShippingNo { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
    }
}
