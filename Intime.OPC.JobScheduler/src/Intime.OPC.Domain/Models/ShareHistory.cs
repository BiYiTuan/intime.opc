using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ShareHistory
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int SourceType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int ShareTo { get; set; }
        public int Stauts { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int User_Id { get; set; }
    }
}
