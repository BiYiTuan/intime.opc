using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_InviteCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int SectionOperatorId { get; set; }
        public Nullable<int> AuthRight { get; set; }
        public int IsBinded { get; set; }
        public int UserId { get; set; }
        public int SourceType { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
        public int Status { get; set; }
    }
}
