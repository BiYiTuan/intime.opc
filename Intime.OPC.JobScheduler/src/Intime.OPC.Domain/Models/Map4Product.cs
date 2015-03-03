using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Map4Product
    {
        public int Id { get; set; }
        public Nullable<int> ChannelId { get; set; }
        public string Channel { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string ChannelProductId { get; set; }
        public int ProductId { get; set; }
        public int Status { get; set; }
        public Nullable<int> IsImageUpload { get; set; }
    }
}
