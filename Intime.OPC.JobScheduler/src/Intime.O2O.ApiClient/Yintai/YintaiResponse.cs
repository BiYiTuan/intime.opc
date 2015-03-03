using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Yintai
{
    [DataContract]
    public class YintaiResponse
    {
        [DataMember(Name = "isSuccessful")]
        public bool IsSuccessful { get; set; }

        [DataMember(Name = "statusCode")]
        public string StatusCode { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "userId")]
        public string UserId { get; set; }

        [DataMember(Name = "ServerVersion")]
        public string ServerVersion { get; set; }

        [DataMember(Name = "Content_type")]
        public string Content_type { get; set; }

        [DataMember(Name = "Date")]
        public string Date { get; set; }

        [DataMember(Name = "Data")]
        public string RawData { get; set; }
    }
}
