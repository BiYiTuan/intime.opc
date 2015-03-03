using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Intime.O2O.ApiClient.Domain
{
      [DataContract]
    public class Detail
    {
        [DataMember(Name = "ID")]
        public string Id { get; set; }
        [DataMember(Name = "PRODUCTID")]
        public string ProductId { get; set; }
        [DataMember(Name = "PRODUCTNAME")]
        public string ProductName { get; set; }
        [DataMember(Name = "PRICE")]

        public decimal Price { get; set; }
        [DataMember(Name = "DISCOUNT")]

        public decimal Discount { get; set; }
        [DataMember(Name = "VIPDISCOUNT")]

        public int VipDiscount { get; set; }
        [DataMember(Name = "QUANTITY")]

        public int Quantity { get; set; }
        [DataMember(Name = "TOTAL")]

        public decimal Total { get; set; }
        [DataMember(Name = "ROWNO")]

        public int RowNo { get; set; }

        [DataMember(Name = "COMCODE")]

        public string ComCode { get; set; }
        [DataMember(Name = "COUNTER")]

        public string Counter { get; set; }
        [DataMember(Name = "MEMO")]

        public string Memo { get; set; }
        [DataMember(Name = "STORENO")]

        public string StoreNo { get; set; }

    }
}
