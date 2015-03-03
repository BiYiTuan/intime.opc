using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class MappedProductBackup
    {
        public int Id { get; set; }
        public Nullable<int> ChannelId { get; set; }
        public string Channel { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string ChannelProductId { get; set; }
        public string ProductId { get; set; }
    }
}
