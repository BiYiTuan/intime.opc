using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class WXReply
    {
        public int Id { get; set; }
        public string MatchKey { get; set; }
        public string ReplyMsg { get; set; }
        public int Status { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
