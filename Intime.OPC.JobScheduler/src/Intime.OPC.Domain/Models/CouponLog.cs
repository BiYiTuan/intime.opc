using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class CouponLog
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public string ConsumeStoreNo { get; set; }
        public string ReceiptNo { get; set; }
        public string BrandNo { get; set; }
        public int ActionType { get; set; }
    }
}
