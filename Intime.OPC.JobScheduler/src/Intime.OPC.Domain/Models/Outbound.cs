using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Outbound
    {
        public int Id { get; set; }
        public string OutboundNo { get; set; }
        public string SourceNo { get; set; }
        public int SourceType { get; set; }
        public Nullable<int> ShippingVia { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingContactPerson { get; set; }
        public string ShippingContactPhone { get; set; }
        public string ShippingZipCode { get; set; }
        public int Status { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int UpdateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string ShippingNo { get; set; }
    }
}
