using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_ChannelMap
    {
        public int Id { get; set; }
        public string InnerValue { get; set; }
        public string ChannelValue { get; set; }
        public Nullable<int> MapType { get; set; }
        public string Channel { get; set; }
    }
}
