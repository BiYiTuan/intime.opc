using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class Brand:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Description { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public string Logo { get; set; }
        public string WebSite { get; set; }
        public int Status { get; set; }
        public string Group { get; set; }
        public Nullable<int> ChannelBrandId { get; set; }

    }
}
