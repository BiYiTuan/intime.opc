using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class DeviceToken
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string Token { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
    }
}
