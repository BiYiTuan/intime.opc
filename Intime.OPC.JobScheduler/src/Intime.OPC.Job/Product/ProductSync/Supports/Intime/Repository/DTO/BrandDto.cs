
using System.Runtime.Serialization;
namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO
{
    /// <summary>
    /// 品牌信息
    /// </summary>
    public class BrandDto
    {
        /// <summary>
        /// 品牌Id
        /// </summary>
        [DataMember(Name = "brandid")]
        public int BrandId { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
