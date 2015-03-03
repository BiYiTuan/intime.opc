using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Intime.OPC.Domain.Models;
using System.Transactions;

namespace Intime.OPC.Job.Trade.SplitOrder.Supports
{
    /// <summary>
    /// 默认销售单存储实现
    /// </summary>
    public class DefaultSaleOrderStore : ISaleOrderStore
    {
        public bool Save(IEnumerable<Models.SaleOrderModel> saleOrders)
        {
            // 遍历保存销售单
            foreach (var saleOrderModel in saleOrders)
            {
                using (var db = new YintaiHZhouContext())
                {
                    using (var ts = new TransactionScope())
                    {
                        var saleOrder = new OPC_Sale()
                        {
                            OrderNo = saleOrderModel.OrderNo,
                            SaleOrderNo = saleOrderModel.SaleOrderNo,
                            SalesType = 0,
                            Status = saleOrderModel.Status,
                            SellDate = saleOrderModel.SellDate,
                            SalesAmount = saleOrderModel.SalesAmount, //是明细累加出的总金额
                            SalesCount = saleOrderModel.SalesCount,
                            SectionId = saleOrderModel.SectionId,
                            CreatedDate = saleOrderModel.CreatedDate,
                            CreatedUser = saleOrderModel.CreatedUser,
                            UpdatedDate = saleOrderModel.CreatedDate,
                            UpdatedUser = saleOrderModel.CreatedUser,
                            CashDate = SplitOrderUtils.GetDefaultDateTime(),
                            RemarkDate = SplitOrderUtils.GetDefaultDateTime()
                        };
                        db.OPC_Sale.Add(saleOrder);

                        // 保存销售单详情
                        foreach (var saleDetail in saleOrderModel.Items)
                        {
                            saleDetail.UpdatedDate = DateTime.UtcNow;
                            saleDetail.Remark = String.Empty;
                            saleDetail.UpdatedUser = SystemDefine.SysUserId;
                            db.OPC_SaleDetail.Add(saleDetail);
                        }

                        // 检查销售是否已经存在，防止重复拆单,
                        // 处理方式：发现销售单存继续其他销售单处理，放弃当前的修改，不进行保存
                        if (db.OPC_Sale.Any(s => s.SaleOrderNo == saleOrderModel.SaleOrderNo))
                        {
                            SaveSplitOrderLog(saleOrderModel.OrderNo.ToString(CultureInfo.InvariantCulture),
                                "已经拆单完成，重复拆单", -999);
                            continue;
                        }

                        db.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            return true;

        }

        /// <summary>
        /// 保存拆单失败的订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="ErrorReason"></param>
        /// <returns></returns>
        public bool SaveSplitOrderLog(string orderNo, string reason, int status)
        {
            using (var db = new YintaiHZhouContext())
            {
                var log =
                    db.OPC_OrderSplitLog.FirstOrDefault(
                        x => x.OrderNo == orderNo && reason == x.Reason && x.Status == status);
                if (log == null)
                {
                    var opc_OrderSplitLog = new OPC_OrderSplitLog
                    {
                        OrderNo = orderNo,
                        Reason = reason,
                        Status = status,
                        CreateDate = DateTime.Now
                    };
                    db.OPC_OrderSplitLog.Add(opc_OrderSplitLog);
                }

                else
                {
                    log.CreateDate = DateTime.Now;
                }

                db.SaveChanges();

            }
            return true;
        }
    }
}
