using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ProductProperty
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string PropertyDesc { get; set; }
        public int Status { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<bool> IsColor { get; set; }
        public Nullable<bool> IsSize { get; set; }
        public Nullable<int> ChannelPropertyId { get; set; }
    }
}
