using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Intime.O2O.ApiClient.Domain
{
      [DataContract]
    public class Head
    {
        //订单号
        [DataMember(Name = "ID")]
        public string Id { get; set; }
        //同订单号
        [DataMember(Name = "MAINID")]
        public string MainId { get; set; }
        //标识  1
        [DataMember(Name = "FLAG")]

        public int Flag { get;set;}
        [DataMember(Name = "CREATETIME")]

        public DateTime CreateTime { get; set; }
        [DataMember(Name = "PAYTIME")]

        //收银时间
        public DateTime PayTime { get; set; }
        [DataMember(Name = "TYPE")]

        //   1
        public int Type { get; set; }
        [DataMember(Name = "STATUS")]

        //状态
        public int Status { get; set; }
        [DataMember(Name = "QUANTITY")]

        //数量
        public int Quantity { get; set; }
        [DataMember(Name = "DISCOUNT")]


        public decimal Discount { get; set; }
        [DataMember(Name = "TOTAL")]

        public decimal Total { get; set; }
        [DataMember(Name = "VIPNO")]

        public string VipNo { get; set; }
        [DataMember(Name = "VIPMEMO")]

        public string VipMemo { get; set; }
        [DataMember(Name = "STRORENO")]

        public string StoreNo { get; set; }
        [DataMember(Name = "OLDID")]

        public string OldId { get; set; }
        [DataMember(Name = "OPERID")]

        public string OperId { get; set; }
        [DataMember(Name = "OPERNAME")]

        public string OperName { get; set; }
        [DataMember(Name = "OPERTIME")]

        public DateTime OperTime { get; set; }


    }
}
