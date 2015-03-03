using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Map4Inventory
    {
        public int Id { get; set; }
        public string attr { get; set; }
        public string desc { get; set; }
        public string stockId { get; set; }
        public Nullable<int> sellerUin { get; set; }
        public Nullable<int> soldNum { get; set; }
        public long skuId { get; set; }
        public int status { get; set; }
        public Nullable<int> num { get; set; }
        public string saleAttr { get; set; }
        public string pic { get; set; }
        public string specAttr { get; set; }
        public int ProductId { get; set; }
        public long InventoryId { get; set; }
        public Nullable<decimal> price { get; set; }
        public string itemId { get; set; }
        public Nullable<int> ChannelId { get; set; }
        public string Channel { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
