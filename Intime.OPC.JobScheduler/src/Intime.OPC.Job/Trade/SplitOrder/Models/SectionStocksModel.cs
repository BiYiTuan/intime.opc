using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Models
{
    /// <summary>
    /// 订单库存数据模型
    /// </summary>
    /// <remarks>
    /// SKU 对应库存的列表
    /// </remarks>
    public class SectionStocksModel : Dictionary<int, IList<OPC_Stock>>
    {
        public SectionStocksModel()
        {
        }

        public SectionStocksModel(IEnumerable<OPC_Stock> items)
        {
            var temp = items.ToLookup(c => c.SkuId);

            foreach (var group in temp)
            {
                Add(group.Key, group.ToList());
            }
        }
    }
}
