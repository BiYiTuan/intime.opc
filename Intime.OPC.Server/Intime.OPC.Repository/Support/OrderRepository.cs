// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-25-2014 13:37:53
//
// Last Modified By : Liuyh
// Last Modified On : 03-25-2014 13:38:11
// ***********************************************************************
// <copyright file="OrderRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;
using Intime.OPC.Repository.Base;
using PagerRequest = Intime.OPC.Domain.PagerRequest;
using LinqKit;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Support
{
    /// <summary>
    ///     Class OrderRepository.
    /// </summary>
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private log4net.ILog _log = log4net.LogManager.GetLogger(typeof(OrderRepository));

        #region IOrderRepository Members

        public PageResult<Order> GetOrder(string orderNo, string orderSource, DateTime dtStart, DateTime dtEnd, int storeId,
            int brandId, int status, string paymentType, string outGoodsType, string shippingContactPhone,
            string expressDeliveryCode, int expressDeliveryCompany, int pageIndex, int pageSize = 20)
        {
            using (var db = new YintaiHZhouContext())
            {

                var query = db.Orders.Where(t => t.CreateDate >= dtStart && t.CreateDate < dtEnd);
                if (!string.IsNullOrWhiteSpace(orderNo))
                {
                    query = query.Where(t => t.OrderNo.Contains(orderNo));
                }
                if (!string.IsNullOrWhiteSpace(orderSource))
                {
                    query = query.Where(t => t.OrderSource == orderSource);
                }
                //if (brandId > 0)
                //{
                //    query = query.Where(t => t.BrandId == brandId);
                //}
                if (status > -1)
                {
                    query = query.Where(t => t.Status == status);
                }
                if (!string.IsNullOrWhiteSpace(paymentType))
                {
                    query = query.Where(t => t.PaymentMethodCode == paymentType);
                }
                //if (!string.IsNullOrWhiteSpace(outGoodsType))
                //{
                //    query = query.Where(t => t.PaymentMethodCode == outGoodsType);
                //}

                //if (!string.IsNullOrWhiteSpace(shippingContactPhone))
                //{
                //    query = query.Where(t => t.ShippingContactPhone == shippingContactPhone);
                //}
                if (!string.IsNullOrWhiteSpace(expressDeliveryCode))
                {
                    query = query.Where(t => t.ShippingNo == expressDeliveryCode);
                }
                //if (expressDeliveryCompany > -1)
                //{
                //    query = query.Where(t => t.ShippingVia == expressDeliveryCompany);
                //}
                //订单没有门店，需要用sale过滤 zxy 2014-6-7
                if (storeId > 0)
                {
                    var sales = db.OPC_Sales.Where(t => t.SellDate >= dtStart && t.SellDate < dtEnd)
                            .Join(
                                db.Sections.Where(
                                    s =>
                                        s.Status != -1 && s.StoreId.HasValue &&
                                        storeId == s.StoreId.Value), o => o.SectionId, s => s.Id,
                                (o, s) => o);
                    List<string> lstOrderNo = sales.Select(t => t.OrderNo).ToList();
                    query = query.Where(t => lstOrderNo.Contains(t.OrderNo));

                }

                query = query.OrderByDescending(t => t.CreateDate);
                return query.ToPageResult(pageIndex, pageSize);
            }
        }

        public Order GetOrderByOrderNo(string orderNo)
        {
            return Select(t => t.OrderNo == orderNo).FirstOrDefault();
        }

        public PageResult<Order> GetOrderByOderNoTime(string orderNo, DateTime starTime, DateTime endTime, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var filter = db.Orders.Where(t => t.CreateDate >= starTime && t.CreateDate < endTime);

                if (string.IsNullOrWhiteSpace(orderNo))
                {
                    filter = filter.Where(t => t.OrderNo.Contains(orderNo));
                }
                filter = filter.OrderByDescending(t => t.CreateDate);
                return filter.ToPageResult(pageIndex, pageSize);
            }
        }

        public PageResult<Order> GetOrderByShippingNo(string shippingNo, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var filter = db.OPC_ShippingSales.Where(t => t.ShippingCode == shippingNo).Join(db.Orders, t => t.OrderNo, o => o.OrderNo, (t, o) => o);
                filter = filter.OrderByDescending(t => t.CreateDate);
                return filter.ToPageResult(pageIndex, pageSize);
            }
        }

        public PageResult<Order> GetByReturnGoodsInfo(ReturnGoodsInfoRequest request)
        {
            //todo 查询所有有退货的订单
            using (var db = new YintaiHZhouContext())
            {
                var filter2 = db.OPC_RMAs.Where(t => t.CreatedDate >= request.StartDate && t.CreatedDate < request.EndDate); //&& t.RMAStatus==(int)EnumReturnGoodsStatus.NoProcess;

                var filter = db.Orders.Where(t => true);

                if (request.OrderNo.IsNotNull())
                {
                    filter = filter.Where(t => t.OrderNo == request.OrderNo);
                }
                if (request.PayType.IsNotNull())
                {
                    filter = filter.Where(t => t.PaymentMethodCode == request.PayType);
                }
                if (request.SaleOrderNo.IsNotNull())
                {
                    filter = filter.Join(db.OPC_Sales.Where(t => t.SaleOrderNo == request.SaleOrderNo),
                        t => t.OrderNo, o => o.OrderNo, (t, o) => t);
                }


                if (request.RmaNo.IsNotNull())
                {
                    filter2 = filter2.Where(t => t.RMANo == request.RmaNo);
                }

                if (request.RmaStatus.HasValue && request.RmaStatus != -1)
                {
                    filter2 = filter2.Where(t => t.Status == request.RmaStatus.Value);

                }

                if (request.StoreID.HasValue)
                {
                    filter2 = filter2.Where(t => t.StoreId == request.StoreID.Value);
                }

                var orderIds = filter2.ToList().Select(t => t.OrderNo).Distinct().ToList();

                filter = filter.Where(t => orderIds.Contains(t.OrderNo)).OrderByDescending(t => t.CreateDate);

                return filter.ToPageResult(request.pageIndex, request.pageSize);
            }
        }

        public PageResult<Order> GetBySaleRma(ReturnGoodsInfoRequest request, int? rmaStatus, EnumReturnGoodsStatus returnGoodsStatus)
        {
            using (var db = new YintaiHZhouContext())
            {


                var filter2 = db.OPC_RMAs.Where(t => t.CreatedDate >= request.StartDate && t.CreatedDate < request.EndDate && t.RMAStatus == (int)returnGoodsStatus);

                if (rmaStatus.HasValue)
                {
                    filter2 = filter2.Where(t => t.Status == rmaStatus.Value);
                }

                var filter = db.Orders.Where(t => true);

                if (CurrentUser != null)
                {
                    filter = filter.Where(t => CurrentUser.StoreIds.Contains(t.StoreId));
                }

                if (request.OrderNo.IsNotNull())
                {
                    filter = filter.Where(t => t.OrderNo.Contains(request.OrderNo));
                }
                if (request.PayType.IsNotNull())
                {
                    filter = filter.Where(t => t.PaymentMethodCode == request.PayType);
                }
                if (request.SaleOrderNo.IsNotNull())
                {
                    filter = filter.Join(db.OPC_Sales.Where(t => t.SaleOrderNo.Contains(request.SaleOrderNo)),
                        t => t.OrderNo, o => o.OrderNo, (t, o) => t);
                }


                if (request.RmaNo.IsNotNull())
                {
                    filter2 = filter2.Where(t => t.RMANo == request.RmaNo);
                }

                if (request.RmaStatus.HasValue)
                {
                    filter2 = filter2.Where(t => t.Status == request.RmaStatus.Value);

                }

                if (request.StoreID.HasValue)
                {
                    filter2 = filter2.Where(t => t.StoreId == request.StoreID.Value);
                }

                var orderIds = filter2.ToList().Select(t => t.OrderNo).Distinct().ToList();

                filter = filter.Where(t => orderIds.Contains(t.OrderNo)).OrderByDescending(t => t.CreateDate);

                return filter.ToPageResult(request.pageIndex, request.pageSize);
            }
        }

        public PageResult<Order> GetByOutOfStockNotify(OutOfStockNotifyRequest request, int orderstatus)
        {
            using (var db = new YintaiHZhouContext())
            {
                var filter2 = db.OPC_Sales.Where(t => true);

                var filter = db.Orders.Where(t => t.CreateDate >= request.StartDate && t.CreateDate < request.EndDate && t.Status == orderstatus);
                if (request.OrderNo.IsNotNull())
                {
                    filter2 = filter2.Where(t => t.OrderNo == request.OrderNo);
                    filter = filter.Where(t => t.OrderNo == request.OrderNo);
                }

                if (request.SaleOrderStatus.HasValue)
                {
                    filter2 = filter2.Where(t => t.Status == request.SaleOrderStatus.Value);
                }

                if (request.PayType.IsNotNull())
                {
                    filter = filter.Where(t => t.PaymentMethodCode == request.PayType);
                }

                if (request.SaleOrderStatus.HasValue)
                {
                    filter2 = filter2.Where(t => t.Status == request.SaleOrderStatus.Value);
                }



                if (request.StoreId.HasValue)
                {
                    filter = filter.Where(t => t.StoreId == request.StoreId.Value);
                }
                filter = Queryable.Join(filter, filter2, t => t.OrderNo, o => o.OrderNo, (t, o) => t);

                filter = filter.OrderByDescending(t => t.CreateDate);

                return filter.ToPageResult(request.pageIndex, request.pageSize);
            }
        }

        public OrderModel GetItemByOrderNo(string orderno)
        {
            using (var db = new YintaiHZhouContext())
            {
                var orders = db.Orders;
                var orderitems = db.OrderItems;
                var orderTransactions = db.OrderTransactions;
                var shipvia = db.ShipVias;

                var paymentMethods = db.PaymentMethods;

                var otps = from tran in orderTransactions
                           join payment in paymentMethods on tran.PaymentCode equals payment.Code
                           select new
                           {
                               tran,
                               payment
                           };

                var q = from o in orders.Where(v => v.OrderNo == orderno)
                        let trans = (from trans in otps
                                     where o.OrderNo == trans.tran.OrderNo
                                     select trans
                                      )
                        join via in shipvia on o.ShippingVia equals via.Id into tmp2
                        from via in tmp2.DefaultIfEmpty()
                        from oi in
                            (from oi in orderitems
                             where o.OrderNo == oi.OrderNo
                             group oi by oi.OrderNo into g
                             select new
                             {
                                 g.Key,
                                 Count = g.Sum(s => s.Quantity)
                             }

                                             )
                        where o.OrderNo == oi.Key
                        select new
                        {
                            o,
                            trans,
                            via,
                            count = oi.Count
                        };

                return q.Select(d => new OrderModel
                 {
                     BuyDate = d.o.CreateDate,
                     CustomerAddress = d.o.ShippingAddress,
                     CustomerFreight = 0m,
                     CustomerName = d.o.ShippingContactPerson,
                     CustomerPhone = d.o.ShippingContactPhone,
                     //CustomerRemark = o.Memo,
                     ExpressCompany = d.via == null ? String.Empty : d.via.Name,
                     ExpressNo = d.o.ShippingNo,
                     Id = d.o.Id,
                     IfReceipt = d.o.NeedInvoice,
                     MustPayTotal = d.o.TotalAmount,
                     //OrderChannelNo = d.ot != null ? d.ot.TransNo : String.Empty,
                     OrderNo = d.o.OrderNo,
                     OrderSouce = d.o.OrderSource,
                     //OutGoodsDate = 
                     //OutGoodsType = 
                     //PaymentMethodName = d.o.PaymentMethodName,
                     PostCode = d.o.ShippingZipCode,
                     //Quantity = o.
                     ReceiptContent = d.o.InvoiceDetail,
                     ReceiptHead = d.o.InvoiceSubject,
                     ShippingNo = d.o.ShippingNo,
                     Status = d.o.Status,
                     TotalAmount = d.o.TotalAmount,
                     Quantity = d.count,

                     OrderTransactionModels = d.trans.Select(s => new OrderTransactionModel
                     {
                         PaymentCode = s.payment.Code,
                         PaymentName = s.payment.Name,
                         TransNo = s.tran.TransNo,
                         Amount = s.tran.Amount
                     }),
                 }).FirstOrDefault();
            }
        }

        public PagerInfo<OrderModel> GetPagedList(PagerRequest pagerRequest, OrderQueryRequest request, OrderSortOrder sortOrder)
        {
            var orderFilter = OrderFiller(request);
            var storeFilter = StoreFiller(request);
            var transFilter = OrderTransactionFiller(request);

            List<OrderModel> datas = null;
            int totalCount = 0;
            using (var db = new YintaiHZhouContext())
            {
                var stores = db.Stores;
                var sections = db.Sections;
                var orders = db.Orders;
                var sales = db.OPC_Sales;
                var orderitems = db.OrderItems;
                var orderTransactions = db.OrderTransactions;
                var shipvia = db.ShipVias;

                var paymentMethods = db.PaymentMethods;

                var q = from o in orders.AsExpandable().Where(orderFilter)
                        join ooo in
                            ((
                            from so in sales
                            from store in stores.AsExpandable().Where(storeFilter)
                            from section in sections
                            where so.SectionId == section.Id && store.Id == section.StoreId
                            select so.OrderNo
                            ).Distinct()) on o.OrderNo equals ooo

                        let trans = (from tran in orderTransactions//.Where(transFilter)
                                     join payment in paymentMethods on tran.PaymentCode equals payment.Code

                                     where o.OrderNo == tran.OrderNo
                                     select new
                                     {

                                         tran,
                                         payment
                                     }
                                     )
                        //from ot in tmp1.DefaultIfEmpty()
                        join via in shipvia on o.ShippingVia equals via.Id into tmp2
                        from via in tmp2.DefaultIfEmpty()
                        from oi in
                            (from oi in orderitems
                             where o.OrderNo == oi.OrderNo
                             group oi by oi.OrderNo into g
                             select new
                             {
                                 g.Key,
                                 Count = g.Sum(s => s.Quantity)
                             }

                                             )
                        join ot in orderTransactions.AsExpandable().Where(transFilter).Select(v=>v.OrderNo).Distinct() on o.OrderNo equals ot
                        where o.OrderNo == oi.Key 
                        select new
                        {
                            o,
                            otps = trans,
                            via,
                            count = oi.Count
                        };

                totalCount = q.Count();
                var rst =
                    q.OrderByDescending(v => v.o.CreateDate)
                        .Skip(pagerRequest.SkipCount)
                        .Take(pagerRequest.PageSize);

                datas = rst.Select(d => new OrderModel
                {
                    BuyDate = d.o.CreateDate,
                    CustomerAddress = d.o.ShippingAddress,
                    CustomerFreight = 0m,
                    CustomerName = d.o.ShippingContactPerson,
                    CustomerPhone = d.o.ShippingContactPhone,
                    //CustomerRemark = o.Memo,
                    ExpressCompany = d.via == null ? String.Empty : d.via.Name,
                    ExpressNo = d.o.ShippingNo,
                    Id = d.o.Id,
                    IfReceipt = d.o.NeedInvoice,
                    MustPayTotal = d.o.TotalAmount,
                    //OrderChannelNo = d.ot != null ? d.ot.TransNo : String.Empty,
                    OrderNo = d.o.OrderNo,
                    OrderSouce = d.o.OrderSource,
                    //OutGoodsDate = 
                    //OutGoodsType = 
                    //PaymentMethodName = d.o.PaymentMethodName,
                    PostCode = d.o.ShippingZipCode,
                    //Quantity = o.
                    ReceiptContent = d.o.InvoiceDetail,
                    ReceiptHead = d.o.InvoiceSubject,
                    ShippingNo = d.o.ShippingNo,
                    Status = d.o.Status,
                    TotalAmount = d.o.TotalAmount,
                    Quantity = d.count,

                    OrderTransactionModels = d.otps.Select(s => new OrderTransactionModel
                    {
                        PaymentCode = s.payment.Code,
                        PaymentName = s.payment.Name,
                        TransNo = s.tran.TransNo,
                        Amount = s.tran.Amount
                    }),
                }).ToList();
            }

            return new PagerInfo<OrderModel>(pagerRequest, totalCount) { Datas = datas };
        }


        /// <summary>
        /// 业务上 不关联 销售单（不管是否拆分销售单）
        /// </summary>
        /// <param name="pagerRequest"></param>
        /// <param name="request"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public PagerInfo<OrderModel> GetPagedListExcludeSalesOrder(PagerRequest pagerRequest, OrderQueryRequest request, OrderSortOrder sortOrder)
        {
            var orderFilter = OrderFiller(request);
            //var storeFilter = StoreFiller(request);
            var transFilter = OrderTransactionFiller(request);
            List<OrderModel> datas = null;
            int totalCount = 0;
            using (var db = new YintaiHZhouContext())
            {
                var orders = db.Orders;
                var orderitems = db.OrderItems;
                var orderTransactions = db.OrderTransactions;
                var shipvia = db.ShipVias;

                var paymentMethods = db.PaymentMethods;

                var q = from o in orders.AsExpandable().Where(orderFilter)
                        let trans = (from tran in orderTransactions
                                     join payment in paymentMethods on tran.PaymentCode equals payment.Code

                                     where o.OrderNo == tran.OrderNo
                                     select new
                                     {

                                         tran,
                                         payment
                                     }
             )
                        join via in shipvia on o.ShippingVia equals via.Id into tmp2
                        from via in tmp2.DefaultIfEmpty()
                        from oi in
                            (from oi in orderitems
                             where o.OrderNo == oi.OrderNo
                             group oi by oi.OrderNo into g
                             select new
                             {
                                 g.Key,
                                 Count = g.Sum(s => s.Quantity)
                             }

                                             )
                        join ot in orderTransactions.AsExpandable().Where(transFilter).Select(v => v.OrderNo).Distinct() on o.OrderNo equals ot
                        where o.OrderNo == oi.Key
                        select new
                        {
                            o,
                            //so,
                            //store,
                            //section,
                            trans,
                            via,
                            count = oi.Count
                        };

                totalCount = q.Count();

                var rst =
                     q.OrderByDescending(v => v.o.CreateDate)
                         .Skip(pagerRequest.SkipCount)
                         .Take(pagerRequest.PageSize);

                datas = rst.Select(d => new OrderModel
                {
                    BuyDate = d.o.CreateDate,
                    CustomerAddress = d.o.ShippingAddress,
                    CustomerFreight = 0m,
                    CustomerName = d.o.ShippingContactPerson,
                    CustomerPhone = d.o.ShippingContactPhone,
                    //CustomerRemark = o.Memo,
                    ExpressCompany = d.via == null ? String.Empty : d.via.Name,
                    ExpressNo = d.o.ShippingNo,
                    Id = d.o.Id,
                    IfReceipt = d.o.NeedInvoice,
                    MustPayTotal = d.o.TotalAmount,
                    //OrderChannelNo = d.ot != null ? d.ot.TransNo : String.Empty,
                    OrderNo = d.o.OrderNo,
                    OrderSouce = d.o.OrderSource,
                    //OutGoodsDate = 
                    //OutGoodsType = 
                    //PaymentMethodName = d.o.PaymentMethodName,
                    PostCode = d.o.ShippingZipCode,
                    //Quantity = o.
                    ReceiptContent = d.o.InvoiceDetail,
                    ReceiptHead = d.o.InvoiceSubject,
                    ShippingNo = d.o.ShippingNo,
                    Status = d.o.Status,
                    TotalAmount = d.o.TotalAmount,
                    Quantity = d.count,

                    OrderTransactionModels = d.trans.Select(s => new OrderTransactionModel
                    {
                        PaymentCode = s.payment.Code,
                        PaymentName = s.payment.Name,
                        TransNo = s.tran.TransNo,
                        Amount = s.tran.Amount
                    }),
                }).ToList();
            }

            return new PagerInfo<OrderModel>(pagerRequest, totalCount) { Datas = datas };
        }

        #endregion

        #region methods

        private static Expression<Func<OrderTransaction, bool>> OrderTransactionFiller(OrderQueryRequest filter)
        {
            var query = PredicateBuilder.True<OrderTransaction>();

            if (filter != null)
            {
                if (!String.IsNullOrWhiteSpace(filter.PaymentType))
                {
                    //TODO: 确认下 客户端传来的是 code 还是 name
                    query = PredicateBuilder.And(query, v => v.PaymentCode == filter.PaymentType);
                }
            }

            return query;
        }

        /// <summary>
        /// 订单 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Order, bool>> OrderFiller(OrderQueryRequest filter)
        {
            var query = PredicateBuilder.True<Order>();

            if (filter != null)
            {
                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }

                if (!String.IsNullOrWhiteSpace(filter.OrderNo))
                {
                    query = PredicateBuilder.And(query, v => v.OrderNo == filter.OrderNo);

                    return query;
                }

                if (!String.IsNullOrWhiteSpace(filter.ExpressDeliveryCode))
                {
                    query = PredicateBuilder.And(query, v => v.ShippingNo == filter.ExpressDeliveryCode);

                    return query;
                }

                if (!String.IsNullOrWhiteSpace(filter.ShippingNo))
                {
                    query = PredicateBuilder.And(query, v => v.ShippingNo == filter.ShippingNo);

                    return query;
                }

                if (!String.IsNullOrWhiteSpace(filter.OrderSource))
                {
                    query = PredicateBuilder.And(query, v => v.OrderSource == filter.OrderSource);
                }

                if (!String.IsNullOrWhiteSpace(filter.ShippingContactPhone))
                {
                    query = PredicateBuilder.And(query, v => v.ShippingContactPhone == filter.ShippingContactPhone);
                }

                if (filter.ExpressDeliveryCompany != null)
                {
                    query = PredicateBuilder.And(query, v => v.ShippingVia == filter.ExpressDeliveryCompany);
                }

                if (filter.StartCreateDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate >= filter.StartCreateDate);
                }

                if (filter.EndCreateDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate < filter.EndCreateDate);
                }
            }

            return query;
        }

        /// <summary>
        /// 门店 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Store, bool>> StoreFiller(OrderQueryRequest filter)
        {
            var query = PredicateBuilder.True<Store>();

            if (filter != null)
            {
                if (filter.StoreId != null)
                {
                    query = PredicateBuilder.And(query, v => v.Id == filter.StoreId);
                }

                if (filter.DataRoleStores != null && filter.StoreId == null)
                {
                    query = PredicateBuilder.And(query, v => filter.DataRoleStores.Contains(v.Id));
                }
            }

            return query;
        }

        #endregion
    }
}