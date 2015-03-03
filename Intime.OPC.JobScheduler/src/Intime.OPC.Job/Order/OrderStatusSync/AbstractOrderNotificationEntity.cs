using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    public abstract class AbstractOrderNotificationEntity
    {
        protected OPC_Sale _saleOrder;
        protected AbstractOrderNotificationEntity(OPC_Sale saleOrder)
        {
            this._saleOrder = saleOrder;
        }

        public dynamic CreateNotifiedEntity()
        {
            using (var db = new YintaiHZhouContext())
            {
                var id = _saleOrder.SaleOrderNo;
                var status = (int)Status;
                // wallace 2014-07-10 OPC暂时无法支持E014（天猫积分支付)，为避免通知到单品时失败，暂时去掉积分支付方式
                var trans =
                    db.OrderTransactions.Where(t => t.OrderNo == _saleOrder.OrderNo)
                        .Join(db.PaymentMethods, t => t.PaymentCode, p => p.Code, (t, p) => new { trans = t, payment = p })
                        .OrderByDescending(t => t.payment.Code)
                        .FirstOrDefault();
                var order = db.Orders.FirstOrDefault(o => o.OrderNo == _saleOrder.OrderNo);
                var storeno = string.Empty;
                if (order == null)
                {
                    throw new OrderNotificationException(string.Format("Not exists order! order No ({0})", _saleOrder.OrderNo));
                }

                if (trans == null)
                {
                    throw new OrderNotificationException(string.Format("Order has no payment information ! order no ({0})", _saleOrder.OrderNo));
                }

                var detail = new List<dynamic>();
                var payment = new List<dynamic>();
                int idx = 1;

                if (order.OrderProductType == (int)OrderProductType.System)
                {
                    foreach (var de in db.OPC_SaleDetail.Where(x => x.SaleOrderNo == _saleOrder.SaleOrderNo)
                        .Join(db.OPC_Stock, x => x.StockId, s => s.Id, (x, s) => new { detail = x, stock = s }))
                    {
                        storeno = de.stock.StoreCode;
                        payment.Add(new
                        {
                            id = _saleOrder.SaleOrderNo,
                            type = trans.payment.Prefix,
                            typeid = trans.payment.Code,
                            typename = trans.payment.Name,
                            no = trans.trans.TransNo,
                            amount = (de.detail.Price * de.detail.SaleCount).ToString(),
                            rowno = idx,
                            memo = string.Empty,
                            storeno,
                        });
                        detail.Add(new
                        {
                            id,
                            productid = de.stock.SourceStockId,
                            productname = de.stock.ProductName,
                            price = de.detail.Price.ToString(),
                            discount = "0.0",
                            vipdiscount = 0,
                            quantity = de.detail.SaleCount,
                            total = (de.detail.Price * de.detail.SaleCount).ToString(),
                            rowno = idx,
                            counter = de.stock.SectionCode,
                            memo = de.detail.Remark,
                            storeno = de.stock.StoreCode
                        });
                        idx += 1;
                    }
                }
                else
                {
                    storeno =
                        db.Map4Store.Where(x => x.Channel == "intime")
                            .Join(db.Sections.Where(x => x.Id == _saleOrder.SectionId), m => m.StoreId, s => s.StoreId,
                                (x, s) => x.ChannelStoreId)
                            .FirstOrDefault();

                    if (string.IsNullOrEmpty(storeno))
                    {
                        throw new OrderNotificationException("没有找到映射的门店编码");
                    }

                    foreach (var item in db.OPC_SaleDetail.Where(x => x.SaleOrderNo == _saleOrder.SaleOrderNo).Join(db.OrderItems.Where(i => i.OrderNo == _saleOrder.OrderNo), d => d.OrderItemID, i => i.Id, (oi, item) => new { detail = oi, item }))
                    {
                        payment.Add(new
                        {
                            id = _saleOrder.SaleOrderNo,
                            type = trans.payment.Prefix,
                            typeid = trans.payment.Code,
                            typename = trans.payment.Name,
                            no = trans.trans.TransNo,
                            amount = (item.detail.Price * item.detail.SaleCount).ToString(),
                            rowno = idx,
                            memo = string.Empty,
                            storeno,
                        });
                        detail.Add(new
                        {
                            id,
                            productid = "NULL",// 自拍商品在单品系统里不存在，传null -- wallace and xuchang 2014-12-23
                            productname = item.item.ProductDesc,
                            price = item.detail.Price.ToString(),
                            discount = "0.0",
                            vipdiscount = 0,
                            quantity = item.detail.SaleCount,
                            total = (item.detail.Price * item.detail.SaleCount).ToString(),
                            rowno = idx,
                            counter = item.detail.SectionCode,
                            comcode = item.detail.ProdSaleCode,
                            memo = string.Empty,
                            storeno
                        });
                        idx += 1;
                    }
                }
                dynamic head = new
                {
                    id,
                    mainid = _saleOrder.OrderNo,
                    flag = 0,
                    createtime = order.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    paytime = trans.trans.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    type = Type,
                    status = Status,
                    quantity = _saleOrder.SalesCount,
                    discount = "0",
                    total = _saleOrder.SalesAmount.ToString(),
                    vipno = string.Empty,
                    vipmemo = string.Empty,
                    storeno,
                    source = "MNY", //和许昶约定迷你银的source为MNY wallace 2014-12-23
                    comcount = idx - 1,
                    paycount = idx - 1,
                    oldid = string.Empty,
                    operid = string.Empty,
                    opername = string.Empty,
                    opertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") 
                };
                return new
                {
                    id,
                    status,
                    head,
                    detail,
                    payment,
                };
            }
        }

        public abstract SaleOrderNotificationStatus Status { get; }

        public abstract SaleOrderNotificationType Type { get; }

        public abstract string PaymentType { get; }
    }

    public enum OrderProductType
    {
        /// <summary>
        /// 单品系统商品
        /// </summary>
        System = 1,

        /// <summary>
        /// 自拍商品订单
        /// </summary>
        SelfProduct = 2,
    }
}