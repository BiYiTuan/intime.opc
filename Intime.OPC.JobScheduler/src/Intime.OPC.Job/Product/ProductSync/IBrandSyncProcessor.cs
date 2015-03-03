using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product.ProductSync
{
    /// <summary>
    /// 品牌同步处理器
    /// </summary>
    public interface IBrandSyncProcessor
    {
        /// <summary>
        /// 同步品牌
        /// </summary>
        /// <param name="channelBrandId">渠道品牌编号</param>
        /// <param name="channelBrandName">渠道品牌名称</param>
        /// <returns>转化后的本地品牌信息</returns>
        Brand SyncBrand(string channelBrandId, string channelBrandName);
    }
}
