using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Property
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public Nullable<int> OrderFlag { get; set; }
        public bool IsSaleProperty { get; set; }
        public bool IsKeyProperty { get; set; }
        public bool IsNecessary { get; set; }
    }
}
