using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Security.Cryptography;
using AutoMapper;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Support
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        #region methods

        /// <summary>
        /// Order 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Order, bool>> OrderFiller(SearchStatRequest filter)
        {
            var query = PredicateBuilder.True<Order>();

            if (filter == null)
            {
                return query;
            }

            //订单号与时间互斥
            if (!String.IsNullOrWhiteSpace(filter.OrderNo))
            {
                query = PredicateBuilder.And(query, v => v.OrderNo == filter.OrderNo);
                return query;
            }


            if (filter.StartDate != null)
            {
                query = PredicateBuilder.And(query, v => v.CreateDate >= filter.StartDate);
            }

            if (filter.EndDate != null)
            {
                query = PredicateBuilder.And(query, v => v.CreateDate < filter.EndDate);
            }

            return query;
        }

        /// <summary>
        /// SaleDetailFiller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<OPC_SaleDetail, bool>> SaleDetailFiller(SearchStatRequest filter)
        {
            var query = PredicateBuilder.True<OPC_SaleDetail>();

            if (filter == null)
            {
                return query;
            }

            if (!String.IsNullOrWhiteSpace(filter.SalesOrderNo))
            {
                query = PredicateBuilder.And(query, v => v.SaleOrderNo == filter.SalesOrderNo);
            }


            return query;
        }

        /// <summary>
        /// OrderTransactionFiller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<OrderTransaction, bool>> OrderTransactionFiller(SearchStatRequest filter)
        {
            var query = PredicateBuilder.True<OrderTransaction>();

            if (filter == null)
            {
                return query;
            }

            if (!String.IsNullOrWhiteSpace(filter.OrderChannelNo))
            {
                query = PredicateBuilder.And(query, v => v.TransNo == filter.OrderChannelNo);
            }


            var c = filter as SearchCashierRequest;
            if (c != null)
            {
                //TODO: 确认下 客户端传来的是 code 还是name
                if (!String.IsNullOrWhiteSpace(c.PayType))
                    query = PredicateBuilder.And(query, v => v.PaymentCode == c.PayType);
            }

            return query;
        }

        /// <summary>
        /// StoreFiller 包含了权限判断
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Store, bool>> StoreFiller(SearchStatRequest filter)
        {
            var query = PredicateBuilder.True<Store>();

            if (filter == null)
            {
                return query;
            }

            if (filter.StoreId != null)
            {
                query = PredicateBuilder.And(query, v => v.Id == filter.StoreId);
            }

            if (filter.DataRoleStores != null && filter.StoreId == null)
            {
                query = PredicateBuilder.And(query, v => filter.DataRoleStores.Contains(v.Id));
            }

            return query;
        }


        /// <summary>
        /// 销售单 统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagerInfo<SaleDetailStatDto> GetPagedList4SaleStat(SearchStatRequest request)
        {
            using (var db = new YintaiHZhouContext())
            {
                var orderFilter = OrderFiller(request);
                var storeFilter = StoreFiller(request);
                var orderTransactionFilter = OrderTransactionFiller(request);
                var saleDetailFilter = SaleDetailFiller(request);

                var stores = db.Stores;
                var sections = db.Sections;
                var orderItems = db.OrderItems;
                var saleDetails = db.OPC_SaleDetails;
                var orderTransactions = db.OrderTransactions;
                var saleorders = db.OPC_Sales;
                var paymentMethods = db.PaymentMethods;

                var storesections = from store in stores.AsExpandable().Where(storeFilter)
                                    from section in sections
                                    where store.Id == section.StoreId
                                    select new
                                    {
                                        store,
                                        section
                                    };


                var query2 = saleDetails.AsExpandable().Where(saleDetailFilter).Join(orderItems, t => t.OrderItemId, o => o.Id,
                    (t, o) => new { OrderItem = o, SaleDetail = t });

                var filter = from q in query2
                             join so in saleorders on q.SaleDetail.SaleOrderNo equals so.SaleOrderNo
                             join o in db.Orders.AsExpandable().Where(orderFilter) on q.OrderItem.OrderNo equals o.OrderNo //into order
                             join b in db.Brands on q.OrderItem.BrandId equals b.Id //into cs
                             join s in storesections on so.SectionId equals s.section.Id //into store
                             let trans = (from tran in orderTransactions.AsExpandable().Where(orderTransactionFilter)
                                          join payment in paymentMethods on tran.PaymentCode equals payment.Code
                                          where tran.OrderNo == so.OrderNo
                                          select new
                                          {
                                              tran,
                                              payment
                                          }
                                       )
                             join ot in orderTransactions.AsExpandable().Where(orderTransactionFilter).Select(v => v.OrderNo).Distinct() on so.OrderNo equals ot
                             select
                                 new
                                 {
                                     Order = o,
                                     q.OrderItem,
                                     Brand = b,
                                     Stroe = s.store,
                                     q.SaleDetail,
                                     trans
                                 };

                var totalCount = filter.Count();

                var lst = filter.OrderByDescending(v => v.Order.CreateDate).Skip(request.PagerRequest.SkipCount).Take(request.PagerRequest.PageSize).ToList();

                var lstDto = new List<SaleDetailStatDto>();

                foreach (var o in lst)
                {
                    var defTrans = o.trans.ToList().OrderByDescending(v => v.tran.Amount).ThenByDescending(v => v.tran.PaymentCode).FirstOrDefault();
                    var dto = new SaleDetailStatDto
                    {
                        OrderItemId = o.OrderItem.Id,
                        Brand = o.Brand == null ? String.Empty : o.Brand.Name,
                        Color = o.OrderItem.ColorValueName,
                        LabelPrice = o.OrderItem.UnitPrice.HasValue ? o.OrderItem.UnitPrice.Value : 0,
                        BuyDate = o.Order.CreateDate,
                        OrderNo = o.Order.OrderNo,
                        OrderSouce = o.Order.OrderSource,
                        OrderTransFee = o.Order.ShippingFee,
                        PaymentMethodName = defTrans == null ? String.Empty : defTrans.payment.Name,
                        SalePrice = o.OrderItem.ItemPrice,
                        SaleTotalPrice = o.OrderItem.ExtendPrice,
                        SectionCode = o.SaleDetail.SectionCode,
                        SellCount = o.SaleDetail.SaleCount,
                        Size = o.OrderItem.SizeValueName,
                        StoreName = o.Stroe == null ? "" : o.Stroe.Name,
                        StyleNo = o.OrderItem.StoreItemNo,
                        SalesCode = o.SaleDetail.ProdSaleCode,
                        SalesOrderNo = o.SaleDetail.SaleOrderNo,
                        OrderChannelNo = defTrans == null ? String.Empty : defTrans.tran.TransNo
                    };

                    lstDto.Add(dto);
                }

                return new PagerInfo<SaleDetailStatDto>(request.PagerRequest, totalCount) { Datas = lstDto };
            }
        }

        /// <summary>
        /// RMA 统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagerInfo<ReturnGoodsStatDto> GetPagedList4RmaStat(SearchStatRequest request)
        {
            using (var db = new YintaiHZhouContext())
            {
                var orderFilter = OrderFiller(request);
                var storeFilter = StoreFiller(request);
                var orderTransactionFilter = OrderTransactionFiller(request);
                var saleDetailFilter = SaleDetailFiller(request);

                var stores = db.Stores;
                var sections = db.Sections;
                var orderItems = db.OrderItems;
                var saleDetails = db.OPC_SaleDetails;
                var orderTransactions = db.OrderTransactions;
                var saleorders = db.OPC_Sales;
                var rmadetails = db.OPC_RMADetails;
                var rmas = db.OPC_RMAs;
                var orders = db.Orders;
                var brands = db.Brands;
                var paymentMethods = db.PaymentMethods;

                var storesections = from store in stores.AsExpandable().Where(storeFilter)
                                    from section in sections
                                    where store.Id == section.StoreId
                                    select new
                                    {
                                        store,
                                        section
                                    };

                var queryOrder = orderItems
                        .Join(rmadetails, t => t.Id, o => o.OrderItemId,
                            (t, o) => new { OrderItem = t, RmaDetail = o });

                var query2 = saleDetails.AsExpandable().Where(saleDetailFilter).Join(queryOrder, t => t.OrderItemId, o => o.OrderItem.Id,
                    (t, o) => new { o.OrderItem, SaleDetail = t, o.RmaDetail });

                var filter = from q in query2
                             join so in saleorders on q.SaleDetail.SaleOrderNo equals so.SaleOrderNo
                             join ot in orderTransactions.AsExpandable().Where(orderTransactionFilter).Select(v => v.OrderNo).Distinct() on so.OrderNo equals ot
                             let trans = (from tran in orderTransactions
                                          join payment in paymentMethods on tran.PaymentCode equals payment.Code
                                          where tran.OrderNo == so.OrderNo
                                          select new
                                          {
                                              tran,
                                              payment
                                          }
)
                             join o in orders.AsExpandable().Where(orderFilter) on q.OrderItem.OrderNo equals o.OrderNo //into order
                             join b in brands on q.OrderItem.BrandId equals b.Id //into cs
                             join s in storesections on so.SectionId equals s.section.Id //into store
                             join r in rmas on q.RmaDetail.RMANo equals r.RMANo //into saleRma
                             select
                                 new
                                 {
                                     Rma = r,
                                     q.RmaDetail,
                                     Order = o,
                                     q.OrderItem,
                                     Brand = b,
                                     Stroe = s.store,
                                     q.SaleDetail,
                                     trans
                                 };

                var totalCount = filter.Count();

                var lst = filter.OrderByDescending(v => v.Order.CreateDate).Skip(request.PagerRequest.SkipCount).Take(request.PagerRequest.PageSize).ToList();

                var lstDto = new List<ReturnGoodsStatDto>();

                foreach (var o in lst)
                {
                    var defTrans = o.trans.ToList().OrderByDescending(v => v.tran.Amount).ThenByDescending(v => v.tran.PaymentCode).FirstOrDefault();

                    var dto = new ReturnGoodsStatDto
                    {
                        OrderItemId = o.OrderItem.Id,
                        Brand = o.Brand == null ? String.Empty : o.Brand.Name,
                        Color = o.OrderItem.ColorValueName,
                        LabelPrice = o.OrderItem.UnitPrice.HasValue ? o.OrderItem.UnitPrice.Value : 0,
                        BuyDate = o.Order.CreateDate,
                        OrderNo = o.Order.OrderNo,
                        OrderSouce = o.Order.OrderSource,
                        OrderTransFee = o.Order.ShippingFee,
                        PaymentMethodName = defTrans == null ? String.Empty : defTrans.payment.Name,
                        SalePrice = o.OrderItem.ItemPrice,
                        ApplyRmaDate = o.RmaDetail.CreatedDate,
                        RMANo = o.RmaDetail.RMANo,
                        ReturnGoodsCount = o.RmaDetail.BackCount,
                        RmaAmount = o.RmaDetail.Amount
                    };
                    dto.OrderTransFee = o.Order.ShippingFee;
                    dto.RmaDate = o.Rma.CreatedDate;
                    dto.SectionCode = o.SaleDetail.SectionCode;
                    dto.RmaPrice = o.RmaDetail.Price;
                    dto.Size = o.OrderItem.SizeValueName;
                    dto.StoreName = o.Stroe.Name;
                    dto.StyleNo = o.OrderItem.StoreItemNo;

                    dto.SalesCode = o.SaleDetail.ProdSaleCode;
                    //dto.SalesOrderNo = o.SaleDetail.SaleOrderNo;
                    dto.RmaStatusName = ((EnumRMAStatus)o.Rma.Status).GetDescription();
                    dto.SalesOrderNo = o.Rma.SaleOrderNo;
                    dto.OrderChannelNo = defTrans == null ? String.Empty : defTrans.tran.TransNo;


                    lstDto.Add(dto);
                }

                return new PagerInfo<ReturnGoodsStatDto>(request.PagerRequest, totalCount) { Datas = lstDto };
            }
        }

        /// <summary>
        /// 收银统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagerInfo<WebSiteCashierSearchDto> GetPagedList4CashierStat(SearchCashierRequest request)
        {
            using (var db = new YintaiHZhouContext())
            {
                int cahStatus = EnumCashStatus.CashOver.AsId();

                var orderFilter = OrderFiller(request);
                var storeFilter = StoreFiller(request);
                var orderTransactionFilter = OrderTransactionFiller(request);
                var saleDetailFilter = SaleDetailFiller(request);

                var stores = db.Stores;
                var sections = db.Sections;
                var orderItems = db.OrderItems;
                var saleDetails = db.OPC_SaleDetails;
                var orderTransactions = db.OrderTransactions;
                var rmas = db.OPC_RMAs;
                var rmadetails = db.OPC_RMADetails;
                var salesorder = db.OPC_Sales;

                var paymentMethods = db.PaymentMethods;

                //var storesections = from store in stores.AsExpandable().Where(storeFilter)
                //                    join  section in sections on store.Id equals section.StoreId
                //                    select new
                //                    {
                //                        store,
                //                        section
                //                    };

                //var querySale = salesorder.Where(
                //        t =>
                //            t.CashStatus == cahStatus && t.SectionId.HasValue)
                //        .Join(saleDetails.AsExpandable().Where(saleDetailFilter), t => t.SaleOrderNo, o => o.SaleOrderNo,
                //            (t, o) => new { Sale = t, SaleDetail = o });

                //var query2 = querySale.Join(orderItems, t => t.SaleDetail.OrderItemId, o => o.Id,
                //    (t, o) => new { OrderItem = o, t.SaleDetail, t.Sale });

                //var rmaquery = from rmadetail in rmadetails
                //               from rma in rmas.Where(v => v.RMACashStatus == cahStatus)
                //               where rmadetail.RMANo == rma.RMANo
                //               select new
                //               {
                //                   rma,
                //                   rmadetail
                //               };

                var filter = from q in ((salesorder.Where(
                        t =>
                            t.CashStatus == cahStatus && t.SectionId.HasValue)
                        .Join(saleDetails.AsExpandable().Where(saleDetailFilter), t => t.SaleOrderNo, o => o.SaleOrderNo,
                            (t, o) => new { Sale = t, SaleDetail = o })).Join(orderItems, t => t.SaleDetail.OrderItemId, o => o.Id,
                    (t, o) => new { OrderItem = o, t.SaleDetail, t.Sale }))
                             join o in db.Orders.AsExpandable().Where(orderFilter) on q.OrderItem.OrderNo equals o.OrderNo //into order
                             join b in db.Brands on q.OrderItem.BrandId equals b.Id //into cs
                             join s in
                                 (
                                     from store in stores.AsExpandable().Where(storeFilter)
                                     join section in sections on store.Id equals section.StoreId
                                     select new
                                     {
                                         store,
                                         section
                                     }
                                     ) on q.Sale.SectionId equals s.section.Id //into store
                             join r in
                                 (
                                     from rmadetail in rmadetails
                                     from rma in rmas.Where(v => v.RMACashStatus == cahStatus)
                                     where rmadetail.RMANo == rma.RMANo
                                     select new
                                     {
                                         rma,
                                         rmadetail
                                     }
                                     ) on q.OrderItem.Id equals r.rmadetail.OrderItemId into rmaDetails
                             from r in rmaDetails.DefaultIfEmpty()
                             let trans = (from tran in orderTransactions
                                          join payment in paymentMethods on tran.PaymentCode equals payment.Code
                                          where tran.OrderNo == o.OrderNo
                                          select new
                                          {
                                              tran,
                                              payment
                                          }
                                                                  )
                             join ot in orderTransactions.AsExpandable().Where(orderTransactionFilter).Select(v => v.OrderNo).Distinct() on o.OrderNo equals ot
                             orderby q.OrderItem.OrderNo, q.Sale.SaleOrderNo
                             select
                                 new
                                 {
                                     RmaAndDetails = r,
                                     q.Sale,
                                     Order = o,
                                     q.OrderItem,
                                     Brand = b,
                                     Stroe = s.store,
                                     q.SaleDetail,
                                     opts = trans
                                 };
                IQueryable<dynamic> lst;
                int totalCount;
                if (!String.IsNullOrWhiteSpace(request.FinancialType))
                {
                    if (String.Compare(request.FinancialType, DefinitionField.Rma, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        filter = filter.Where(v => v.RmaAndDetails != null);
                    }
                    else if (String.Compare(request.FinancialType, DefinitionField.Sales, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        filter = filter.Where(v => v.RmaAndDetails == null);
                    }
                }

                totalCount = filter.Count();
                var rst = filter.OrderByDescending(v => v.Order.CreateDate).Skip(request.PagerRequest.SkipCount).Take(request.PagerRequest.PageSize).ToList();
                var lstDto = new List<WebSiteCashierSearchDto>();

                foreach (var o in rst)
                {
                    var defTrans = o.opts.ToList().OrderByDescending(v => v.tran.Amount).ThenByDescending(v => v.tran.PaymentCode).FirstOrDefault();
                    var dto = new WebSiteCashierSearchDto
                    {
                        OrderItemId = o.OrderItem.Id,
                        Brand = o.Brand == null ? "" : o.Brand.Name,
                        Color = o.OrderItem.ColorValueName,
                        LabelPrice = o.OrderItem.UnitPrice ?? 0m,
                        BuyDate = o.Order.CreateDate,
                        OrderNo = o.Order.OrderNo,
                        OrderSouce = o.Order.OrderSource,
                        CashNum = o.Sale.CashNum,
                        PaymentMethodName = defTrans == null ? String.Empty : defTrans.payment.Name,
                        SalePrice = o.OrderItem.ItemPrice,
                        Count = o.OrderItem.Quantity,
                        SectionCode = o.SaleDetail.SectionCode,
                        Size = o.OrderItem.SizeValueName,
                        StoreName = o.Stroe.Name,
                        StyleNo = o.OrderItem.StoreItemNo,
                        SalesOrderNo = o.SaleDetail.SaleOrderNo,
                        SaleTotalPrice = o.Sale.SalesAmount,
                        SalesCode = o.SaleDetail.ProdSaleCode,
                        OrderChannelNo = defTrans == null ? String.Empty : defTrans.tran.TransNo
                    };

                    if (o.RmaAndDetails == null)
                    {
                        dto.DetailType = "销售单";
                    }
                    else
                    {
                        dto.RmaCashNum = o.RmaAndDetails.rma.RmaCashNum;
                        dto.DetailType = "退货单";
                    }

                    lstDto.Add(dto);
                }
                return new PagerInfo<WebSiteCashierSearchDto>(request.PagerRequest, totalCount) { Datas = lstDto };
            }
        }

        #endregion


        #region IOrderItemRepository Members

        public PageResult<OrderItemDto> GetByOrderNo(string orderNo, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                IQueryable<OrderItem> query = db.OrderItems.Where(t => t.OrderNo == orderNo);

                IQueryable<OPC_RMADetail> query2 = db.OrderItems.Where(t => t.OrderNo == orderNo)
                    .Join(db.OPC_RMADetails, t => t.Id, o => o.OrderItemId, (t, o) => o);
                var filter = from q in query
                             join b in db.Brands on q.BrandId equals b.Id into cs
                             join rmaDetail in query2 on q.Id equals rmaDetail.OrderItemId into rma
                             select new { Order = q, Brand = cs.FirstOrDefault(), Rma = rma.FirstOrDefault() };
                filter = filter.OrderByDescending(t => t.Order.CreateDate);
                var list = filter.ToPageResult(pageIndex, pageSize);
                IList<OrderItemDto> lstDtos = new List<OrderItemDto>();
                foreach (var t in list.Result)
                {
                    OrderItemDto o = Mapper.Map<OrderItem, OrderItemDto>(t.Order);
                    o.BrandName = t.Brand == null ? "" : t.Brand.Name;
                    if (t.Rma != null)
                    {
                        o.NeedReturnCount = t.Order.Quantity - t.Rma.BackCount;
                        o.ReturnCount = t.Rma.BackCount;
                    }
                    lstDtos.Add(o);
                }
                return new PageResult<OrderItemDto>(lstDtos, list.TotalCount);
            }
        }

        public IList<OrderItem> GetByIDs(IEnumerable<int> ids)
        {
            return Select(t => ids.Contains(t.Id));
        }



        /// <summary>
        /// 销售单
        /// </summary>
        /// <param name="request"></param>
        public PagedSaleDetailStatListDto WebSiteStatSaleDetailPaged(SearchStatRequest request)
        {
            var paged = GetPagedList4SaleStat(request);

            var result = new PagedSaleDetailStatListDto(request.PagerRequest, paged.TotalCount, paged.Datas);

            return result;
        }

        /// <summary>
        /// RMA
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedReturnGoodsStatListDto WebSiteStatReturnGoodsPaged(SearchStatRequest request)
        {
            var paged = GetPagedList4RmaStat(request);

            var result = new PagedReturnGoodsStatListDto(request.PagerRequest, paged.TotalCount, paged.Datas);

            return result;
        }

        /// <summary>
        /// 收银
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagedCashierList WebSiteCashierPaged(SearchCashierRequest request)
        {
            var paged = GetPagedList4CashierStat(request);

            var result = new PagedCashierList(request.PagerRequest, paged.TotalCount, paged.Datas);

            return result;
        }

        /// <summary>
        /// 销售单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("已过期请使用GetPagedList4SaleStat")]
        public SaleDetailStatListDto WebSiteStatSaleDetail(SearchStatRequest request)
        {
            using (var db = new YintaiHZhouContext())
            {
                //IQueryable<OPC_SaleDetail> query =
                //    db.OPC_SaleDetails.Where(t => t.CreatedDate >= request.StartTime && t.CreatedDate < request.EndTime );
                //IQueryable<OrderItem> queryOrder =
                //    db.OrderItems.Where(t => t.CreateDate >= request.StartTime && t.CreateDate < request.EndTime);

                var orderFilter = OrderFiller(request);
                var storeFilter = StoreFiller(request);
                var orderTransactionFilter = OrderTransactionFiller(request);
                var saleDetailFilter = SaleDetailFiller(request);

                var stores = db.Stores;
                //var sections = db.Sections;
                var orderItems = db.OrderItems;
                var saleDetails = db.OPC_SaleDetails;
                var orderTransactions = db.OrderTransactions;

                var storeQuery = from store in stores.AsExpandable().Where(storeFilter)
                                 //from section in sections
                                 //where store.Id == section.StoreId
                                 select new
                                 {
                                     store,
                                     //section
                                 };


                var query2 = saleDetails.AsExpandable().Where(saleDetailFilter).Join(orderItems, t => t.OrderItemId, o => o.Id,
                    (t, o) => new { OrderItem = o, SaleDetail = t });


                var filter = from q in query2
                             join o in db.Orders.AsExpandable().Where(orderFilter) on q.OrderItem.OrderNo equals o.OrderNo //into order
                             join b in db.Brands on q.OrderItem.BrandId equals b.Id //into cs
                             join s in storeQuery on q.OrderItem.StoreId equals s.store.Id //into store
                             join t in orderTransactions.AsExpandable().Where(orderTransactionFilter) on o.OrderNo equals t.OrderNo
                             select
                                 new
                                 {
                                     Order = o,
                                     q.OrderItem,
                                     Brand = b,
                                     Stroe = s.store,
                                     q.SaleDetail,
                                     t
                                 };
                var lst = filter.ToList();

                var lstDto = new SaleDetailStatListDto();

                foreach (var o in lst)
                {
                    var dto = new SaleDetailStatDto
                    {
                        OrderItemId = o.OrderItem.Id,
                        Brand = o.Brand == null ? "" : o.Brand.Name,
                        Color = o.OrderItem.ColorValueName,
                        LabelPrice = o.OrderItem.UnitPrice.HasValue ? o.OrderItem.UnitPrice.Value : 0,
                        BuyDate = o.Order.CreateDate,
                        OrderNo = o.Order.OrderNo,
                        OrderSouce = o.Order.OrderSource,
                        OrderTransFee = o.Order.ShippingFee,
                        PaymentMethodName = o.Order.PaymentMethodName,
                        SalePrice = o.OrderItem.ItemPrice,
                        SaleTotalPrice = o.OrderItem.ExtendPrice,
                        SectionCode = o.SaleDetail.SectionCode,
                        SellCount = o.SaleDetail.SaleCount,
                        Size = o.OrderItem.SizeValueName,
                        StoreName = o.Stroe == null ? "" : o.Stroe.Name,
                        StyleNo = o.OrderItem.StoreItemNo,
                        SalesCode = o.SaleDetail.ProdSaleCode,
                        SalesOrderNo = o.SaleDetail.SaleOrderNo,
                        OrderChannelNo = o.t.TransNo
                    };

                    lstDto.Add(dto);
                }
                return lstDto;
            }
        }

        [Obsolete("已过期请使用GetPagedList4RmaStat")]
        public ReturnGoodsStatListDto WebSiteStatReturnGoods(SearchStatRequest request)
        {
            using (var db = new YintaiHZhouContext())
            {
                //IQueryable<OPC_SaleDetail> query =
                //    db.OPC_SaleDetails.Where(t => t.CreatedDate >= request.StartTime && t.CreatedDate < request.EndTime);


                var orderFilter = OrderFiller(request);
                var storeFilter = StoreFiller(request);
                var orderTransactionFilter = OrderTransactionFiller(request);
                var saleDetailFilter = SaleDetailFiller(request);

                var stores = db.Stores;
                //var sections = db.Sections;
                var orderItems = db.OrderItems;
                var saleDetails = db.OPC_SaleDetails;
                var orderTransactions = db.OrderTransactions;

                var storeQuery = from store in stores.AsExpandable().Where(storeFilter)
                                 //from section in sections
                                 //where store.Id == section.StoreId
                                 select new
                                 {
                                     store,
                                     //section
                                 };

                var queryOrder =
                    orderItems
                        .Join(db.OPC_RMADetails, t => t.Id, o => o.OrderItemId,
                            (t, o) => new { OrderItem = t, RmaDetail = o });

                var query2 = saleDetails.AsExpandable().Where(saleDetailFilter).Join(queryOrder, t => t.OrderItemId, o => o.OrderItem.Id,
                    (t, o) => new { o.OrderItem, SaleDetail = t, o.RmaDetail });

                var filter = from q in query2
                             join o in db.Orders.AsExpandable().Where(orderFilter) on q.OrderItem.OrderNo equals o.OrderNo //into order
                             join b in db.Brands on q.OrderItem.BrandId equals b.Id //into cs
                             join s in storeQuery on q.OrderItem.StoreId equals s.store.Id //into store
                             join r in db.OPC_RMAs on q.RmaDetail.RMANo equals r.RMANo //into saleRma
                             join t in orderTransactions.Where(orderTransactionFilter) on o.OrderNo equals t.OrderNo
                             select
                                 new
                                 {
                                     Rma = r,
                                     q.RmaDetail,
                                     Order = o,
                                     q.OrderItem,
                                     Brand = b,
                                     Stroe = s.store,
                                     q.SaleDetail,
                                     t
                                 };
                var lst = filter.ToList();

                var lstDto = new ReturnGoodsStatListDto();

                foreach (var o in lst)
                {
                    var dto = new ReturnGoodsStatDto
                    {
                        OrderItemId = o.OrderItem.Id,
                        Brand = o.Brand == null ? "" : o.Brand.Name,
                        Color = o.OrderItem.ColorValueName,
                        LabelPrice = o.OrderItem.UnitPrice.HasValue ? o.OrderItem.UnitPrice.Value : 0,
                        BuyDate = o.Order.CreateDate,
                        OrderNo = o.Order.OrderNo,
                        OrderSouce = o.Order.OrderSource,
                        OrderTransFee = o.Order.ShippingFee,
                        PaymentMethodName = o.Order.PaymentMethodName,
                        SalePrice = o.OrderItem.ItemPrice,
                        ApplyRmaDate = o.RmaDetail.CreatedDate,
                        RMANo = o.RmaDetail.RMANo,
                        ReturnGoodsCount = o.RmaDetail.BackCount
                    };
                    dto.OrderTransFee = o.Order.ShippingFee;
                    dto.RmaDate = o.Rma.CreatedDate;
                    dto.SectionCode = o.SaleDetail.SectionCode;
                    dto.RmaPrice = o.RmaDetail.Price;
                    dto.Size = o.OrderItem.SizeValueName;
                    dto.StoreName = o.Stroe.Name;
                    dto.StyleNo = o.OrderItem.StoreItemNo;

                    dto.SalesCode = o.SaleDetail.ProdSaleCode;
                    //dto.SalesOrderNo = o.SaleDetail.SaleOrderNo;
                    dto.RmaStatusName = ((EnumRMAStatus)o.RmaDetail.Status).GetDescription();
                    dto.SalesOrderNo = o.SaleDetail.SaleOrderNo;
                    dto.OrderChannelNo = o.t.TransNo;


                    lstDto.Add(dto);
                }
                return lstDto;
            }
        }

        [Obsolete("已过期请使用GetPagedList4CashierStat")]
        public CashierList WebSiteCashier(SearchCashierRequest request)
        {
            /*
             * DECLARE @p__linq__0 INT
SET @p__linq__0=10

SELECT [Extent3].*
FROM
[dbo].[OPC_Sale]AS
[Extent1] INNER JOIN[dbo].[OPC_SaleDetail]AS[Extent2]ON[Extent1].[SaleOrderNo]=
[Extent2].[SaleOrderNo]
INNER JOIN[dbo].[OrderItem]AS
[Extent3]ON[Extent2].[OrderItemId]=
[Extent3].[Id]

INNER JOIN[dbo].[Order]
AS[Extent4]ON
[Extent3].[OrderNo]=[Extent4].[OrderNo]
INNER JOIN[dbo].[Brand]AS
[Extent5]ON[Extent3].[BrandId]=[Extent5].[Id]
INNER JOIN[dbo].[Store]AS[Extent6]
ON[Extent3].[StoreId]=[Extent6].[Id]

LEFT OUTER JOIN[dbo].[OPC_RMADetail]
AS[Extent7]ON
[Extent3].[Id]=
[Extent7].[OrderItemId]

INNER JOIN[dbo].[OrderTransaction]AS
[Extent8]ON[Extent4].[OrderNo]=[Extent8].[OrderNo]

WHERE(( ([Extent1].[CashStatus] = @p__linq__0) AND(NOT([Extent1].[CashStatus]IS NULL OR @p__linq__0 IS NULL)))OR(
([Extent1].[CashStatus]IS NULL)AND(@p__linq__0 IS NULL)))AND([Extent1].[SectionId] IS NOT NULL)

--AND(CAST([Extent4].[CreateDate] AS datetime2)>=@p__linq__1)AND
--(CAST([Extent4].[CreateDate] AS datetime2)<@p__linq__2)
AND([Extent4].[PaymentMethodCode]=@p__linq__3)
AND(@p__linq__3 IS NOT NULL))AS[Project1]
ORDER BY[Project1].[OrderNo1]ASC,[Project1].[SaleOrderNo] ASC

             */



            using (var db = new YintaiHZhouContext())
            {
                int cahStatus = EnumCashStatus.CashOver.AsId();

                var orderFilter = OrderFiller(request);
                var storeFilter = StoreFiller(request);
                var orderTransactionFilter = OrderTransactionFiller(request);
                var saleDetailFilter = SaleDetailFiller(request);

                var stores = db.Stores;
                //var sections = db.Sections;
                var orderItems = db.OrderItems;
                var saleDetails = db.OPC_SaleDetails;
                var orderTransactions = db.OrderTransactions;

                var storeQuery = from store in stores.AsExpandable().Where(storeFilter)
                                 //from section in sections
                                 //where store.Id == section.StoreId
                                 select new
                                 {
                                     store,
                                     //section
                                 };

                var querySale =
                    db.OPC_Sales.Where(
                        t =>
                            t.CashStatus == cahStatus && t.SectionId.HasValue)
                        .Join(saleDetails.AsExpandable().Where(saleDetailFilter), t => t.SaleOrderNo, o => o.SaleOrderNo,
                            (t, o) => new { Sale = t, SaleDetail = o });



                var query2 = querySale.Join(orderItems, t => t.SaleDetail.OrderItemId, o => o.Id,
                    (t, o) => new { OrderItem = o, t.SaleDetail, t.Sale });

                var query3 = from r in db.OPC_RMADetails
                             join rma in db.OPC_RMAs on r.RMANo equals rma.RMANo
                             select new
                             {
                                 rma_details = r,
                                 rma
                             };

                var filter = from q in query2
                             join o in db.Orders.AsExpandable().Where(orderFilter) on q.OrderItem.OrderNo equals o.OrderNo //into order
                             join b in db.Brands on q.OrderItem.BrandId equals b.Id //into cs
                             join s in storeQuery on q.OrderItem.StoreId equals s.store.Id //into store
                             join r in query3 on q.OrderItem.Id equals r.rma_details.OrderItemId into rmas
                             from r in rmas.DefaultIfEmpty()
                             join t in orderTransactions.AsExpandable().Where(orderTransactionFilter) on o.OrderNo equals t.OrderNo
                             orderby q.OrderItem.OrderNo, q.Sale.SaleOrderNo
                             select
                                 new
                                 {
                                     RmaDetails = r.rma_details,
                                     rmacashno = r == null ? "" : r.rma.RmaCashNum,
                                     q.Sale,
                                     Order = o,
                                     q.OrderItem,
                                     Brand = b,
                                     Stroe = s.store,
                                     q.SaleDetail,
                                     OrderTransactions = t
                                 };
                dynamic lst;

                if (!String.IsNullOrWhiteSpace(request.FinancialType))
                {
                    if (String.Compare(request.FinancialType, DefinitionField.Rma, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        lst = filter.Where(v => v.RmaDetails != null).ToList();
                    }
                    else if (String.Compare(request.FinancialType, DefinitionField.Sales, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        lst = filter.Where(v => v.RmaDetails == null).ToList();
                    }
                    else
                    {
                        lst = filter.ToList();
                    }
                }
                else
                {
                    lst = filter.ToList();
                }

                var lstDto = new CashierList();

                foreach (var o in lst)
                {
                    var dto = new WebSiteCashierSearchDto
                    {
                        OrderItemId = o.OrderItem.Id,
                        Brand = o.Brand == null ? "" : o.Brand.Name,
                        Color = o.OrderItem.ColorValueName,
                        LabelPrice = o.OrderItem.UnitPrice,
                        BuyDate = o.Order.CreateDate,
                        OrderNo = o.Order.OrderNo,
                        OrderSouce = o.Order.OrderSource,
                        CashNum = o.Sale.CashNum,
                        PaymentMethodName = o.Order.PaymentMethodName,
                        SalePrice = o.OrderItem.ItemPrice,
                        Count = o.OrderItem.Quantity,
                        SectionCode = o.SaleDetail.SectionCode,
                        Size = o.OrderItem.SizeValueName,
                        StoreName = o.Stroe.Name,
                        StyleNo = o.OrderItem.StoreItemNo,
                        SalesOrderNo = o.SaleDetail.SaleOrderNo,
                        SaleTotalPrice = o.Sale.SalesAmount,
                        SalesCode = o.SaleDetail.ProdSaleCode,
                        OrderChannelNo = o.OrderTransactions.TransNo
                    };

                    if (o.RmaDetails == null)
                    {
                        dto.DetailType = "销售单";


                        // lstDto.Add(dto);
                    }
                    else
                    {
                        ////foreach (OPC_RMADetail rma in o.RmaDetails)
                        ////{
                        //var dto = new WebSiteCashierSearchDto();

                        //dto.Brand = o.Brand == null ? "" : o.Brand.Name;
                        //dto.Color = o.OrderItem.ColorValueName;
                        //dto.LabelPrice = o.OrderItem.UnitPrice;
                        //dto.BuyDate = o.Order.CreateDate;
                        //dto.OrderNo = o.Order.OrderNo;
                        //dto.OrderSouce = o.Order.OrderSource;
                        //dto.CashNum = o.Sale.CashNum;
                        //dto.PaymentMethodName = o.Order.PaymentMethodName;
                        //dto.SalePrice = o.OrderItem.ItemPrice;
                        //dto.Count = o.OrderItem.Quantity;
                        //dto.SectionCode = o.SaleDetail.SectionCode;

                        //dto.Size = o.OrderItem.SizeValueName;
                        //dto.StoreName = o.Stroe.Name;
                        //dto.StyleNo = o.OrderItem.StoreItemNo;
                        dto.RmaCashNum = o.rmacashno;
                        dto.DetailType = "退货单";

                        //}
                    }

                    lstDto.Add(dto);
                }
                return lstDto;
            }
        }

        public PageResult<OrderItemDto> GetOrderItemsAutoBack(string orderNo, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                IQueryable<OrderItem> query = db.OrderItems.Where(t => t.OrderNo == orderNo);

                var filter = from q in query
                             join b in db.Brands on q.BrandId equals b.Id into cs
                             select new { Order = q, Brand = cs.FirstOrDefault() };
                filter = filter.OrderByDescending(t => t.Order.CreateDate);
                var list = filter.ToPageResult(pageIndex, pageSize);
                IList<OrderItemDto> lstDtos = new List<OrderItemDto>();
                foreach (var t in list.Result)
                {
                    OrderItemDto o = Mapper.Map<OrderItem, OrderItemDto>(t.Order);
                    o.BrandName = t.Brand == null ? "" : t.Brand.Name;

                    o.NeedReturnCount = t.Order.Quantity;
                    o.ReturnCount = t.Order.Quantity;

                    lstDtos.Add(o);
                }
                return new PageResult<OrderItemDto>(lstDtos, list.TotalCount);
            }
        }

        public void SetSaleOrderVoid(string saleOrderNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                var sale = db.OPC_Sales.FirstOrDefault(t => t.SaleOrderNo == saleOrderNo);
                if (sale == null)
                {
                    throw new Exception("销售单不存在，销售单号:" + saleOrderNo);
                }
                sale.Status = EnumSaleOrderStatus.Void.AsId();

                db.OPC_Sales.AddOrUpdate(sale);

                db.SaveChanges();
            }
        }

        #endregion
    }
}