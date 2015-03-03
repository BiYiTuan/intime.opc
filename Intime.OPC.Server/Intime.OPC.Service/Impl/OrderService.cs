using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;
using System.Linq;


namespace Intime.OPC.Service.Impl
{
    public class OrderService
    {
        public OrderDto GetOrder(string orderNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                var trade =
                    db.Orders.Where(x => x.OrderNo == orderNo)
                        .Join(db.OrderTransactions, o => o.OrderNo, ot => ot.OrderNo,
                            (o, ot) => new {order = o, trans = ot}).Join(db.PaymentMethods,o=>o.trans.PaymentCode,s=>s.Code,(o,p)=>new{o=o,p})
                        .GroupJoin(db.OPC_Sales.Where(x => x.OrderNo == orderNo), x => x.o.order.OrderNo, s => s.OrderNo,
                            (o, s) =>
                                new
                                {
                                    order = o.o.order,
                                    trans = o.o.trans,
                                    payment = o.p,
                                    sale = s.OrderByDescending(t => t.Status).FirstOrDefault()
                                }).FirstOrDefault();
                if (trade == null)
                {
                    return null;
                }
                return new OrderDto()
                {
                    BuyDate = trade.order.CreateDate,
                    CustomerAddress =  trade.order.ShippingAddress,
                    CustomerFreight = trade.sale.ShippingFee.HasValue?trade.sale.ShippingFee.Value:(decimal)0.0,
                    CustomerName = trade.order.ShippingContactPerson,
                    CustomerPhone = trade.order.ShippingContactPhone,
                    CustomerRemark = trade.order.Memo,
                    PaymentMethodName = trade.payment.Name,
                    OrderSouce = trade.order.OrderSource,
                    //Status = trade.order.Status,
                    TotalAmount = trade.order.TotalAmount,
                    MustPayTotal = trade.order.TotalAmount + (trade.sale.ShippingFee.HasValue?trade.sale.ShippingFee.Value:(decimal)0.0),
                    IfReceipt = trade.order.NeedInvoice.HasValue && trade.order.NeedInvoice.Value?"是":"否",
                    ReceiptHead = trade.order.InvoiceSubject,
                    ReceiptContent = trade.order.InvoiceDetail,
                    //OutGoodsType = trade.sale.ShipViaId,
                    PostCode  = trade.order.ShippingZipCode,
                    ShippingNo  = trade.sale.ShippingCode,
                    ExpressNo = trade.sale.ShippingCode,
                    //OutGoodsDate = trade.sale
                };
            }
        }
    }
}
