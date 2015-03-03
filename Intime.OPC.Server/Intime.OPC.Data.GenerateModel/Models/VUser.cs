using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class VUser
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public string Mobile { get; set; }
        public string EMail { get; set; }
        public int Status { get; set; }
        public int UserLevel { get; set; }
        public int Store_Id { get; set; }
        public int Region_Id { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public byte Gender { get; set; }
        public int Role_Id { get; set; }
        public string Role_Name { get; set; }
        public string Role_Description { get; set; }
        public int Role_Val { get; set; }
    }
}
