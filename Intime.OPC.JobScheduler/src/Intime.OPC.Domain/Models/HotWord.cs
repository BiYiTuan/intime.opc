using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class HotWord
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public int Type { get; set; }
        public int SortOrder { get; set; }
        public int Status { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
