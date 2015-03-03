using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Request
{
    [DataContract]
    public class GetBrandByIdRequestData
    {
        [DataMember(Name = "brandid")]
        public string BrandId { get; set; }
    }
}
