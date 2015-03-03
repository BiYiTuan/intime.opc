
using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Domain
{
    /// <summary>
    /// 专柜信息
    /// </summary>
    [DataContract]
    public class Section
    {
        /// <summary>
        /// 专柜Id
        /// </summary>
        [DataMember(Name = "COUNTERID")]
        public string CounterId { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [DataMember(Name = "STORENO")]
        public string StoreNo { get; set; }

        /// <summary>
        /// 专柜名称
        /// </summary>
        [DataMember(Name = "NAME")]
        public string Name { get; set; }

        /// <summary>
        /// 专柜状态
        /// </summary>
        [DataMember(Name = "STATUS")]
        public string Status { get; set; }
    }
}
