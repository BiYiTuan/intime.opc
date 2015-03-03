using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ConfigMsg
    {
        public int Id { get; set; }
        public string MKey { get; set; }
        public string Channel { get; set; }
        public string Message { get; set; }
        public Nullable<int> StoreId { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
