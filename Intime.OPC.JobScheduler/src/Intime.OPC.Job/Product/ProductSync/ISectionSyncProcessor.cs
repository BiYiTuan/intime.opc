using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product.ProductSync
{
    /// <summary>
    /// 专柜同步处理器
    /// </summary>
    public interface ISectionSyncProcessor
    {
        /// <summary>
        /// 同步专柜
        /// </summary>
        /// <param name="channelSectionId">渠道专柜编号</param>
        /// <param name="channelStoreNo">渠道门店编号</param>
        /// <returns>同步后的本地专柜信息</returns>
        Section Sync(string channelSectionId, string channelStoreNo);
    }
}
