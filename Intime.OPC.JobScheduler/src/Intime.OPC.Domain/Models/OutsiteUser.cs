using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OutsiteUser
    {
        public int Id { get; set; }
        public int AssociateUserId { get; set; }
        public string OutsiteUserId { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int OutsiteType { get; set; }
    }
}
