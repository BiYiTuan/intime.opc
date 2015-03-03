using Intime.OPC.Domain.Models;
using System;
using System.Linq;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    public class BrandSizeProcessor : IBrandSizeProcessor
    {
        public void Process(Repository.DTO.ProductDto product,Inventory inventory)
        {
            if (string.IsNullOrEmpty(product.BrandSizeCode) && string.IsNullOrEmpty(product.BrandSizeName))
            {
                return;
            }

            using (var db = new YintaiHZhouContext())
            {
                var rpv =
                    db.OPC_StockPropertyValueRaw.FirstOrDefault(
                        x => x.InventoryId == inventory.Id && x.SourceStockId == product.ProductId);
                if (rpv == null)
                {
                    db.OPC_StockPropertyValueRaw.Add(new OPC_StockPropertyValueRaw()
                    {
                        BrandSizeCode = product.BrandSizeCode,
                        BrandSizeName = product.BrandSizeName,
                        Channel = SystemDefine.IntimeChannel,
                        InventoryId = inventory.Id,
                        PropertyData = string.Empty,
                        SourceStockId = product.ProductId,
                        UpdateDate = DateTime.Now.AddDays(-1)
                    });
                }
                else
                {
                    rpv.BrandSizeName = product.BrandSizeName;
                    rpv.BrandSizeCode = product.BrandSizeCode;
                }
                db.SaveChanges();
            }
        }
    }
}
