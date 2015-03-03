using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PColorId { get; set; }
        public int PSizeId { get; set; }
        public int Amount { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
        public int ChannelInventoryId { get; set; }
    }
}
