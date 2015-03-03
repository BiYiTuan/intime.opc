using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class VUserRole
    {
        public int Role_Id { get; set; }
        public int User_Id { get; set; }
        public string Role_Name { get; set; }
        public string Role_Description { get; set; }
        public int Role_Val { get; set; }
    }
}
