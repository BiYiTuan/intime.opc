using System;
using System.Linq;

using Common.Logging;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Processors
{
    /// <summary>
    /// 库存同步处理器
    /// </summary>
    public class StockSyncProcessor : IStockSyncProcessor
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private readonly ISectionSyncProcessor _sectionSyncProcessor;

        public StockSyncProcessor(ISectionSyncProcessor sectionSyncProcessor)
        {
            _sectionSyncProcessor = sectionSyncProcessor;
        }

        public OPC_Stock Sync(int skuId, ProductDto product) {
            using (var db = new YintaiHZhouContext())
            {
                /**
                 * 库存同步逻辑
                 * 1. 检查门店信息
                 * 2. 检查商品Id映射关系
                 */
                // 同步门店相关信息
                var sectionExt = _sectionSyncProcessor.Sync(product.SectionId, product.StoreNo);
                if (sectionExt == null)
                {
                    Log.ErrorFormat("同步库存时发生错误,专柜sectionId:{0}不存在", product.SectionId);
                    return null;
                }


                // 获取单品信息
                var skuExt = db.OPC_SKU.FirstOrDefault(s => s.Id == skuId);
                if (skuExt == null)
                {
                    Log.ErrorFormat("单品id:{0}不存在,请排查数据", skuId);
                    return null;
                }

                // 检查库存信息,进行价格和库存更新
                var stockExt = db.OPC_Stock.FirstOrDefault(s => s.SkuId == skuExt.Id && s.SectionId == sectionExt.Id);

                if (stockExt == null)
                {
                    var newStock = new OPC_Stock()
                    {
                        SkuId = skuExt.Id,
                        SectionId = sectionExt.Id,
                        Price = product.CurrentPrice,
                        Count = Convert.ToInt32( decimal.Floor( product.Stock??0)),
                        SourceStockId = product.ProductId,
                        ProdSaleCode = product.PosCode,
                        ProductName = product.ProductName,
                        SectionCode = product.SectionId,
                        StoreCode = product.StoreNo,
                        ProductCode = product.ProductCode,
                        Status = 1,
                        IsDel = false,
                        CreatedDate = DateTime.Now,
                        CreatedUser = SystemDefine.SystemUser,
                        UpdatedUser = SystemDefine.SystemUser,
                        UpdatedDate = DateTime.Now,
                        LabelPrice = product.LabelPrice
                    };

                    db.OPC_Stock.Add(newStock);
                    db.SaveChanges();

                    Log.TraceFormat("单品id:{1}创建成功", skuId);

                    return newStock;
                }

                stockExt.Price = product.CurrentPrice;
                stockExt.LabelPrice = product.LabelPrice;
                stockExt.Count = Convert.ToInt32(decimal.Floor(product.Stock ?? 0));
                stockExt.SourceStockId = product.ProductId;
                stockExt.ProdSaleCode = product.PosCode;
                stockExt.CreatedDate = DateTime.Now;
                stockExt.CreatedUser = SystemDefine.SystemUser;
                stockExt.UpdatedDate = DateTime.Now;
                stockExt.UpdatedUser = SystemDefine.SystemUser;

                // 保存更新价格
                db.SaveChanges();

                Log.TraceFormat("单品id:{1}更新成功", skuId);

                return stockExt;
            }
        }
    }
}
