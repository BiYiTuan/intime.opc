using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class CategoryPropertyValue
    {
        public int Id { get; set; }
        public string ValueDesc { get; set; }
        public int SortOrder { get; set; }
        public int Status { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int UpdateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int PropertyId { get; set; }
    }
}
