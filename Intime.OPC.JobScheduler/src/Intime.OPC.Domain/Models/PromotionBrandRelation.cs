using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class PromotionBrandRelation
    {
        public int Id { get; set; }
        public int Promotion_Id { get; set; }
        public int Brand_Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
