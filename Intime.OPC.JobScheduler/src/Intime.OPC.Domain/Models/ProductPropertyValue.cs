using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ProductPropertyValue
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string ValueDesc { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int Status { get; set; }
        public Nullable<int> ChannelValueId { get; set; }
    }
}
