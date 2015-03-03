using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class VerifyCode
    {
        public int Id { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string Code { get; set; }
        public int Type { get; set; }
        public int TryCount { get; set; }
        public int User_Id { get; set; }
        public int VerifyMode { get; set; }
        public string VerifySource { get; set; }
        public int Status { get; set; }
    }
}
