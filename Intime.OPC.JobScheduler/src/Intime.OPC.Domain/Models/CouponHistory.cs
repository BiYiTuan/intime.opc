using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class CouponHistory
    {
        public int Id { get; set; }
        public string CouponId { get; set; }
        public int User_Id { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int Status { get; set; }
        public int FromStore { get; set; }
        public int FromUser { get; set; }
        public int FromProduct { get; set; }
        public int FromPromotion { get; set; }
        public System.DateTime ValidStartDate { get; set; }
        public System.DateTime ValidEndDate { get; set; }
        public Nullable<bool> IsLimitOnce { get; set; }
    }
}
