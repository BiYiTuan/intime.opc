using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Map4Store
    {
        public int Id { get; set; }

        public int StoreId { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string ChannelStoreId { get; set; }

        public string Channel { get; set; }

        public Nullable<int> ChannelId { get; set; } 
    }
}
