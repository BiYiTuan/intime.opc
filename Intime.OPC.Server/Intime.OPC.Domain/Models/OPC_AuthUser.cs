using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public class OPC_AuthUser : IEntity
    {
        public OPC_AuthUser()
        {
            IsSystem = false;
        }

        public string Name { get; set; }
        public int? SectionId { get; set; }
        public string SectionName { get; set; }
        public string LogonName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool? IsValid { get; set; }
        public string OrgId { get; set; }

        public string OrgName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }

        public string DataAuthId { get; set; }

        public bool IsSystem { get; set; }

        #region IEntity Members

        public int Id { get; set; }

        #endregion
    }
}