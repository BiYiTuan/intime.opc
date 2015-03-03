using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class CategoryMap
    {
        public int Id { get; set; }
        public int ChannelCatId { get; set; }
        public int CatId { get; set; }
        public int Status { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public string ShowChannel { get; set; }
    }
}
