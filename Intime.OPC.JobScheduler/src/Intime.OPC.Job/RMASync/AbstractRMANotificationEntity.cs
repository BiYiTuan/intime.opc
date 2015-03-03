using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.RMASync
{
    public abstract class AbstractRMANotificationEntity
    {
        protected OPC_RMA _saleRMA;
        protected AbstractRMANotificationEntity(OPC_RMA saleRMA)
        {
            this._saleRMA = saleRMA;
        }

        public dynamic CreateNotifiedEntity()
        {
            using (var db = new YintaiHZhouContext())
            {
                var id = _saleRMA.RMANo;
                var status = (int)Status;

                var trans =
                    db.OrderTransactions.Where(t => t.OrderNo == _saleRMA.OrderNo)
                        .Join(db.PaymentMethods, t => t.PaymentCode, p => p.Code, (t, p) => new { trans = t, payment = p })
                        .FirstOrDefault();
                var saleRMA = db.OPC_RMA.FirstOrDefault(o => o.RMANo == _saleRMA.RMANo);
                var storeno = string.Empty;
                if (saleRMA == null)
                {
                    throw new OrderNotificationException(string.Format("Not exists RMA! RMANo ({0})", _saleRMA.RMANo));
                }

                if (trans == null)
                {
                    throw new OrderNotificationException(string.Format("RMA has no payment information ! RMANo ({0})", _saleRMA.RMANo));
                }

                var detail = new List<dynamic>();
                var payment = new List<dynamic>();
                int idx = 1;

                foreach (var de in db.OPC_RMADetail.Where(x => x.RMANo == _saleRMA.RMANo).Join(db.OPC_Stock, x => x.StockId, s => s.Id, (x, s) => new { detail = x, stock = s }))
                {
                    storeno = de.stock.StoreCode;
                    payment.Add(new
                    {
                        id = _saleRMA.RMANo,
                        type = PaymentType,
                        typeid = trans.payment.Code,    //
                        typename = trans.payment.Name,
                        no = trans.trans.TransNo,
                        amount = de.detail.Amount.ToString(),
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
                        quantity = de.detail.BackCount,
                        total = de.detail.Amount.ToString(),//(de.detail.Price * de.detail.SaleCount).ToString(),
                        rowno = idx,
                        counter = de.stock.SectionCode,
                        memo = string.Empty,
                        storeno = de.stock.StoreCode
                    });
                    idx += 1;
                }
                dynamic head = new
                {
                    id,
                    mainid = _saleRMA.RMANo,
                    flag = 0,
                    createtime = saleRMA.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),//order.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    paytime = trans.trans.CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    type = Type,
                    status = Status,
                    quantity = _saleRMA.Count,
                    discount = "0.0",
                    total = _saleRMA.RMAAmount.ToString(),
                    vipno = string.Empty,
                    vipmemo = string.Empty,
                    storeno,
                    comcount = idx - 1,
                    paycount = idx - 1,
                    oldid = string.Empty,
                    operid = string.Empty,
                    opername = string.Empty,
                    opertime = string.Empty
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

        public abstract NotificationStatus Status { get; }

        public abstract NotificationType Type { get; }

        public abstract string PaymentType { get; }
    }
}