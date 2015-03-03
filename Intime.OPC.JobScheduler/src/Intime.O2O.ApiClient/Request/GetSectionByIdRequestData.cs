using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Request
{
    /// <summary>
    /// 根据Id获取专柜请求
    /// </summary>
    [DataContract]
    public class GetSectionByIdRequestData
    {
        [DataMember(Name = "counterid")]
        public string SectionId { get; set; }

        [DataMember(Name = "storeno")]
        public string StoreNo { get; set; }
    }
}
