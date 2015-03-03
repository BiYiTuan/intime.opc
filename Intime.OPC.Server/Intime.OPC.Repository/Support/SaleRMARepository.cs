using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using System;
using System.Linq;

namespace Intime.OPC.Repository.Support
{
    public class SaleRMARepository : BaseRepository<OPC_SaleRMA>, ISaleRMARepository
    {
        public int Count(string orderNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                return db.OPC_SaleRMAs.Count(t => t.OrderNo == orderNo);
            }
        }

        public PageResult<SaleRmaDto> GetAll(string orderNo, string payType, int? bandId, DateTime startTime, DateTime endTime, string telephone, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var query = db.OPC_RMAs.Where(t => t.CreatedDate >= startTime && t.CreatedDate < endTime && CurrentUser.StoreIds.Contains(t.StoreId));

                var query2 = db.Orders.Where(t => t.CreateDate >= startTime && t.CreateDate < endTime).Join(db.OrderTransactions, o => o.OrderNo, t => t.OrderNo, (o, t) => new
                {
                    Order = o,
                    Trans = t
                });

                if (!string.IsNullOrWhiteSpace(orderNo))
                {
                    query = query.Where(t => t.OrderNo == orderNo);
                    query2 = query2.Where(t => t.Order.OrderNo == orderNo);
                }

                if (!string.IsNullOrWhiteSpace(payType))
                {
                    query2 = query2.Where(t => t.Order.PaymentMethodCode == payType);
                }

                var q = from t in query2
                        join o in query on t.Order.OrderNo equals o.OrderNo into cs
                        select new { SaleRMA = cs.FirstOrDefault(), Orders = t };
                q = q.OrderByDescending(t => t.Orders.Order.OrderNo);
                var lst = q.ToPageResult(pageIndex, pageSize);
                var lstSaleRma = lst.Result.Select(t => CreateSaleRmaDto(t.Orders.Order,t.Orders.Trans,t.SaleRMA)).ToList();
                return new PageResult<SaleRmaDto>(lstSaleRma, lst.TotalCount);
            }
        }

        private SaleRmaDto CreateSaleRmaDto(Order order, OrderTransaction trans, OPC_RMA rma)
        {
            var o = new SaleRmaDto
            {
                    Id = order.Id,
                OrderChannelNo = trans.TransNo,
                CustomerAddress = order.ShippingAddress,
                CustomerName = order.ShippingContactPerson,
                CustomerPhone = order.ShippingContactPhone,
                IfReceipt = order.NeedInvoice.HasValue && order.NeedInvoice.Value,
                MustPayTotal = (double)order.TotalAmount,
                OrderNo = order.OrderNo,
                PaymentMethodName = order.PaymentMethodName,
                ReceiptContent = order.InvoiceDetail,
                ReceiptHead = order.InvoiceSubject,
                OrderSource = order.OrderSource,
                OrderTransFee = order.ShippingFee,
                BuyDate = order.CreateDate
            };

            if (rma != null)
            {
                o.CustomFee = rma.CustomFee;
                o.RealRMASumMoney = rma.RealRMASumMoney;
                o.RecoverableSumMoney = rma.RecoverableSumMoney;
                o.RealRMASumMoney = rma.RealRMASumMoney;
                o.SaleOrderNo = rma.SaleOrderNo;
                o.StoreFee = rma.StoreFee;
                o.ServiceAgreeDate = rma.CreatedDate;
                o.CustomerRemark = rma.Reason;
                o.RmaNo = rma.RMANo;
                o.CreateDate = rma.CreatedDate;
                o.RMACount = rma.Count.HasValue ? rma.Count.Value :0;
                o.CompensationFee = rma.CompensationFee;

            }

            return o;
        }

        public PageResult<SaleRmaDto> GetAll(string orderNo, string saleOrderNo, string payType, string rmaNo, DateTime startTime, DateTime endTime,
            int? rmaStatus, int? storeId, EnumReturnGoodsStatus returnGoodsStatus, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var query =
                    db.OPC_RMAs.Where(
                        t =>
                            t.CreatedDate >= startTime && t.CreatedDate < endTime &&
                            CurrentUser.StoreIds.Contains(t.StoreId));
                var query2 = db.Orders.Where(t => true).Join(db.OrderTransactions, o => o.OrderNo, t => t.OrderNo, (o, t) => new
                {
                    Order = o,
                    o.OrderNo,
                    Trans = t
                }); 
                if (!string.IsNullOrWhiteSpace(orderNo))
                {
                    query = query.Where(t => t.OrderNo == orderNo);
                    query2 = query2.Where(t => t.OrderNo == orderNo);
                }

                if (!string.IsNullOrWhiteSpace(saleOrderNo))
                {
                    query = query.Where(t => t.SaleOrderNo == saleOrderNo);
                }

                if (!string.IsNullOrWhiteSpace(rmaNo))
                {
                    query = query.Where(t => t.RMANo == rmaNo);
                }

                if (returnGoodsStatus != EnumReturnGoodsStatus.NoProcess)
                {
                    query = query.Where(t => t.RMAStatus == (int)returnGoodsStatus);
                }
                if (rmaStatus.HasValue && rmaStatus.Value != -1)
                {
                    query = query.Where(t => t.Status == rmaStatus.Value);
                }

                if (!string.IsNullOrWhiteSpace(payType))
                {
                    query2 = query2.Where(t => t.Order.PaymentMethodCode == payType);
                }

                if (storeId.HasValue)
                {
                    query2 = query2.Where(t => t.Order.StoreId == storeId.Value);
                }

                var lst = query.Join(query2, t => t.OrderNo, o => o.OrderNo, (t, o) => new { SaleRMA = t, Orders = o }).OrderByDescending(t => t.Orders.Order.CreateDate).ToPageResult(pageIndex, pageSize);

                var lstSaleRma = lst.Result.Select(t => CreateSaleRmaDto(t.Orders.Order, t.Orders.Trans, t.SaleRMA)).ToList();
                return new PageResult<SaleRmaDto>(lstSaleRma, lst.TotalCount);
            }
        }
        /// <summary>
        /// 付款确认
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="saleOrderNo"></param>
        /// <param name="payType"></param>
        /// <param name="rmaNo"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="storeId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PageResult<SaleRmaDto> GetByReturnGoodPay(string orderNo, string saleOrderNo, string payType, string rmaNo, DateTime startTime, DateTime endTime,
             int? storeId, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                //收银状态 未送收银
                //系统商品要送收银的，非系统商品要未送收银的 wallace 
                var query = db.OPC_RMAs.Where(t => t.RMACashStatus == (int)EnumRMACashStatus.CashOver && t.CreatedDate >= startTime && t.CreatedDate < endTime && CurrentUser.StoreIds.Contains(t.StoreId)).Join(db.Orders.Where(o=>o.OrderProductType.HasValue && o.OrderProductType.Value == 1),r=>r.OrderNo,o=>o.OrderNo,(r,o)=>r);
                var query1 = db.OPC_RMAs.Where(t => t.CreatedDate >= startTime && t.CreatedDate < endTime && CurrentUser.StoreIds.Contains(t.StoreId)).Join(db.Orders.Where(o=>o.OrderProductType.HasValue && o.OrderProductType.Value == 2),r=>r.OrderNo,o=>o.OrderNo,(r,o)=>r);
                query = query.Union(query1);

                var query2 = db.Orders.Where(t => true).Join(db.OrderTransactions, o => o.OrderNo, t => t.OrderNo, (o, t) => new
                {
                    Order = o,
                    o.OrderNo,
                    Trans = t
                });
                if (!string.IsNullOrWhiteSpace(orderNo))
                {
                    query = query.Where(t => t.OrderNo == orderNo);
                    query2 = query2.Where(t => t.OrderNo == orderNo);
                }

                if (!string.IsNullOrWhiteSpace(saleOrderNo))
                {
                    query = query.Where(t => t.SaleOrderNo == saleOrderNo);
                }

                if (!string.IsNullOrWhiteSpace(rmaNo))
                {
                    query = query.Where(t => t.RMANo == rmaNo);
                }
                //退货单状态 物流入库+通知单品    zxy 2014-5-10
                //2014-5-28  zxy 按与晓华和韩璐讨论结果，修改为 完成打印退货单状态 或者导购确认收货
                //query = query.Where(t => (t.Status == (int)EnumRMAStatus.ShipInStorage || t.Status == (int)EnumRMAStatus.NotifyProduct));
                query = query.Where(t => (t.Status == (int)EnumRMAStatus.PrintRMA || t.Status == (int)EnumRMAStatus.ShoppingGuideReceive));


                if (!string.IsNullOrWhiteSpace(payType))
                {
                    query2 = query2.Where(t => t.Order.PaymentMethodCode == payType);
                }

                if (storeId.HasValue)
                {
                    query2 = query2.Where(t => t.Order.StoreId == storeId.Value);
                }

                var lst = query.Join(query2, t => t.OrderNo, o => o.OrderNo, (t, o) => new { SaleRMA = t, Orders = o }).OrderByDescending(t => t.Orders.Order.CreateDate).ToPageResult(pageIndex, pageSize);

                var lstSaleRma = lst.Result.Select(t => CreateSaleRmaDto(t.Orders.Order, t.Orders.Trans, t.SaleRMA)).ToList();
                return new PageResult<SaleRmaDto>(lstSaleRma, lst.TotalCount);
            }
        }
        public OPC_SaleRMA GetByRmaNo(string rmaNo)
        {
            return Select(t => t.RMANo == rmaNo).FirstOrDefault();
        }

        public PageResult<SaleRmaDto> GetOrderAutoBack(ReturnGoodsRequest request)
        {
            CheckUser();
            using (var db = new YintaiHZhouContext())
            {
                var query = db.OPC_RMAs.Where(t => true && CurrentUser.StoreIds.Contains(t.StoreId));
//                var query2 = db.Orders.Where(t => CurrentUser.StoreIds.Contains(t.StoreId) && t.CreateDate >= request.StartDate && t.CreateDate < request.EndDate).Join(db.OrderTransactions, o => o.OrderNo, t => t.OrderNo, (o, t) => new
                var query2 = db.Orders.Where(t => t.CreateDate >= request.StartDate && t.CreateDate < request.EndDate).Join(db.OrderTransactions, o => o.OrderNo, t => t.OrderNo, (o, t) => new
                {
                    Order = o,
                    o.OrderNo,
                    Trans = t
                });
                var queryRMA = db.RMAs.Where(t => true);
                if (request.OrderNo.IsNotNull())
                {
                    query = query.Where(t => t.OrderNo == request.OrderNo);
                    query2 = query2.Where(t => t.OrderNo == request.OrderNo);
                    queryRMA = queryRMA.Where(t => t.OrderNo == request.OrderNo);
                }

                if (request.PayType.IsNotNull())
                {
                    query2 = query2.Where(t => t.Order.PaymentMethodCode == request.PayType);
                }

                var query3 = Queryable.Join(query2, queryRMA, t => t.OrderNo, o => o.OrderNo,
                     (t, o) => new { Order = t, RMA = o });

                var q = from t in query3
                        join o in query on t.Order.OrderNo equals o.OrderNo into cs
                        select new { SaleRMA = cs.FirstOrDefault(), Orders = t.Order, RMA = t.RMA };
                
                q = q.OrderByDescending(t => t.Orders.Order.CreateDate);
                var lst = q.ToPageResult(request.pageIndex, request.pageSize);
                var lstSaleRma = lst.Result.Select(t => CreateSaleRmaDto(t.Orders.Order, t.Orders.Trans, t.SaleRMA)).ToList();
                return new PageResult<SaleRmaDto>(lstSaleRma, lst.TotalCount);
            }
        }

        public void SetVoidBySaleOrder(string saleOrderNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                var lst = db.OPC_RMAs.Where(t => t.SaleOrderNo == saleOrderNo).ToList();
                foreach (var sale in lst)
                {
                    sale.Status = EnumRMAStatus.OutofStack.AsId();
                    sale.RMAStatus = (int)EnumReturnGoodsStatus.Valid;
                    sale.RMACashStatus = EnumCashStatus.CashOver.AsId();
                }
                db.SaveChanges();
            }
        }
    }
}