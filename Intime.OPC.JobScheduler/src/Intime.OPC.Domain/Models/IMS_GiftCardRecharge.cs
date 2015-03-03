using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_GiftCardRecharge
    {
        public int Id { get; set; }
        public int ChargeUserId { get; set; }
        public string OrderNo { get; set; }
        public string ChargePhone { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
    }
}
