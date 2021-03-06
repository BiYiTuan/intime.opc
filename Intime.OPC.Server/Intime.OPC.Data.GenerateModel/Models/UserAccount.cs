using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class UserAccount
    {
        public int Id { get; set; }
        public int AccountType { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int User_Id { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
    }
}
