using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsProvince { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int Status { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string ZipCode { get; set; }
        public Nullable<bool> IsCity { get; set; }
    }
}
