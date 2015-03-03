using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class CategoryProperty
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string PropertyDesc { get; set; }
        public int SortOrder { get; set; }
        public int Status { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public Nullable<bool> IsVisible { get; set; }
        public Nullable<bool> IsSize { get; set; }
    }
}
