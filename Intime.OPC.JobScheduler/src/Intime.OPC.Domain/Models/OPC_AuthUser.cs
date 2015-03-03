using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_AuthUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> SectionId { get; set; }
        public string LogonName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public string OrgId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
        public string SectionName { get; set; }
        public string OrgName { get; set; }
        public string DataAuthId { get; set; }
        public Nullable<bool> IsSystem { get; set; }
    }
}
