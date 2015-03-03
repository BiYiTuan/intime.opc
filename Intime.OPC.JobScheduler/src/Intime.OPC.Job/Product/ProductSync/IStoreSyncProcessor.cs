using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product.ProductSync
{
    /// <summary>
    /// 门店同步处理器
    /// </summary>
    public interface IStoreSyncProcessor
    {
        /// <summary>
        ///同步门店
        /// </summary>
        /// <param name="channelStoreNo">渠道门店编号</param>
        /// <returns>同步后的本地门店信息</returns>
        Store Sync(string channelStoreNo);
    }
}
