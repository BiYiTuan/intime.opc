using System.Linq;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    /// <summary>
    /// Sku同步处理器
    /// </summary>
    public class SkuSyncProcessor : ISkuSyncProcessor
    {
        public OPC_SKU Sync(int productId, int colorValueId, int sizeValueId)
        {
            using (var db = new YintaiHZhouContext())
            {
                var skuExt = db.OPC_SKU.FirstOrDefault(p => p.ColorValueId == colorValueId
                                                            && p.SizeValueId == sizeValueId
                                                            && p.ProductId == productId);
                // 如果已经存在SKU记录直接返回
                if (skuExt != null)
                {
                    return skuExt;
                }

                // 创建新的Sku并返回
                var newSku = new OPC_SKU()
                {
                    ProductId = productId,
                    ColorValueId = colorValueId,
                    SizeValueId = sizeValueId
                };

                db.OPC_SKU.Add(newSku);
                db.SaveChanges();

                return newSku;
            }
        }
    }
}
