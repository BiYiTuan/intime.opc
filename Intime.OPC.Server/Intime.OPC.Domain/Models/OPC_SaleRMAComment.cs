using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public class OPC_SaleRMAComment : IEntity
    {
        public string RMANo { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }

        #region IEntity Members

        public int Id { get; set; }

        #endregion
    }
}