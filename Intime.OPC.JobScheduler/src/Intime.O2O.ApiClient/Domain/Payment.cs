using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Intime.O2O.ApiClient.Domain
{
    [DataContract]
    public class PayMent
    {
        [DataMember(Name = "ID")]
        public string Id { get; set; }
        [DataMember(Name = "TYPE")]

        public string Type { get; set; }
        [DataMember(Name = "TYPEID")]

        public int TypeId { get; set; }
        [DataMember(Name = "TYPENAME")]

        public string TypeName { get; set; }
        [DataMember(Name = "NO")]

        public string No { get; set; }
        [DataMember(Name = "AMOUNT")]

        public decimal Amount { get; set; }
        [DataMember(Name = "ROWNO")]

        public int RowNo { get; set; }
        [DataMember(Name = "MENO")]

        public string  Memo { get; set; }
        [DataMember(Name = "STORENO")]

        public string StoreNo { get; set; }

    }
}
