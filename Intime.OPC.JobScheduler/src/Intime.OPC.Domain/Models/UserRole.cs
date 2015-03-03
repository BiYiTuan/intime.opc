using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public int Role_Id { get; set; }
        public int User_Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public int Status { get; set; }
    }
}
