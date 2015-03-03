using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO;

namespace Intime.OPC.Job.Product.ProductSync
{
    /// <summary>
    /// OPC库存同步处理器
    /// </summary>
    public interface IStockSyncProcessor
    {
        /// <summary>
        /// 同步单品
        /// </summary>
        /// <param name="skuId">本地skuid</param>
        /// <param name="product">单品dto对象</param>
        /// <returns></returns>
        OPC_Stock Sync(int skuId, ProductDto product);
    }
}
