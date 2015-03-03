using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public class OPC_AuthMenu : IEntity
    {
        public string MenuName { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
        public int PraentMenuId { get; set; }
        public bool IsValid { get; set; }
        public int Sort { get; set; }
        public string Url { get; set; }

        #region IEntity Members

        public int Id { get; set; }

        #endregion
    }
}