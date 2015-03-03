using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class SpecialTopicProductRelation
    {
        public int Id { get; set; }
        public int SpecialTopic_Id { get; set; }
        public int Product_Id { get; set; }
        public int Status { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
