
using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Request
{
    [DataContract]
    public class GetStoreByIdRequestData
    {
        [DataMember(Name = "storeno")]
        public string StoreNo { get; set; }
    }
}
