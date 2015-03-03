using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class PointOrderRule
    {
        public int Id { get; set; }
        public int StorePromotionId { get; set; }
        public Nullable<int> RangeFrom { get; set; }
        public Nullable<int> RangeTo { get; set; }
        public Nullable<decimal> Ratio { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }
    }
}
