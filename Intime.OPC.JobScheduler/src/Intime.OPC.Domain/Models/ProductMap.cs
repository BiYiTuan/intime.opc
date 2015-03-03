using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ProductMap
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ChannelPId { get; set; }
        public Nullable<int> ChannelBrandId { get; set; }
        public Nullable<int> ChannelCatId { get; set; }
        public string Channel { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
