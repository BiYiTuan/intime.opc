using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Request
{
    [DataContract]
    public class GetProductImagesRequestData
    {
        [DataMember(Name = "page")]
        public int PageIndex { get; set; }

        [DataMember(Name = "size")]
        public int PageSize { get; set; }

        [DataMember(Name = "lastupdate")]
        public string LastUpdate { get; set; }
    }
}
