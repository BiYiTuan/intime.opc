using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Seed
    {
        public string Name { get; set; }
        public Nullable<int> Id { get; set; }
        public Nullable<int> Value { get; set; }
    }
}
