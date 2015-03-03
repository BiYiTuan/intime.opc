using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Job.Product.StockAggregate
{
    public class StockModel
    {
        public int SkuId { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }
    }
    public interface IStockAggregator
    {
        StockModel Aggregate(IEnumerable<OPC_Stock> stocks);
    }

    public class DefaultStockAggregator : IStockAggregator
    {
        #region IStockAggregator 成员

        public StockModel Aggregate(IEnumerable<OPC_Stock> stocks)
        {
            if (stocks == null || stocks.Count() == 0)
            {
                throw new ArgumentException("stocks");
            }
            var skuId = stocks.First().SkuId;

            if (stocks.Any(x => x.Count > 0))
            {
                var price = stocks.Where(x => x.Count >= 0).Min(x => x.Price);
                var amount = stocks.Where(x => x.Price == price).Sum(x => x.Count);
                return new StockModel
                {
                    SkuId = skuId,
                    Count = amount.Value,
                    Price = price
                };
            }
            return new StockModel
            {
                Count = 0,
                SkuId = skuId,
                Price = stocks.Max(x => x.Price)
            };
        }

        #endregion
    }

    public interface IStockAttregateProcessor
    {
        void Process(StockModel stock);
    }

    public class AggregateToInveontoryProcessor : IStockAttregateProcessor
    {
        #region IStockAttregateProcessor 成员

        public void Process(StockModel stock)
        {
            using (var db = new YintaiHZhouContext())
            {
                var sku = db.OPC_SKU.FirstOrDefault(x => x.Id == stock.SkuId);
                var inventory = db.Inventories.FirstOrDefault(x => x.ProductId == sku.ProductId && x.PColorId == sku.ColorValueId && x.PSizeId == sku.SizeValueId);
                if (inventory == null)
                {
                    db.Inventories.Add(new Inventory()
                    {
                        ProductId = sku.ProductId,
                        PColorId = sku.ColorValueId,
                        PSizeId = sku.SizeValueId,
                        UpdateDate = DateTime.Now,
                        UpdateUser = SystemDefine.SystemUser,
                        ChannelInventoryId = 0,
                        Amount = stock.Count
                    });
                }
                else
                {
                    inventory.UpdateDate = DateTime.Now;
                    inventory.UpdateUser = SystemDefine.SystemUser;
                    inventory.Amount = stock.Count;
                }
                db.SaveChanges();
            }
        }

        #endregion
    }

    public class Set4SaleProcessor : IStockAttregateProcessor
    {
        #region IStockAttregateProcessor 成员

        public void Process(StockModel stock)
        {
            using (var db = new YintaiHZhouContext())
            {
                var product = db.Products.Join(db.OPC_SKU.Where(x => x.Id == stock.SkuId), p => p.Id, x => x.ProductId, (x, p) => x).FirstOrDefault();
                if (null != product)
                {
                    if (!product.Is4Sale.HasValue || !product.Is4Sale.Value || product.Price != stock.Price)
                    {
                        product.Is4Sale = true;
                        product.Price = stock.Price;
                        product.UpdatedDate = DateTime.Now;
                        //product.UpdatedUser = SystemDefine.SystemUser;
                        db.SaveChanges();
                    }
                }
            }
        }

        #endregion
    }

    public abstract class AbstractStockHandler : IStockAttregateProcessor
    {
        protected IEnumerable<IStockAttregateProcessor> _processors;
        protected AbstractStockHandler(IEnumerable<IStockAttregateProcessor> processors)
        {
            this._processors = processors;
        }

        public abstract void Process(StockModel model);
    }

    public class DefaultStockHandler : AbstractStockHandler
    {
        public DefaultStockHandler(IEnumerable<IStockAttregateProcessor> processors) : base(processors) { }
        public override void Process(StockModel model)
        {
            foreach (var processor in _processors)
            {
                processor.Process(model);
            }
        }
    }
}
