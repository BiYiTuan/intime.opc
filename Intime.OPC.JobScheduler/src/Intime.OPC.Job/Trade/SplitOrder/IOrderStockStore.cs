using System.Collections.Generic;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder
{
    /// <summary>
    /// 库存数据存储接口
    /// </summary>
    public interface IOrderStockStore
    {
        /// <summary>
        /// 根据SKU获取专柜库存列表
        /// </summary>
        /// <param name="skus">单品列表</param>
        /// <returns>单品对应专柜的库存列表</returns>
        SectionStocksModel GetStocksBySkus(IEnumerable<int> skus);

        /// <summary>
        /// 通过销售单更新库存
        /// </summary>
        /// <param name="saleOrder">销售单</param>
        /// <returns>操作状态</returns>
        bool UpdateStock(IEnumerable<SaleOrderModel> saleOrder);
    }
}
