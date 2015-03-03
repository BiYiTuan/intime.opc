using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Map4Category
    {
        public int Id { get; set; }
        public Nullable<int> ChannelId { get; set; }
        public string Channel { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string CategoryCode { get; set; }
        public int ChannelCategoryId { get; set; }
    }
}
