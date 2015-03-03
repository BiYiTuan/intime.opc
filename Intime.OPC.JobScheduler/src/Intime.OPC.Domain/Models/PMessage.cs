using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class PMessage
    {
        public int Id { get; set; }
        public bool IsVoice { get; set; }
        public string TextMsg { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int FromUser { get; set; }
        public int ToUser { get; set; }
        public bool IsAuto { get; set; }
    }
}
