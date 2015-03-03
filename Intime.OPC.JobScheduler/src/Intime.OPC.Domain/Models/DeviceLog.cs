using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class DeviceLog
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public string DeviceToken { get; set; }
        public int Type { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public string DeviceUid { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
    }
}
