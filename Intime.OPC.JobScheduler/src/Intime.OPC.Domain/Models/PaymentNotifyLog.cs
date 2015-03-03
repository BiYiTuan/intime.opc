using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class PaymentNotifyLog
    {
        public int Id { get; set; }
        public string PaymentCode { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string OrderNo { get; set; }
        public string PaymentContent { get; set; }
    }
}
