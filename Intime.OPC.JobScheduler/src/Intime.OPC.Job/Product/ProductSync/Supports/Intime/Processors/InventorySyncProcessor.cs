using Intime.OPC.Domain.Models;
using System;
using System.Linq;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    public class InventorySyncProcessor : IInventorySyncProcessor
    {
        public Inventory Sync(OPC_SKU sku)
        {
            using (var db = new YintaiHZhouContext())
            {
                var stocks = db.OPC_Stock.Where(x => x.SkuId == sku.Id);
                if (!stocks.Any())
                {
                    return null;
                }

                int amount = 0;

                if (stocks.Any(x => x.Count > 0))
                {
                    var price = stocks.Where(x => x.Count >= 0).Min(x => x.Price);
                    var cnt = stocks.Where(x => x.Price == price).Sum(x => x.Count);
                    if (cnt.HasValue)
                    {
                        amount = cnt.Value;
                    }
                }

                var inventory = db.Inventories.FirstOrDefault(x => x.ProductId == sku.ProductId && x.PColorId == sku.ColorValueId && x.PSizeId == sku.SizeValueId);
                if (inventory == null)
                {
                    inventory = db.Inventories.Add(new Inventory()
                    {
                        ProductId = sku.ProductId,
                        PColorId = sku.ColorValueId,
                        PSizeId = sku.SizeValueId,
                        UpdateDate = DateTime.Now,
                        UpdateUser = SystemDefine.SystemUser,
                        ChannelInventoryId = 0,
                        Amount = amount
                    });
                }
                else
                {
                    inventory.UpdateDate = DateTime.Now;
                    inventory.UpdateUser = SystemDefine.SystemUser;
                    inventory.Amount = amount;
                }
                db.SaveChanges();
                return inventory;
            }
        }
    }
}
