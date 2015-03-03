using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Resource
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int SourceType { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ContentSize { get; set; }
        public string ExtName { get; set; }
        public Nullable<bool> IsDimension { get; set; }
        public Nullable<bool> IsExternal { get; set; }
        public Nullable<int> ColorId { get; set; }
        public string ValueId { get; set; }
        public Nullable<int> ChannelPicId { get; set; }
    }
}
