using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class NotificationLog
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> NotifyDate { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> SourceType { get; set; }
        public Nullable<int> SourceId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    }
}
