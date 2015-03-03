using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public class OPC_AuthRole : IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateUserId { get; set; }

        public bool IsSystem { get; set; }

        #region IEntity Members

        public int Id { get; set; }

        #endregion
    }
}