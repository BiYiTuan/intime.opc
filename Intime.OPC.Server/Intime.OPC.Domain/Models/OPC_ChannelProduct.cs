using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public class OPC_ChannelProduct : IEntity
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int ChannelId { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public DateTime? UpDateTime { get; set; }
        public DateTime? DownDateTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }

        #region IEntity Members

        public int Id { get; set; }

        #endregion
    }
}