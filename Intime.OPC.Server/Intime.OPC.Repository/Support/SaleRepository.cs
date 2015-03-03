// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-19-2014 20:11:42
//
// Last Modified By : Liuyh
// Last Modified On : 03-24-2014 01:29:37
// ***********************************************************************
// <copyright file="SaleRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using AutoMapper;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using Intime.OPC.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Repository.Support
{
    /// <summary>
    ///     Class SaleRepository.
    /// </summary>
    public class SaleRepository : BaseRepository<OPC_Sale>, ISaleRepository
    {
        private readonly SalesOrderRepository _salesOrderRepository = new SalesOrderRepository();

        #region ISaleRepository Members

        /// <summary>
        ///     Selects this instance.
        /// </summary>
        /// <returns>IList{OPC_Sale}.</returns>
        public IList<OPC_Sale> Select()
        {
            using (var db = new YintaiHZhouContext())
            {
                if (CurrentUser != null)
                {
                    var sections = db.Stores.Where(s => CurrentUser.StoreIds.Contains(s.Id))
                        .Join(db.Sections, st => st.Id, se => se.StoreId, (st, se) => se);

                    return db.OPC_Sales.Join(sections, o => o.SectionId, s => s.Id, (o, s) => o).ToList();
                }
                List<OPC_Sale> saleList = db.OPC_Sales.ToList();
                return saleList;
            }
        }

        public bool UpdateSatus(string saleNo, EnumSaleOrderStatus saleOrderStatus, int userID)
        {
            using (var db = new YintaiHZhouContext())
            {
                OPC_Sale sale = db.OPC_Sales.FirstOrDefault(t => t.SaleOrderNo == saleNo);
                if (sale != null)
                {
                    sale.UpdatedDate = DateTime.Now;
                    sale.UpdatedUser = userID;
                    sale.Status = (int)saleOrderStatus;
                }

                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        ///     Gets the by sale no.
        /// </summary>
        /// <param name="saleNo">The sale no.</param>
        /// <returns>OPC_Sale.</returns>
        public OPC_Sale GetBySaleNo(string saleNo)
        {
            using (var db = new YintaiHZhouContext())
            {
                return db.OPC_Sales.FirstOrDefault(t => t.SaleOrderNo == saleNo);
            }
        }

        /// <summary>
        ///     Gets the sale order details.
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <returns>IList{OPC_SaleDetail}.</returns>
        public PageResult<SaleDetailDto> GetSaleOrderDetails(string saleOrderNo, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var query =
                    db.OPC_SaleDetails.Where(t => t.SaleOrderNo == saleOrderNo);

                var qq = from q in db.OrderItems
                         join b in db.Brands on q.BrandId equals b.Id into bb
                         join o in db.Orders on q.OrderNo equals o.OrderNo into oo

                         select new { OrderItems = q, Brand = bb.FirstOrDefault(), Order = oo.FirstOrDefault() };

                var filter = from q in query
                             join o in qq on q.OrderItemId equals o.OrderItems.Id into oo
                             join p in db.OPC_Stocks on q.StockId equals p.Id into pp

                             select new { OrderItem = oo.FirstOrDefault(), Sale = q, Stock = pp.FirstOrDefault() };

                var lst3 = filter.OrderByDescending(t => t.Sale.CreatedDate);
                var lst = lst3.ToPageResult(pageIndex, pageSize);
                var lstDto = new List<SaleDetailDto>();
                foreach (var t in lst.Result)
                {
                    SaleDetailDto o = Mapper.Map<OPC_SaleDetail, SaleDetailDto>(t.Sale);
                    if (t.OrderItem != null)
                    {

                        o.Color = t.OrderItem.OrderItems.ColorValueName;
                        o.Size = t.OrderItem.OrderItems.SizeValueName;
                        o.ProductNo = t.Sale.ProdSaleCode;
                        o.ProductName = t.OrderItem.OrderItems.ProductName;
                        if (t.OrderItem.Brand != null)
                        {
                            o.Brand = t.OrderItem.Brand.Name;
                        }
                    }
                    o.SectionCode = t.Sale.SectionCode;
                    o.StyleNo = t.OrderItem.OrderItems.StoreItemNo;
                    lstDto.Add(o);
                }
                return new PageResult<SaleDetailDto>(lstDto, lst.TotalCount);
            }
        }

        public PageResult<SaleDto> GetPickUped(string saleId, string orderNo, DateTime dtStart, DateTime dtEnd,
            int pageIndex, int pageSize, params int[] storeIds)
        {
            return getSalesData(saleId, orderNo, dtStart, dtEnd, EnumSaleOrderStatus.PickUp, pageIndex, pageSize,
                storeIds);
        }

        /// <summary>
        ///     获得已完成 打印销售单 的数据
        /// </summary>
        /// <param name="saleId">The sale identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        public PageResult<SaleDto> GetPrintSale(string saleId, string orderNo, DateTime dtStart, DateTime dtEnd,
            int pageIndex, int pageSize, params int[] StoreIds)
        {
            var saleOrderStatus = EnumSaleOrderStatus.PrintSale.AsId();
            var saleOrderStatus1 = EnumSaleOrderStatus.ShoppingGuidePickUp.AsId();
            using (var db = new YintaiHZhouContext())
            {
                //IQueryable<OPC_Sale> query = db.OPC_Sales.Where(t => (t.Status == saleOrderStatus || t.Status == saleOrderStatus1)
                //                                                    && t.SellDate >= dtStart
                //                                                    && t.SellDate < dtEnd);


                IQueryable<OPC_Sale> query =
                        db.OPC_Sales.Where(t => (t.Status == saleOrderStatus || t.Status == saleOrderStatus1)
                            && t.SellDate >= dtStart && t.SellDate < dtEnd)
                            .Join(
                                db.Sections.Where(
                                    s =>
                                        s.StoreId.HasValue &&
                                        StoreIds.Contains(s.StoreId.Value)), o => o.SectionId, s => s.Id,
                                (o, s) => o);

                if (!string.IsNullOrWhiteSpace(orderNo))
                {
                    query = query.Where(t => t.OrderNo == orderNo);
                }

                if (!string.IsNullOrWhiteSpace(saleId))
                {
                    query = query.Where(t => t.SaleOrderNo == saleId);
                }

                var stores = db.Stores.Where(x => StoreIds.Contains(x.Id));

                var ll = from s in db.Sections
                         join store in stores on s.StoreId equals store.Id into ss
                         select new { SectionID = s.Id, Section = s, Store = ss.FirstOrDefault() };

                var ll2 = from s in query
                          join l in ll on s.SectionId equals l.SectionID into ss
                          join o in db.Orders on s.OrderNo equals o.OrderNo into oo
                          join r in db.OrderTransactions on s.OrderNo equals r.OrderNo into rr
                          select new { Sale = s, Order = oo.FirstOrDefault(), Store = ss.FirstOrDefault(), OrderTrans = rr.FirstOrDefault() };

                //                ll2 = ll2.Where(x => x.Store != null);

                ll2 = ll2.OrderByDescending(t => t.Sale.CreatedDate);

                var lst = ll2.ToPageResult(pageIndex, pageSize);

                var lstDto = new List<SaleDto>();
                foreach (var t in lst.Result)
                {
                    var o = Mapper.Map<OPC_Sale, SaleDto>(t.Sale);
                    if (t.Store != null && t.Store.Store != null)
                    {
                        o.StoreName = t.Store.Store.Name;
                        o.StoreTelephone = t.Store.Store.Tel;
                        o.StoreAddress = t.Store.Store.Location;
                        o.SectionCode = t.Store.Section.SectionCode;

                    }


                    if (t.Store != null && t.Store.Section != null)
                    {
                        o.SectionName = t.Store.Section.Name;
                    }
                    if (t.OrderTrans != null)
                    {
                        o.TransNo = t.OrderTrans.TransNo;
                    }
                    o.ReceivePerson = t.Order.ShippingContactPerson;
                    o.OrderSource = t.Order.OrderSource;
                    o.InvoiceSubject = t.Order.InvoiceSubject;
                    o.PayType = t.Order.PaymentMethodName;
                    o.Invoice = t.Order.InvoiceDetail;
                    lstDto.Add(o);
                }
                return new PageResult<SaleDto>(lstDto, lst.TotalCount);
            }
        }

        /// <summary>
        ///     获得 未提货 的数据
        /// </summary>
        /// <param name="saleOrderNo">The sale identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        public PageResult<SaleDto> GetNoPickUp(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd,
            int pageIndex, int pageSize, params int[] storeIds)
        {
            int saleOrderStatus = EnumSaleOrderStatus.NotifyProduct.AsId();
            using (var db = new YintaiHZhouContext())
            {
                IQueryable<OPC_Sale> query =
                    db.OPC_Sales.Where(
                        t =>
                            (t.Status == saleOrderStatus || t.Status == (int)EnumSaleOrderStatus.NoPickUp ||
                             t.Status == (int)EnumSaleOrderStatus.Fetched)
                            && t.SellDate >= dtStart
                            && t.SellDate < dtEnd);

                if (!string.IsNullOrWhiteSpace(saleOrderNo))
                {
                    query = query.Where(t => t.SaleOrderNo == saleOrderNo);
                }
                else if (!string.IsNullOrWhiteSpace(orderNo))
                {
                    query = query.Where(t => t.OrderNo == orderNo);
                }

                var stores = from s in db.Stores
                             where storeIds.Contains(s.Id)
                             select s;

                var sections = from s in db.Sections
                               join r in stores on s.StoreId equals r.Id into uu
                               select s;

                var qq = from sale in query
                         join s in sections on sale.SectionId equals s.Id into cs
                         join r in db.OrderTransactions on sale.OrderNo equals r.OrderNo into rr
                         select new { Sale = sale, Section = cs.FirstOrDefault(), OrderTrans = rr.FirstOrDefault() };


                var filter = from q in qq
                             join s in stores on q.Section.StoreId equals s.Id into mm
                             join o in db.Orders on q.Sale.OrderNo equals o.OrderNo into oo
                             select new { q.Sale, q.Section, q.OrderTrans, Order = oo.FirstOrDefault(), Store = mm.FirstOrDefault() };


                filter = filter.Where(x => x.Store != null);

                filter = filter.OrderByDescending(t => t.Sale.CreatedDate);
                var lst = filter.ToPageResult(pageIndex, pageSize);
                var lstDto = new List<SaleDto>();
                foreach (var s in lst.Result)
                {
                    var o = Mapper.Map<OPC_Sale, SaleDto>(s.Sale);
                    if (s.Store != null)
                    {
                        o.StoreName = s.Store.Name;
                        o.StoreTelephone = s.Store.Tel;
                        o.StoreAddress = s.Store.Location;
                    }
                    if (s.Section != null)
                    {
                        o.SectionName = s.Section.Name;
                    }
                    if (s.OrderTrans != null)
                    {
                        o.TransNo = s.OrderTrans.TransNo;
                    }
                    o.ReceivePerson = s.Order.ShippingContactPerson;
                    o.OrderSource = s.Order.OrderSource;
                    o.InvoiceSubject = s.Order.InvoiceSubject;
                    o.PayType = s.Order.PaymentMethodName;
                    o.Invoice = s.Order.InvoiceDetail;
                    lstDto.Add(o);
                }
                return new PageResult<SaleDto>(lstDto, lst.TotalCount);

            }
        }

        /// <summary>
        ///     Updates the satus.
        /// </summary>
        /// <param name="saleNos">The sale nos.</param>
        /// <param name="saleOrderStatus">The sale order status.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool UpdateSatus(IEnumerable<string> saleNos, EnumSaleOrderStatus saleOrderStatus, int userID)
        {
            //todo 更新状态为实现
            throw new NotImplementedException("UpdateSatus 未实现");
            //using (var db = new YintaiHZhouContext())
            //{
            //    foreach (string saleNo in saleNos)
            //    {
            //        OPC_Sale sale = db.OPC_Sales.FirstOrDefault(t => t.SaleOrderNo == saleNo);
            //        if (sale != null)
            //        {
            //            sale.UpdatedDate = DateTime.Now;
            //            sale.UpdatedUser = userID;
            //            sale.Status = (int)saleOrderStatus;
            //        }
            //    }
            //    IQueryable<OPC_Sale> lst = db.OPC_Sales.Where(t => saleNos.Contains(t.SaleOrderNo));

            //    IEnumerable<OPC_Sale> lst2 = saleNos.Join(db.OPC_Sales, t => t, o => o.SaleOrderNo, (t, o) => o);

            //    db.SaveChanges();
            //    return true;
            //}
        }

        /// <summary>
        ///     Gets the print express.
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        public PageResult<SaleDto> GetPrintExpress(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd,
            int pageIndex, int pageSize, params int[] storeIds)
        {
            return getSalesData(saleOrderNo, orderNo, dtStart, dtEnd, EnumSaleOrderStatus.PrintExpress, pageIndex,
                pageSize, storeIds);
        }

        /// <summary>
        ///     Gets the print invoice.
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        public PageResult<SaleDto> GetPrintInvoice(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd,
            int pageIndex, int pageSize, params int[] sectionIds)
        {
            return getSalesData(saleOrderNo, orderNo, dtStart, dtEnd, EnumSaleOrderStatus.PrintExpress, pageIndex,
                pageSize, sectionIds);
        }

        public PageResult<SaleDto> GetShipInStorage(string saleOrderNo, string orderNo, DateTime dtStart,
            DateTime dtEnd, int pageIndex, int pageSize, params int[] sectionIds)
        {
            return getSalesData(saleOrderNo, orderNo, dtStart, dtEnd, EnumSaleOrderStatus.ShipInStorage, pageIndex,
                pageSize, sectionIds);
        }

        public IList<OPC_Sale> GetByOrderNo(string orderID, int sectinID)
        {
            if (sectinID > -1)
            {
                return Select(t => t.OrderNo == orderID && t.SectionId == sectinID);
            }
            return Select(t => t.OrderNo == orderID);
        }

        public IList<SaleDto> GetByOrderNo2(string orderID)
        {
            using (var db = new YintaiHZhouContext())
            {
                var query = db.OPC_Sales.Where(t => t.OrderNo == orderID);

                var qq = from sale in query
                         join s in db.Sections on sale.SectionId equals s.Id into cs
                         join r in db.OrderTransactions on sale.OrderNo equals r.OrderNo into rr
                         select new { Sale = sale, Section = cs.FirstOrDefault(), OrderTrans = rr.FirstOrDefault() };

                var uu = from o in db.Orders
                         select new { OrderNo = o.OrderNo, Order = o };

                var filter = from q in qq
                             join s in db.Stores on q.Section.StoreId equals s.Id
                             join o in uu on q.Sale.OrderNo equals o.OrderNo into oo

                             select new { Sale = q.Sale, Section = q.Section, OrderTrans = q.OrderTrans, Order = oo.FirstOrDefault().Order, Store = s };


                var lst = filter.ToList();
                var lstDto = new List<SaleDto>();
                foreach (var s in lst)
                {
                    var o = Mapper.Map<OPC_Sale, SaleDto>(s.Sale);
                    if (s.Store != null)
                    {
                        o.StoreName = s.Store.Name;
                        o.StoreTelephone = s.Store.Tel;
                        o.StoreAddress = s.Store.Location;
                    }
                    if (s.Section != null)
                    {
                        o.SectionName = s.Section.Name;
                    }

                    if (s.OrderTrans != null)
                    {
                        o.TransNo = s.OrderTrans.TransNo;
                    }

                    o.ReceivePerson = s.Order.ShippingContactPerson;
                    o.OrderSource = s.Order.OrderSource;
                    o.InvoiceSubject = s.Order.InvoiceSubject;
                    o.PayType = s.Order.PaymentMethodName;
                    o.Invoice = s.Order.InvoiceDetail;
                    lstDto.Add(o);
                }
                return lstDto;
            }
        }

        public PageResult<SaleDto> GetShipped(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd,
            int pageIndex, int pageSize, params int[] sectionIds)
        {
            return getSalesData(saleOrderNo, orderNo, dtStart, dtEnd, EnumSaleOrderStatus.Shipped, pageIndex, pageSize,
                sectionIds);
        }

        public IList<OPC_Sale> GetByShippingCode(string shippingCode)
        {
            var va = (int)(EnumSaleOrderStatus.Void);
            return Select(t => t.ShippingCode == shippingCode && t.Status > va);
        }

        #endregion

        /// <summary>
        ///     Gets the data.
        /// </summary>
        /// <param name="saleId">The sale identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <param name="saleOrderStatus">The sale order status.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        private PageResult<SaleDto> getSalesData(string saleId, string orderNo, DateTime dtStart, DateTime dtEnd,
            EnumSaleOrderStatus saleOrderStatus, int pageIndex, int pageSize, params int[] storeIds)
        {
            using (var db = new YintaiHZhouContext())
            {
                IQueryable<OPC_Sale> query = db.OPC_Sales.Where(t => t.Status == (int)saleOrderStatus
                                                                    && t.SellDate >= dtStart
                                                                    && t.SellDate < dtEnd);

                if (!string.IsNullOrWhiteSpace(orderNo))
                {
                    query = query.Where(t => t.OrderNo.Contains(orderNo));
                }

                if (!string.IsNullOrWhiteSpace(saleId))
                {
                    query = query.Where(t => t.SaleOrderNo.Contains(saleId));
                }


                var stores = db.Stores.Where(x => storeIds.Contains(x.Id));

                var ll = from s in db.Sections
                         join store in stores on s.StoreId equals store.Id into ss
                         select new { SectionID = s.Id, Section = s, Store = ss.FirstOrDefault() };

                var ll2 = from s in query
                          join l in ll on s.SectionId equals l.SectionID into ss
                          join o in db.Orders on s.OrderNo equals o.OrderNo into oo
                          join r in db.OrderTransactions on s.OrderNo equals r.OrderNo into rr

                          select new { Sale = s, Order = oo.FirstOrDefault(), Store = ss.FirstOrDefault(), OrderTrans = rr.FirstOrDefault() };

                ll2 = ll2.Where(x => x.Store != null);

                ll2 = ll2.OrderByDescending(t => t.Sale.CreatedDate);
                var lst = ll2.ToPageResult(pageIndex, pageSize);
                var lstDto = new List<SaleDto>();
                foreach (var t in lst.Result)
                {
                    var o = Mapper.Map<OPC_Sale, SaleDto>(t.Sale);
                    if (t.Store != null && t.Store.Store != null)
                    {
                        o.StoreName = t.Store.Store.Name;
                        o.StoreTelephone = t.Store.Store.Tel;
                        o.StoreAddress = t.Store.Store.Location;

                    }

                    if (t.OrderTrans != null)
                    {
                        o.TransNo = t.OrderTrans.TransNo;
                    }

                    if (t.Store != null && t.Store.Section != null)
                    {
                        o.SectionName = t.Store.Section.Name;
                    }
                    o.ReceivePerson = t.Order.ShippingContactPerson;
                    o.OrderSource = t.Order.OrderSource;
                    o.InvoiceSubject = t.Order.InvoiceSubject;
                    o.PayType = t.Order.PaymentMethodName;
                    o.Invoice = t.Order.InvoiceDetail;

                    lstDto.Add(o);
                }
                return new PageResult<SaleDto>(lstDto, lst.TotalCount);
            }
        }

        /// <summary>
        /// 根据销售单号获取
        /// </summary>
        /// <param name="saleOrderNo">销售单号</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="storeIds">门店列表</param>
        /// <returns>销售单列表</returns>
        public PagerInfo<SaleDetailDto> GetSalesDetailsBySaleOrderNo(string saleOrderNo, IEnumerable<int> storeIds, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var saleDetails = db.OPC_SaleDetails;
                var brands = db.Brands;
                var sections = db.Sections;
                var sales = db.OPC_Sales;
                var orderItems = db.OrderItems;

                var query =
                    from sale in sales
                    from saleDetail in saleDetails
                    from brand in brands
                    from section in sections
                    from orderItem in orderItems
                    where
                    saleDetail.SaleOrderNo == sale.SaleOrderNo
                    && saleDetail.OrderItemId == orderItem.Id
                    && orderItem.BrandId == brand.Id
                    && sale.SectionId == section.Id
                    && storeIds.Contains(section.StoreId ?? -1)
                    && sale.SaleOrderNo == saleOrderNo
                    orderby saleDetail.Id ascending
                    select new SaleDetailDto()
                    {
                        Id = saleDetail.Id,
                        ProductName = orderItem.ProductName,
                        ProductNo = saleDetail.ProdSaleCode,
                        Color = orderItem.ColorValueName,
                        Size = orderItem.SizeValueName,
                        Brand = brand.Name,
                        SaleOrderNo = sale.SaleOrderNo,
                        LabelPrice = orderItem.UnitPrice.HasValue ? orderItem.UnitPrice.Value : 0M,
                        Price = orderItem.ItemPrice,
                        SellCount = saleDetail.SaleCount,
                        SectionCode = saleDetail.SectionCode,
                        StyleNo = orderItem.StoreItemNo
                    };

                return new PagerInfo<SaleDetailDto>()
                {
                    Index = pageIndex,
                    Size = pageSize,
                    TotalCount = query.Count(),
                    Datas = query.Page(pageIndex, pageSize).ToList()
                };
            }
        }


        public List<SalesOrderModel> GetPagedList(PagerRequest pagerRequest, out int totalCount, SaleOrderFilter filter,
    SaleOrderSortOrder sortOrder)
        {
            return _salesOrderRepository.GetPagedList(pagerRequest, out totalCount, filter, sortOrder);
        }

        public List<OPC_Sale> GetListByNos(List<string> salesOrderNos, SaleOrderFilter filter)
        {
            return _salesOrderRepository.GetListByNos(salesOrderNos, filter);
        }

        public SalesOrderModel GetItemModel(string salesorderno)
        {
            return _salesOrderRepository.GetItemModel(salesorderno);
        }

        public void Update4Cash(SalesOrderModel model, int userId)
        {
            _salesOrderRepository.Update4Cash(model, userId);
        }
    }
}