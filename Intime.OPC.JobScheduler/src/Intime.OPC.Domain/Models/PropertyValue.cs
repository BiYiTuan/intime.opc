using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class PropertyValue
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int ValueId { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public Nullable<bool> HasChild { get; set; }
    }
}
