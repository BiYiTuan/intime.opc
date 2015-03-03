using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Impl
{

    internal class SalesOrderRepository : OPCBaseRepository<int, OPC_Sale>//, ISaleOrderRepository
    {
        #region methods

        /// <summary>
        /// 销售单 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<OPC_Sale, bool>> Filler(SaleOrderFilter filter)
        {
            var query = PredicateBuilder.True<OPC_Sale>();

            if (filter != null)
            {
                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }

                if (filter.Status == null && filter.Statuses != null && filter.Statuses.Count > 0)
                {
                    query = PredicateBuilder.And(query, v => filter.Statuses.Contains(v.Status));
                }

                //销售单收银状态
                if (filter.CashStatuses != null && filter.CashStatuses.Count > 0)
                {
                    query = PredicateBuilder.And(query, v => filter.CashStatuses.Contains(v.CashStatus ?? -1));
                }

                //像如下这样 会返回特定数据的 将与时间互斥
                if (!String.IsNullOrWhiteSpace(filter.SalesOrderNo))
                {
                    query = PredicateBuilder.And(query, v => v.SaleOrderNo == filter.SalesOrderNo);

                    return query;
                }


                if (!String.IsNullOrWhiteSpace(filter.OrderNo))
                {
                    query = PredicateBuilder.And(query, v => v.OrderNo == filter.OrderNo);
                    return query;
                }

                if (filter.ShippingOrderId != null)
                {
                    query = PredicateBuilder.And(query, v => v.ShippingSaleId == filter.ShippingOrderId);

                    return query;
                }



                //if (filter.if != null)
                //{
                //    if (filter.HasDeliveryOrderGenerated.Value)
                //    {
                //        //已经生成发货单的
                //        query = PredicateBuilder.And(query, v => v.ShippingSaleId > 0);
                //    }
                //    else
                //    {
                //        //未生成发货单的
                //        query = PredicateBuilder.And(query, v => (!v.ShippingSaleId.HasValue) || v.ShippingSaleId < 1);
                //    }
                //}



                if (filter.DateRange != null)
                {
                    if (filter.DateRange.StartDateTime != null)
                    {
                        query = PredicateBuilder.And(query, v => v.CreatedDate >= filter.DateRange.StartDateTime.Value);
                    }

                    if (filter.DateRange.EndDateTime != null)
                    {
                        query = PredicateBuilder.And(query, v => v.CreatedDate < filter.DateRange.EndDateTime.Value);
                    }
                }
            }

            return query;
        }

        /// <summary>
        /// STORE 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Store, bool>> Store4Filler(SaleOrderFilter filter)
        {
            var query = PredicateBuilder.True<Store>();

            if (filter != null)
            {
                if (filter.StoreId != null)
                {
                    query = PredicateBuilder.And(query, v => v.Id == filter.StoreId.Value);
                }

                if (filter.StoreId == null && filter.DataRoleStores != null && filter.DataRoleStores.Count > 0)
                {
                    query = PredicateBuilder.And(query, v => filter.DataRoleStores.Contains(v.Id));
                }
            }

            return query;
        }


        /// <summary>
        /// oRDER 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Order, bool>> Order4Filler(SaleOrderFilter filter)
        {
            var query = PredicateBuilder.True<Order>();

            if (filter != null)
            {
                if (filter.OrderProductType != null)
                {
                    query = PredicateBuilder.And(query, v => v.OrderProductType == filter.OrderProductType.Value);
                }
            }

            return query;
        }

        #endregion

        public override IEnumerable<OPC_Sale> AutoComplete(string query)
        {
            throw new NotImplementedException();
        }

        public List<SalesOrderModel> GetPagedList(PagerRequest pagerRequest, out int totalCount, SaleOrderFilter filter,
            SaleOrderSortOrder sortOrder)
        {
            var salesFilter = Filler(filter);
            var storeFilter = Store4Filler(filter);
            var orderFilter = Order4Filler(filter);

            using (var db = GetYintaiHZhouContext())
            {
                var salesorders = db.OPC_Sales;
                var sections = db.Sections;
                var stores = db.Stores;
                var ordertrans = db.OrderTransactions;
                var paymentMethods = db.PaymentMethods;
                var orders = db.Orders;

                #region cankao

                //var otps = from tran in ordertrans
                //           join payment in paymentMethods on tran.PaymentCode equals payment.Code
                //           select new
                //               {
                //                   tran,
                //                   payment
                //                   //transno = tran.TransNo,
                //                   //paymentname = payment.Name,
                //                   //paymentcode = payment.Code
                //               };
                //var storesections = sections.Join(stores.AsExpandable().Where(storeFilter), s => s.StoreId,
                //                       x => x.Id, (section, store) => new { section, store });


                //var query = from so in salesorders.AsExpandable().Where(salesFilter)
                //            join ss in storesections on so.SectionId equals  ss.section.Id
                //            join order in orders.AsExpandable().Where(orderFilter) on so.OrderNo equals order.OrderNo
                //            let trans = ( from trans in otps
                //                          where so.OrderNo == trans.tran.OrderNo
                //                          select trans
                //                        )
                //            select new
                //                {
                //                    salesorder = so,
                //                    storesections = ss,
                //                    order,
                //                    trans
                //                };

                //totalCount = query.Count();

                //var rstt = query.OrderByDescending(v => v.salesorder.Id).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize)
                //    .Select(v =>

                //        new SalesOrderModel
                //  {
                //      Id = v.salesorder.Id,
                //      OrderNo = v.salesorder.OrderNo,
                //      SaleOrderNo = v.salesorder.SaleOrderNo,
                //      SalesType = v.salesorder.SalesType,
                //      ShipViaId = v.salesorder.ShipViaId,
                //      Status = v.salesorder.Status,
                //      ShippingCode = v.salesorder.ShippingCode,
                //      ShippingFee = v.salesorder.ShippingFee.HasValue ? v.salesorder.ShippingFee.Value : 0m,
                //      ShippingStatus = v.salesorder.ShippingStatus,
                //      //ShippingStatusName = v.salesorder.ShippingStatus.HasValue ? ((EnumSaleOrderStatus)v.salesorder.ShippingStatus.Value).GetDescription() : String.Empty,
                //      ShippingRemark = v.salesorder.ShippingRemark,
                //      SellDate = v.order.CreateDate,
                //      IfTrans = v.salesorder.IfTrans,
                //      TransStatus = v.salesorder.TransStatus,
                //      SalesAmount = v.salesorder.SalesAmount,
                //      SalesCount = v.salesorder.SalesCount,
                //      CashStatus = v.salesorder.CashStatus,
                //      CashNum = v.salesorder.CashNum,
                //      CashDate = v.salesorder.CashDate,
                //      //CashStatusName = ((EnumCashStatus)v.salesorder.CashStatus).GetDescription(),
                //      //SectionId = v.salesorder.SectionId??0,
                //      PrintTimes = v.salesorder.PrintTimes ?? 0,
                //      Remark = v.salesorder.Remark,
                //      RemarkDate = v.salesorder.RemarkDate,
                //      //store
                //      StoreId = v.storesections.store.Id,
                //      StoreName = v.storesections.store.Name,
                //      StoreAddress = v.storesections.store.Location,
                //      StoreTelephone = v.storesections.store.Tel,
                //      //section
                //      SectionId = v.storesections.section.Id,
                //      SectionName = v.storesections.section.Name,
                //      SectionCode = v.storesections.section.SectionCode,

                //      CreatedDate = v.salesorder.CreatedDate,
                //      CreatedUser = v.salesorder.CreatedUser,
                //      UpdatedDate = v.salesorder.UpdatedDate,
                //      UpdatedUser = v.salesorder.UpdatedUser ?? 0,
                //      //ot.ot
                //      //ShippingSale
                //      //ShipViaId = shipp_let.FirstOrDefault()
                //      //ShipViaName = ss!=null? ss.ShipViaName:String.Empty,
                //      //ShippingCode = ss!=null? ss.ShippingCode:String.Empty,
                //      //ShippingFee = ss!=null? ss.ShippingFee:String.Empty,
                //      //ShippingStatus == ss!=null? ss.ShippingStatus:String.Empty,

                //      //Order
                //      CustomerName = v.order.ShippingContactPerson,
                //      ReceivePerson = v.order.ShippingContactPerson,
                //      CustomerPhone = v.order.ShippingContactPhone,
                //      OrderSource = v.order.OrderSource,
                //      //CustomerAddress = v.oo.ShippingAddress,
                //      //CustomerRemark = v.oo.Memo,
                //      Invoice = v.order.NeedInvoice,
                //      InvoiceSubject = v.order.InvoiceSubject,
                //      //IfReceipt = v.oo.NeedInvoice,
                //      //ReceiptHead = v.oo.InvoiceSubject,
                //      //ReceiptContent = v.oo.InvoiceDetail,



                //      ShippingSaleId = v.salesorder.ShippingSaleId,
                //      OrderProductType = v.order.OrderProductType ?? 1,

                //      ////OT

                //      //TransNo = v.ootp.trans.Any(v=>v.tran.TransNo),
                //      //pay
                //      //PayType = v.pm.Name,
                //      OrderTransactionModels = v.trans.Select(s => new OrderTransactionModel
                //      {
                //          PaymentCode = s.payment.Code,
                //          PaymentName = s.payment.Name,
                //          TransNo = s.tran.TransNo

                //      }),

                //      Memo = v.order.Memo,
                //      PromotionRules = v.order.PromotionRules,
                //      PromotionDesc = v.order.PromotionDesc

                //  }).ToList();

                #endregion

                #region

                var otps = from tran in ordertrans
                           join payment in paymentMethods on tran.PaymentCode equals payment.Code
                           select new
                               {
                                   tran,
                                   payment
                               };
                var storesections = sections.Join(stores.AsExpandable().Where(storeFilter), s => s.StoreId,
                                       x => x.Id, (section, store) => new { section, store });


                var query = from so in salesorders.AsExpandable().Where(salesFilter)
                            join ss in storesections on so.SectionId equals ss.section.Id
                            join order in orders.AsExpandable().Where(orderFilter) on so.OrderNo equals order.OrderNo
                            let trans = (from trans in otps
                                         where so.OrderNo == trans.tran.OrderNo
                                         select trans
                                        )
                            select new
                                {
                                    salesorder = so,
                                    storesections = ss,
                                    order,
                                    otp = trans//.OrderByDescending(v => v.tran.Amount).ThenByDescending(v => v.tran.PaymentCode).Skip(0).Take(1)
                                };

                totalCount = query.Count();

                var rst = query.OrderByDescending(v => v.salesorder.Id).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize)
                    .Select(v =>

                        new SalesOrderModel
                  {
                      Id = v.salesorder.Id,
                      OrderNo = v.salesorder.OrderNo,
                      SaleOrderNo = v.salesorder.SaleOrderNo,
                      SalesType = v.salesorder.SalesType,
                      ShipViaId = v.salesorder.ShipViaId,
                      Status = v.salesorder.Status,
                      ShippingCode = v.salesorder.ShippingCode,
                      ShippingFee = v.salesorder.ShippingFee.HasValue ? v.salesorder.ShippingFee.Value : 0m,
                      ShippingStatus = v.salesorder.ShippingStatus,
                      //ShippingStatusName = v.salesorder.ShippingStatus.HasValue ? ((EnumSaleOrderStatus)v.salesorder.ShippingStatus.Value).GetDescription() : String.Empty,
                      ShippingRemark = v.salesorder.ShippingRemark,
                      SellDate = v.order.CreateDate,
                      IfTrans = v.salesorder.IfTrans,
                      TransStatus = v.salesorder.TransStatus,
                      SalesAmount = v.salesorder.SalesAmount,
                      SalesCount = v.salesorder.SalesCount,
                      CashStatus = v.salesorder.CashStatus,
                      CashNum = v.salesorder.CashNum,
                      CashDate = v.salesorder.CashDate,
                      //CashStatusName = ((EnumCashStatus)v.salesorder.CashStatus).GetDescription(),
                      //SectionId = v.salesorder.SectionId??0,
                      PrintTimes = v.salesorder.PrintTimes ?? 0,
                      Remark = v.salesorder.Remark,
                      RemarkDate = v.salesorder.RemarkDate,
                      //store
                      StoreId = v.storesections.store.Id,
                      StoreName = v.storesections.store.Name,
                      StoreAddress = v.storesections.store.Location,
                      StoreTelephone = v.storesections.store.Tel,
                      //section
                      SectionId = v.storesections.section.Id,
                      SectionName = v.storesections.section.Name,
                      SectionCode = v.storesections.section.SectionCode,

                      CreatedDate = v.salesorder.CreatedDate,
                      CreatedUser = v.salesorder.CreatedUser,
                      UpdatedDate = v.salesorder.UpdatedDate,
                      UpdatedUser = v.salesorder.UpdatedUser ?? 0,
                      //ot.ot
                      //ShippingSale
                      //ShipViaId = shipp_let.FirstOrDefault()
                      //ShipViaName = ss!=null? ss.ShipViaName:String.Empty,
                      //ShippingCode = ss!=null? ss.ShippingCode:String.Empty,
                      //ShippingFee = ss!=null? ss.ShippingFee:String.Empty,
                      //ShippingStatus == ss!=null? ss.ShippingStatus:String.Empty,

                      //Order
                      CustomerName = v.order.ShippingContactPerson,
                      ReceivePerson = v.order.ShippingContactPerson,
                      CustomerPhone = v.order.ShippingContactPhone,
                      OrderSource = v.order.OrderSource,
                      //CustomerAddress = v.oo.ShippingAddress,
                      //CustomerRemark = v.oo.Memo,
                      NeedInvoice = v.order.NeedInvoice,
                      InvoiceSubject = v.order.InvoiceSubject,
                      Invoice = v.order.InvoiceDetail,
                      //IfReceipt = v.oo.NeedInvoice,
                      //ReceiptHead = v.oo.InvoiceSubject,
                      //ReceiptContent = v.oo.InvoiceDetail,



                      ShippingSaleId = v.salesorder.ShippingSaleId,
                      OrderProductType = v.order.OrderProductType ?? 1,

                      ////OT

                      //TransNo = v.ootp.trans.Any(v=>v.tran.TransNo),
                      //pay
                      //PayType = v.pm.Name,
                      //TransNo = v.otp != null ? v.otp[0].tran.TransNo : String.Empty,
                      //PayType = v.otp != null ? v.otp.payment.Name : String.Empty,

                      OrderTransactionModels = v.otp.Select(s => new OrderTransactionModel
                      {
                          PaymentCode = s.payment.Code,
                          PaymentName = s.payment.Name,
                          TransNo = s.tran.TransNo,
                          Amount = s.tran.Amount
                      }),


                      Memo = v.order.Memo,
                      PromotionRules = v.order.PromotionRules,
                      PromotionDesc = v.order.PromotionDesc

                  }).ToList();

                #endregion

                #region old
                //var q1 = salesorders.AsExpandable().Where(salesFilter)
                //                    .Join(
                //                        sections.Join(stores.AsExpandable().Where(storeFilter), s => s.StoreId,
                //                                      x => x.Id, (section, store) => new { section, store }),
                //                        o => o.SectionId, s => s.section.Id, (o, s) => new { o, store = s })
                //    .Join(ordertrans, o => o.o.OrderNo, ot => ot.OrderNo, (o, ot) => new { o, ot });



                //var q = from ot in q1
                //        //join ss in db.OPC_ShippingSales on ot.o.o.ShippingSaleId equals ss.Id into tmp1
                //        //from ss in tmp1.DefaultIfEmpty()
                //        join p in paymentMethods on ot.ot.PaymentCode equals p.Code
                //        join oo in orders.AsExpandable().Where(orderFilter) on ot.o.o.OrderNo equals oo.OrderNo
                //        select new //SalesOrderModel
                //  {
                //      ot,
                //      oo,
                //      pm = p
                //  };

                //totalCount = q.Count();

                //var rst = q.OrderByDescending(v => v.ot.o.o.CreatedDate).ThenBy(v => v.ot.o.o.OrderNo).Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize)
                //    .Select(v =>

                //        new SalesOrderModel
                //  {
                //      Id = v.ot.o.o.Id,
                //      OrderNo = v.ot.o.o.OrderNo,
                //      SaleOrderNo = v.ot.o.o.SaleOrderNo,
                //      SalesType = v.ot.o.o.SalesType,
                //      ShipViaId = v.ot.o.o.ShipViaId,
                //      Status = v.ot.o.o.Status,
                //      ShippingCode = v.ot.o.o.ShippingCode,
                //      ShippingFee = v.ot.o.o.ShippingFee.HasValue ? v.ot.o.o.ShippingFee.Value : 0m,
                //      ShippingStatus = v.ot.o.o.ShippingStatus,
                //      //ShippingStatusName = v.ot.o.o.ShippingStatus.HasValue ? ((EnumSaleOrderStatus)v.ot.o.o.ShippingStatus.Value).GetDescription() : String.Empty,
                //      ShippingRemark = v.ot.o.o.ShippingRemark,
                //      SellDate = v.ot.ot.CreateDate,
                //      IfTrans = v.ot.o.o.IfTrans,
                //      TransStatus = v.ot.o.o.TransStatus,
                //      SalesAmount = v.ot.o.o.SalesAmount,
                //      SalesCount = v.ot.o.o.SalesCount,
                //      CashStatus = v.ot.o.o.CashStatus,
                //      CashNum = v.ot.o.o.CashNum,
                //      CashDate = v.ot.o.o.CashDate,
                //      //CashStatusName = ((EnumCashStatus)v.ot.o.o.CashStatus).GetDescription(),
                //      //SectionId = v.ot.o.o.SectionId??0,
                //      PrintTimes = v.ot.o.o.PrintTimes ?? 0,
                //      Remark = v.ot.o.o.Remark,
                //      RemarkDate = v.ot.o.o.RemarkDate,
                //      //store
                //      StoreId = v.ot.o.store.store.Id,
                //      StoreName = v.ot.o.store.store.Name,
                //      StoreAddress = v.ot.o.store.store.Location,
                //      StoreTelephone = v.ot.o.store.store.Tel,
                //      //section
                //      SectionId = v.ot.o.store.section.Id,
                //      SectionName = v.ot.o.store.section.Name,
                //      SectionCode = v.ot.o.store.section.SectionCode,

                //      CreatedDate = v.ot.o.o.CreatedDate,
                //      CreatedUser = v.ot.o.o.CreatedUser,
                //      UpdatedDate = v.ot.o.o.UpdatedDate,
                //      UpdatedUser = v.ot.o.o.UpdatedUser ?? 0,
                //      //ot.ot
                //      //ShippingSale
                //      //ShipViaId = shipp_let.FirstOrDefault()
                //      //ShipViaName = ss!=null? ss.ShipViaName:String.Empty,
                //      //ShippingCode = ss!=null? ss.ShippingCode:String.Empty,
                //      //ShippingFee = ss!=null? ss.ShippingFee:String.Empty,
                //      //ShippingStatus == ss!=null? ss.ShippingStatus:String.Empty,

                //      //Order
                //      CustomerName = v.oo.ShippingContactPerson,
                //      ReceivePerson = v.oo.ShippingContactPerson,
                //      CustomerPhone = v.oo.ShippingContactPhone,
                //      OrderSource = v.oo.OrderSource,
                //      //CustomerAddress = v.oo.ShippingAddress,
                //      //CustomerRemark = v.oo.Memo,
                //      Invoice = v.oo.NeedInvoice,
                //      InvoiceSubject = v.oo.InvoiceSubject,
                //      //IfReceipt = v.oo.NeedInvoice,
                //      //ReceiptHead = v.oo.InvoiceSubject,
                //      //ReceiptContent = v.oo.InvoiceDetail,

                //      ////OT
                //      TransNo = v.ot.ot.TransNo,

                //      ShippingSaleId = v.ot.o.o.ShippingSaleId,
                //      OrderProductType = v.oo.OrderProductType ?? 1,

                //      //pay
                //      PayType = v.pm.Name,

                //      Memo = v.oo.Memo,
                //      PromotionRules = v.oo.PromotionRules,
                //      PromotionDesc = v.oo.PromotionDesc

                //  }).ToList();

                #endregion

                return rst;
                //return AutoMapper.Mapper.Map<List<OPC_SaleClone>, List<OPC_Sale>>(rst);
            }
        }

        public List<OPC_Sale> GetListByNos(List<string> salesOrderNos, SaleOrderFilter filter)
        {
            var query = Filler(filter);

            query = PredicateBuilder.And(query, v => salesOrderNos.Contains(v.SaleOrderNo));
            return Func(db => EFHelper.Get(db, query).ToList());
        }


        public SalesOrderModel GetItemModel(string salesorderno)
        {
            using (var db = GetYintaiHZhouContext())
            {
                var salesorders = db.OPC_Sales;
                var sections = db.Sections;
                var stores = db.Stores;
                var ordertrans = db.OrderTransactions;
                var paymentMethods = db.PaymentMethods;
                var orders = db.Orders;

                var otps = from tran in ordertrans
                           join payment in paymentMethods on tran.PaymentCode equals payment.Code
                           select new
                           {
                               tran,
                               payment
                               //transno = tran.TransNo,
                               //paymentname = payment.Name,
                               //paymentcode = payment.Code
                           };
                var storesections = sections.Join(stores, s => s.StoreId,
                                       x => x.Id, (section, store) => new { section, store });


                var query = from so in salesorders.Where(v => v.SaleOrderNo == salesorderno)
                            join ss in storesections on so.SectionId equals ss.section.Id
                            join order in orders on so.OrderNo equals order.OrderNo
                            let trans = (from trans in otps
                                         where so.OrderNo == trans.tran.OrderNo
                                         select trans
                                        )
                            select new
                            {
                                salesorder = so,
                                storesections = ss,
                                order,
                                otp = trans//.OrderByDescending(v => v.tran.Amount).ThenByDescending(v => v.tran.PaymentCode).Skip(0).Take(1)
                            };

                var rst = query
                    .Select(v =>

                        new SalesOrderModel
                        {
                            Id = v.salesorder.Id,
                            OrderNo = v.salesorder.OrderNo,
                            SaleOrderNo = v.salesorder.SaleOrderNo,
                            SalesType = v.salesorder.SalesType,
                            ShipViaId = v.salesorder.ShipViaId,
                            Status = v.salesorder.Status,
                            ShippingCode = v.salesorder.ShippingCode,
                            ShippingFee = v.salesorder.ShippingFee.HasValue ? v.salesorder.ShippingFee.Value : 0m,
                            ShippingStatus = v.salesorder.ShippingStatus,
                            //ShippingStatusName = v.salesorder.ShippingStatus.HasValue ? ((EnumSaleOrderStatus)v.salesorder.ShippingStatus.Value).GetDescription() : String.Empty,
                            ShippingRemark = v.salesorder.ShippingRemark,
                            SellDate = v.order.CreateDate,
                            IfTrans = v.salesorder.IfTrans,
                            TransStatus = v.salesorder.TransStatus,
                            SalesAmount = v.salesorder.SalesAmount,
                            SalesCount = v.salesorder.SalesCount,
                            CashStatus = v.salesorder.CashStatus,
                            CashNum = v.salesorder.CashNum,
                            CashDate = v.salesorder.CashDate,
                            //CashStatusName = ((EnumCashStatus)v.salesorder.CashStatus).GetDescription(),
                            //SectionId = v.salesorder.SectionId??0,
                            PrintTimes = v.salesorder.PrintTimes ?? 0,
                            Remark = v.salesorder.Remark,
                            RemarkDate = v.salesorder.RemarkDate,
                            //store
                            StoreId = v.storesections.store.Id,
                            StoreName = v.storesections.store.Name,
                            StoreAddress = v.storesections.store.Location,
                            StoreTelephone = v.storesections.store.Tel,
                            //section
                            SectionId = v.storesections.section.Id,
                            SectionName = v.storesections.section.Name,
                            SectionCode = v.storesections.section.SectionCode,

                            CreatedDate = v.salesorder.CreatedDate,
                            CreatedUser = v.salesorder.CreatedUser,
                            UpdatedDate = v.salesorder.UpdatedDate,
                            UpdatedUser = v.salesorder.UpdatedUser ?? 0,
                            //ot.ot
                            //ShippingSale
                            //ShipViaId = shipp_let.FirstOrDefault()
                            //ShipViaName = ss!=null? ss.ShipViaName:String.Empty,
                            //ShippingCode = ss!=null? ss.ShippingCode:String.Empty,
                            //ShippingFee = ss!=null? ss.ShippingFee:String.Empty,
                            //ShippingStatus == ss!=null? ss.ShippingStatus:String.Empty,

                            //Order
                            CustomerName = v.order.ShippingContactPerson,
                            ReceivePerson = v.order.ShippingContactPerson,
                            CustomerPhone = v.order.ShippingContactPhone,
                            OrderSource = v.order.OrderSource,
                            //CustomerAddress = v.oo.ShippingAddress,
                            //CustomerRemark = v.oo.Memo,
                            NeedInvoice = v.order.NeedInvoice,
                            InvoiceSubject = v.order.InvoiceSubject,
                            //IfReceipt = v.oo.NeedInvoice,
                            //ReceiptHead = v.oo.InvoiceSubject,
                            //ReceiptContent = v.oo.InvoiceDetail,



                            ShippingSaleId = v.salesorder.ShippingSaleId,
                            OrderProductType = v.order.OrderProductType ?? 1,

                            ////OT

                            //TransNo = v.ootp.trans.Any(v=>v.tran.TransNo),
                            //pay
                            //PayType = v.pm.Name,
                            //TransNo = v.otp != null ? v.otp[0].tran.TransNo : String.Empty,
                            //PayType = v.otp != null ? v.otp.payment.Name : String.Empty,

                            OrderTransactionModels = v.otp.Select(s => new OrderTransactionModel
                            {
                                PaymentCode = s.payment.Code,
                                PaymentName = s.payment.Name,
                                TransNo = s.tran.TransNo,
                                Amount = s.tran.Amount
                            }),


                            Memo = v.order.Memo,
                            PromotionRules = v.order.PromotionRules,
                            PromotionDesc = v.order.PromotionDesc

                        }).FirstOrDefault();

                return rst;

                #region old

                //var q1 = db.OPC_Sales.Where(v => v.SaleOrderNo == salesorderno)
                //    .Join(db.Sections
                //        .Join(db.Stores, s => s.StoreId, x => x.Id, (section, store) => new { section, store }),
                //        o => o.SectionId, s => s.section.Id, (o, s) => new { o, store = s })
                //    .Join(db.OrderTransactions, o => o.o.OrderNo, ot => ot.OrderNo, (o, ot) => new { o, ot });
                //var q = from ot in q1
                //        //join ss in db.OPC_ShippingSales on ot.o.o.ShippingSaleId equals ss.Id into tmp1
                //        //from ss in tmp1.DefaultIfEmpty()
                //        join p in db.PaymentMethods on ot.ot.PaymentCode equals p.Code
                //        join oo in db.Orders on ot.o.o.OrderNo equals oo.OrderNo
                //        select new //SalesOrderModel
                //        {
                //            ot,
                //            oo,
                //            pm = p
                //        };

                //var rst = q
                //    .Select(v =>

                //        new SalesOrderModel
                //        {
                //            Id = v.ot.o.o.Id,
                //            OrderNo = v.ot.o.o.OrderNo,
                //            SaleOrderNo = v.ot.o.o.SaleOrderNo,
                //            SalesType = v.ot.o.o.SalesType,
                //            ShipViaId = v.ot.o.o.ShipViaId,
                //            Status = v.ot.o.o.Status,
                //            ShippingCode = v.ot.o.o.ShippingCode,
                //            ShippingFee = v.ot.o.o.ShippingFee.HasValue ? v.ot.o.o.ShippingFee.Value : 0m,
                //            ShippingStatus = v.ot.o.o.ShippingStatus,
                //            //ShippingStatusName = v.ot.o.o.ShippingStatus.HasValue ? ((EnumSaleOrderStatus)v.ot.o.o.ShippingStatus.Value).GetDescription() : String.Empty,
                //            ShippingRemark = v.ot.o.o.ShippingRemark,
                //            SellDate = v.ot.ot.CreateDate,
                //            IfTrans = v.ot.o.o.IfTrans,
                //            TransStatus = v.ot.o.o.TransStatus,
                //            SalesAmount = v.ot.o.o.SalesAmount,
                //            SalesCount = v.ot.o.o.SalesCount,
                //            CashStatus = v.ot.o.o.CashStatus,
                //            CashNum = v.ot.o.o.CashNum,
                //            CashDate = v.ot.o.o.CashDate,
                //            //CashStatusName = ((EnumCashStatus)v.ot.o.o.CashStatus).GetDescription(),
                //            //SectionId = v.ot.o.o.SectionId??0,
                //            PrintTimes = v.ot.o.o.PrintTimes ?? 0,
                //            Remark = v.ot.o.o.Remark,
                //            RemarkDate = v.ot.o.o.RemarkDate,
                //            //store
                //            StoreId = v.ot.o.store.store.Id,
                //            StoreName = v.ot.o.store.store.Name,
                //            StoreAddress = v.ot.o.store.store.Location,
                //            StoreTelephone = v.ot.o.store.store.Tel,
                //            //section
                //            SectionId = v.ot.o.store.section.Id,
                //            SectionName = v.ot.o.store.section.Name,
                //            SectionCode = v.ot.o.store.section.SectionCode,

                //            CreatedDate = v.ot.o.o.CreatedDate,
                //            CreatedUser = v.ot.o.o.CreatedUser,
                //            UpdatedDate = v.ot.o.o.UpdatedDate,
                //            UpdatedUser = v.ot.o.o.UpdatedUser ?? 0,
                //            //ot.ot
                //            //ShippingSale
                //            //ShipViaId = shipp_let.FirstOrDefault()
                //            //ShipViaName = ss!=null? ss.ShipViaName:String.Empty,
                //            //ShippingCode = ss!=null? ss.ShippingCode:String.Empty,
                //            //ShippingFee = ss!=null? ss.ShippingFee:String.Empty,
                //            //ShippingStatus == ss!=null? ss.ShippingStatus:String.Empty,

                //            //Order
                //            CustomerName = v.oo.ShippingContactPerson,
                //            ReceivePerson = v.oo.ShippingContactPerson,
                //            CustomerPhone = v.oo.ShippingContactPhone,
                //            OrderSource = v.oo.OrderSource,
                //            //CustomerAddress = v.oo.ShippingAddress,
                //            //CustomerRemark = v.oo.Memo,
                //            Invoice = v.oo.NeedInvoice,
                //            InvoiceSubject = v.oo.InvoiceSubject,
                //            //IfReceipt = v.oo.NeedInvoice,
                //            //ReceiptHead = v.oo.InvoiceSubject,
                //            //ReceiptContent = v.oo.InvoiceDetail,

                //            ////OT
                //            TransNo = v.ot.ot.TransNo,

                //            ShippingSaleId = v.ot.o.o.ShippingSaleId,
                //            OrderProductType = v.oo.OrderProductType ?? 1,

                //            //pay
                //            PayType = v.pm.Name,
                //            Memo = v.oo.Memo,
                //            PromotionRules = v.oo.PromotionRules,
                //            PromotionDesc = v.oo.PromotionDesc
                //        }).FirstOrDefault();

                //return rst;

                #endregion

                //return AutoMapper.Mapper.Map<List<OPC_SaleClone>, List<OPC_Sale>>(rst);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        public void Update4Cash(SalesOrderModel model, int userId)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            var entity = GetItem(model.Id);

            Action(db =>
            {

                entity.CashDate = model.CashDate;
                entity.CashNum = model.CashNum;
                entity.CashStatus = model.CashStatus;
                entity.UpdatedDate = DateTime.Now;
                entity.UpdatedUser = userId;

                EFHelper.UpdateEntityFields(db, entity, new List<string>
                    {
                        "CashDate",
                        "CashNum",
                        "CashStatus",
                        "UpdatedDate",
                        "UpdatedUser",
                    });
            });
        }
    }
}
