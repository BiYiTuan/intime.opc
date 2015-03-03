
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product.ProductSync.Models
{
    /// <summary>
    /// 渠道相关值映射关系
    /// </summary>
    public class ChannelMap
    {
        /// <summary>
        /// 本地值
        /// </summary>
        public int LocalId { get; set; }

        /// <summary>
        /// 渠道值
        /// </summary>
        public string ChannnelValue { get; set; }

        /// <summary>
        /// 映射类型
        /// </summary>
        public ChannelMapType MapType { get; set; }

    }
}
