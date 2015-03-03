using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product.ProductSync.Models
{
    /// <summary>
    /// 属性数据模型
    /// </summary>
    public class ProductPropertyAndValue
    {
        /// <summary>
        /// 商品属性
        /// </summary>
        public ProductProperty Property { get; set; }

        /// <summary>
        /// 商品属性值
        /// </summary>
        public ProductPropertyValue Value { get; set; }
    }
}
