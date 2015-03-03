using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product.ProductSync
{
    public interface ISkuSyncProcessor
    {
        /// <summary>
        /// 同步Sku相关信息
        /// </summary>
        /// <param name="productId">商品Id</param>
        /// <param name="colorValueId">颜色Id</param>
        /// <param name="sizeValueId">尺寸Id</param>
        /// <returns></returns>
        OPC_SKU Sync(int productId, int colorValueId, int sizeValueId);
    }
}
