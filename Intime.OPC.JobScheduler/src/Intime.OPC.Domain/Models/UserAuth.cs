using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class UserAuth
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedUser { get; set; }
        public int Type { get; set; }
    }
}
