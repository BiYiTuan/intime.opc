using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Remind
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int RemindUser { get; set; }
        public int Type { get; set; }
        public int SourceId { get; set; }
        public bool IsRemind { get; set; }
        public int Stauts { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
    }
}
