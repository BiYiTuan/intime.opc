using System;
using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Domain
{
    [DataContract]
    public class PropertyValue
    {
        [DataMember(Name = "RN")]
        public int RN { get; set; }

        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "PROPERTYID")]
        public int PropertyId { get; set; }

        [DataMember(Name = "PROPERTYNAME")]
        public string PropertyName { get; set; }

        [DataMember(Name = "VALUE")]
        public string PropertyValueId { get; set; }

        [DataMember(Name = "MEMO")]
        public string ValueName { get; set; }

        [DataMember(Name = "ARTNO")]
        public string ProductCode { get; set; }

        [DataMember(Name = "SHOPPEID")]
        public string ShoppeId { get; set; }

        [DataMember(Name = "LASTUPDATE")]
        public DateTime LastUpdate { get; set; }

    }

    [DataContract]
    public class PropertyValueRaw
    {
        [DataMember(Name = "RN")]
        public int RN { get; set; }

        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "PROPERTYID")]
        public string PropertyId { get; set; }

        [DataMember(Name = "PROPERTYNAME")]
        public string PropertyName { get; set; }

        [DataMember(Name = "VALUE")]
        public string PropertyValueId { get; set; }

        [DataMember(Name = "MEMO")]
        public string ValueName { get; set; }

        [DataMember(Name = "ARTNO")]
        public string ProductCode { get; set; }

        [DataMember(Name = "SHOPPEID")]
        public string ShoppeId { get; set; }

        [DataMember(Name = "LASTUPDATE")]
        public DateTime LastUpdate { get; set; }

    }
}
