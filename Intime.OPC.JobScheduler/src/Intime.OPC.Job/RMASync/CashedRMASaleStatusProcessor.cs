using Common.Logging;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Order.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Job.RMASync
{
    public class CashedRMASaleStatusProcessor : AbstractRMASaleStatusProcessor
    {
         private static readonly ILog Log = LogManager.GetCurrentClassLogger();
         public CashedRMASaleStatusProcessor(EnumRMAStatus status) : base(status) { }

        /// <summary>
        /// OPC_SaleRMA  RMACashStatus 入收银状态 Status 退货单状态
        /// OPC_RMA    RMACashNum 、RMACashDate、Status 退货单状态
        /// </summary>
        /// <param name="rmaNo"></param>
        /// <param name="statusResult"></param>
        public override void Process(string rmaNo, OrderStatusResultDto statusResult)
        {
            using (var db = new YintaiHZhouContext())
            {
                var saleRMA = db.OPC_RMA.FirstOrDefault(t => t.RMANo == rmaNo);
                saleRMA.RMACashStatus = (int)EnumCashStatus.Cashed;
                saleRMA.RMAStatus = (int)EnumReturnGoodsStatus.Valid;
                saleRMA.RMACashDate = statusResult.PosTime;
                saleRMA.RMACashNum = statusResult.PosSeqNo;
                saleRMA.UpdatedDate = DateTime.Now;
                saleRMA.UpdatedUser = -100;          

                db.SaveChanges();

                if (string.IsNullOrEmpty(statusResult.Products_SaleCodes))
                {
                    Log.ErrorFormat("没有销售码信息,退货单号{0}",rmaNo);
                    return;
                }

                Log.Error("**************************************");
                Log.ErrorFormat(statusResult.Products_SaleCodes);
                Log.Error("**************************************");

                var slices = ParseProductIdAndPosCode(statusResult.Products_SaleCodes);

                foreach (var slice in slices)
                {
                    var productId = slice.Key;
                    var detail =
                        db.OPC_RMADetail.Where(x => x.RMANo == rmaNo)
                            .Join(db.OPC_Stock.Where(s => s.SourceStockId == productId), d => d.StockId, s => s.Id,
                                (o, s) => o)
                            .FirstOrDefault();
                    if (detail != null)
                    {
                        detail.ProdSaleCode = slice.Value;
                        db.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// 解析信息部给的结构
        /// </summary>
        /// <param name="strPosSeq">信息部给的结构 : productid|comcode,productid|comcode</param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<string, string>> ParseProductIdAndPosCode(string strPosSeq)
        {
            var slices = strPosSeq.Split(',');
            return from slice in slices select slice.Split('|') into kv where kv.Length == 2 select new KeyValuePair<string, string>(kv[0],kv[1]);
        }
    }

}
