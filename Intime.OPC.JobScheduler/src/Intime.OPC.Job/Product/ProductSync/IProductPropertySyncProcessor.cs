using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO;

namespace Intime.OPC.Job.Product.ProductSync
{
    public interface IProductPropertySyncProcessor
    {
        /// <summary>
        /// 同步商品属性
        /// </summary>
        /// <param name="productId">本地商品Id,说明:通过商品同步获取</param>
        /// <param name="channelPropertyValueName">渠道属性名称</param>
        /// <param name="channelPropertyValueId">渠道属性值</param>
        /// <param name="productPropertyType">属性类型</param>
        /// <returns>同步后的属性值信息</returns>
        ProductPropertyValue Sync(int productId, string channelPropertyValueName, string channelPropertyValueId, ProductPropertyType productPropertyType);
    }

    public interface IProductPropertySyncHandler
    {
        ProductPropertyValue SyncColor(int productId, ProductDto productDto);

        ProductPropertyValue SyncSize(int productId, ProductDto productDto);
    }
}
