
namespace Intime.OPC.Job.Product.ProductSync
{
    /// <summary>
    /// 商品同步处理器
    /// </summary>
    public interface IProductSyncProcessor<in T>
    {
        /// <summary>
        /// 同步商品
        /// </summary>
        /// <param name="channelProduct">渠道商品实体类</param>
        /// <returns>同步后的本地商品实体</returns>
        Domain.Models.Product Sync(T channelProduct);
    }
}
