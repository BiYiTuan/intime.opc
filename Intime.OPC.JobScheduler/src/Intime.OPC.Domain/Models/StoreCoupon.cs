using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class StoreCoupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> ValidStartDate { get; set; }
        public Nullable<System.DateTime> ValidEndDate { get; set; }
        public string VipCard { get; set; }
        public Nullable<int> Points { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> StorePromotionId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> StoreId { get; set; }
    }
}
