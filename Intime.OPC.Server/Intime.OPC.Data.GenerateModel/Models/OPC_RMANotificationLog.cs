using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class OPC_RMANotificationLog
    {
        public int Id { get; set; }
        public string RMANo { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
    }
}
