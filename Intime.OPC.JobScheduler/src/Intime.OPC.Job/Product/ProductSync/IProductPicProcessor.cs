using Intime.OPC.Domain.Models;
using System;

namespace Intime.OPC.Job.Product.ProductSync
{
    /// <summary>
    /// 图片同步处理器
    /// </summary>
    public interface IProductPicProcessor
    {
        /// <summary>
        /// 同步商品图片
        /// </summary>
        /// <param name="channelProductId">商品编号</param>
        /// <param name="channelColorId">花色id</param>
        /// <param name="channelUrl">图片地址</param>
        /// <returns>保存后的本地资源实体</returns>
        Resource Sync(string channelProductId, string channelColorId, string channelUrl, string Id, int SeqNo,DateTime WriteTime);
    }
}
