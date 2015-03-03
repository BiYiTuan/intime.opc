using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class StorePromotionScope
    {
        public int Id { get; set; }
        public Nullable<int> StorePromotionId { get; set; }
        public Nullable<int> StoreId { get; set; }
        public string StoreName { get; set; }
        public string Excludes { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }
    }
}
