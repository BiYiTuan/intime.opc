
using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Domain
{
    /// <summary>
    /// 品牌信息
    /// </summary>
    [DataContract]
    public class Brand
    {
        /// <summary>
        /// 品牌Id
        /// </summary>
        [DataMember(Name = "brandid")]
        public string BrandId { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
