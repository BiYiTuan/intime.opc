using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Banner
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int SourceType { get; set; }
        public int SortOrder { get; set; }
        public int Status { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
