using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Map4Brand
    {
        public int Id { get; set; }
        public Nullable<int> ChannelId { get; set; }
        public string Channel { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int BrandId { get; set; }
        public string ChannelBrandId { get; set; }
    }
}
