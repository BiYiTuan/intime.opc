using System.Collections.Generic;
using System.Linq;

using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Trade.SplitOrder.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Supports
{
    public class DefaultOrderStockStore : IOrderStockStore
    {
        public SectionStocksModel GetStocksBySkus(IEnumerable<int> skus)
        {
            using (var db = new YintaiHZhouContext())
            {
                var stocks = db.OPC_Stock.Where(c => skus.Contains(c.SkuId)).OrderByDescending(c => c.Count);
                return new SectionStocksModel(stocks);
            }
        }

        public bool UpdateStock(IEnumerable<SaleOrderModel> saleOrders)
        {
            using (var db = new YintaiHZhouContext())
            {
                foreach (var saleOrder in saleOrders)
                {
                    var items = saleOrder.Items;

                    foreach (var item in items)   
                    {
                        var stock = db.OPC_Stock.FirstOrDefault(c => c.Id == item.StockId);
                        if (stock != null) stock.Count = stock.Count - item.SaleCount;
                    }
                }
                db.SaveChanges();
                return true;
            }
        }
    }
}
