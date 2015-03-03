using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Support
{
    public class RMARepository : BaseRepository<OPC_RMA>, IRMARepository
    {
        #region methods

        /// <summary>
        /// RMA 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<OPC_RMA, bool>> RMAFilter(RmaQueryRequest filter)
        {
            var query = PredicateBuilder.True<OPC_RMA>();

            if (filter != null)
            {
                if (filter.StoreId != null)
                {
                    query = PredicateBuilder.And(query, v => v.StoreId == filter.StoreId.Value);
                }

                if (filter.DataRoleStores != null && filter.StoreId == null)
                {
                    query = PredicateBuilder.And(query, v => filter.DataRoleStores.Contains(v.StoreId));
                }

                if (filter.Status != null)
                {
                    query = PredicateBuilder.And(query, v => v.Status == filter.Status.Value);
                }

                if (filter.Statuses != null && filter.Statuses.Count > 0)
                {
                    query = PredicateBuilder.And(query, v => filter.Statuses.Contains(v.Status));
                }

                if (filter.ReturnGoodsStatus != null)
                {
                    query = PredicateBuilder.And(query, v => v.RMAStatus == filter.ReturnGoodsStatus.AsId());
                }


                //根据业务 Orderno 与以下互斥
                if (!String.IsNullOrWhiteSpace(filter.OrderNo))
                {
                    query = PredicateBuilder.And(query, v => v.OrderNo == filter.OrderNo);
                    return query;
                }

                if (!String.IsNullOrEmpty(filter.Telephone))
                {
                    query = PredicateBuilder.And(query, v => v.ContactPhone == filter.Telephone);
                    return query;
                }

                if (!String.IsNullOrEmpty(filter.RMANo))
                {
                    query = PredicateBuilder.And(query, v => v.RMANo == filter.RMANo);
                    return query;
                }


                if (filter.StartDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreatedDate >= filter.StartDate.Value);
                }

                if (filter.EndDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreatedDate < filter.EndDate.Value);
                }

                //退货时间
                if (filter.ReturnStartDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.BackDate >= filter.ReturnStartDate.Value);
                }

                if (filter.ReturnEndDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.BackDate < filter.ReturnEndDate.Value);
                }

                //收货时间
                if (filter.ReceiptStartDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.BackDate >= filter.ReceiptStartDate.Value);
                }

                if (filter.ReceiptEndDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.BackDate < filter.ReceiptEndDate.Value);
                }

            }

            return query;
        }

        /// <summary>
        /// Order 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<Order, bool>> OrderFilter(RmaQueryRequest filter)
        {
            var query = PredicateBuilder.True<Order>();

            if (filter != null)
            {
                if (filter.OrderProductType != null)
                {
                    query = PredicateBuilder.And(query, v => v.OrderProductType == filter.OrderProductType);
                }

                //购买时间
                if (filter.BuyStartDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate >= filter.BuyStartDate.Value);
                }

                if (filter.BuyEndDate != null)
                {
                    query = PredicateBuilder.And(query, v => v.CreateDate < filter.BuyEndDate.Value);
                }

                if (!String.IsNullOrEmpty(filter.PayType))
                {
                    query = PredicateBuilder.And(query, v => v.PaymentMethodCode == filter.PayType);
                }

            }

            return query;
        }

        /// <summary>
        /// 销售单 筛选
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        private static Expression<Func<OPC_Sale, bool>> SalesOrderFilter(RmaQueryRequest filter)
        {
            var query = PredicateBuilder.True<OPC_Sale>();

            if (filter != null)
            {
                if (!String.IsNullOrEmpty(filter.SalesOrderNo))
                {
                    query = PredicateBuilder.And(query, v => v.SaleOrderNo == filter.SalesOrderNo);
                }
            }

            return query;
        }


        ///// <summary>
        ///// dynamic to dto
        ///// </summary>
        ///// <param name="t"></param>
        ///// <returns></returns>
        //private static RMADto ConvertRMADto(dynamic t)
        //{
        //    if (t == null)
        //    {
        //        throw new ArgumentNullException("t");
        //    }

        //    var o = new RMADto
        //    {
        //        Id = t.rma.Id,
        //        OrderNo = t.rma.OrderNo,
        //        SaleOrderNo = t.rma.SaleOrderNo,
        //        CashDate = t.sale.CashDate,
        //        CashNum = t.sale.CashNum,
        //        Count = t.rma.Count,
        //        CreatedDate = t.rma.CreatedDate,
        //        RMAAmount = t.rma.RMAAmount,
        //        RMANo = t.rma.RMANo,
        //        BackDate = t.rma.BackDate ?? DateTime.MinValue.AddYears(1),
        //        CompensationFee = t.rma.CompensationFee,
        //        RecoverableSumMoney = t.rma.RecoverableSumMoney,
        //        RMAReason = t.rma.Reason,
        //        RMAType = t.rma.RMAType,
        //        RefundAmount = t.rma.RefundAmount,
        //        RmaCashDate = t.rma.RmaCashDate,
        //        RmaCashNum = t.rma.RmaCashNum,
        //        PayType = t.order.PaymentMethodName,
        //        BuyDate = t.order.CreateDate,
        //        PaymentMethodName = t.order.PaymentMethodName,
        //        RmaCashStatusName = ((EnumRMACashStatus)(t.rma.RMACashStatus ?? 0)).GetDescription(),
        //        Status = t.rma.Status,
        //        StatusName = ((EnumRMAStatus)t.rma.Status).GetDescription(),
        //        SourceDesc = t.rma.SaleRMASource,
        //        RmaStatusName = ((EnumReturnGoodsStatus)(t.rma.RMAStatus ?? 0)).GetDescription(),
        //        StoreName = t.store.Name,
        //        SectionCode = t.section.SectionCode,
        //        SectionName = t.section.Name,
        //        StoreFee = t.rma.StoreFee,
        //        CustomFee = t.rma.CustomFee,
        //        ServiceAgreeDate = t.rma.CreatedDate,
        //        OrderProductType = t.order.OrderProductType ?? 1
        //    };
        //    return o;
        //}

        #endregion

        /// <summary>
        /// rma pagelist
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PagerInfo<RMADto> GetPagedList(RmaQueryRequest request)
        {
            var rmafilter = RMAFilter(request);
            var orderfilter = OrderFilter(request);
            var salesOrderFilter = SalesOrderFilter(request);
            using (var db = new YintaiHZhouContext())
            {
                var rmas = db.OPC_RMAs;
                var orders = db.Orders;
                var sales = db.OPC_Sales;
                var stores = db.Stores;
                var sections = db.Sections;
                var ordertrans = db.OrderTransactions;
                var paymentMethods = db.PaymentMethods;


                var otps = from tran in ordertrans
                           join payment in paymentMethods on tran.PaymentCode equals payment.Code
                           select new
                           {
                               tran,
                               payment
                           };

                //var storesections = sections.Join(stores, s => s.StoreId,
                //       x => x.Id, (section, store) => new { section, store });


                var q = from rma in rmas.AsExpandable().Where(rmafilter)
                        join order in orders.AsExpandable().Where(orderfilter) on rma.OrderNo equals order.OrderNo
                        join sale in sales.AsExpandable().Where(salesOrderFilter) on rma.SaleOrderNo equals sale.SaleOrderNo
                        //join ss in storesections on rma.StoreId equals ss.store.Id
                        join section in sections on rma.SectionId equals section.Id
                        join store in stores on rma.StoreId equals store.Id
                        let otpss = (from otp in otps
                                     where rma.OrderNo == otp.tran.OrderNo
                                     select otp
                        )
                        //where
                        //        rma.StoreId == ss.store.Id
                        //select new
                        //{
                        //    rma,
                        //    order,
                        //    sale,
                        //    store = ss.store,
                        //    section = ss.section,
                        //    trans
                        //};
                        select new RMADto
                        {
                            Id = rma.Id,
                            OrderNo = rma.OrderNo,
                            SaleOrderNo = rma.SaleOrderNo,
                            CashDate = sale.CashDate,
                            CashNum = sale.CashNum,
                            Count = rma.Count,
                            CreatedDate = rma.CreatedDate,
                            RMAAmount = rma.RMAAmount,
                            RMANo = rma.RMANo,
                            BackDate = rma.BackDate,
                            CompensationFee = rma.CompensationFee,
                            RecoverableSumMoney = rma.RecoverableSumMoney,
                            RMAReason = rma.Reason,
                            RMAType = rma.RMAType,
                            RefundAmount = rma.RefundAmount,
                            RmaCashDate = rma.RmaCashDate,
                            RmaCashNum = rma.RmaCashNum,

                            BuyDate = order.CreateDate,

                            // RmaCashStatusName = ((EnumRMACashStatus)(rma.RMACashStatus ?? 0)).GetDescription(),
                            RmaCashStatus = rma.RMACashStatus ?? 0,
                            Status = rma.Status,
                            //StatusName = ((EnumRMAStatus)rma.Status).GetDescription(),
                            SourceDesc = rma.SaleRMASource,
                            //RmaStatusName = ((EnumReturnGoodsStatus)(rma.RMAStatus ?? 0)).GetDescription(),
                            RmaStatus = rma.RMAStatus ?? 0,
                            StoreName = store.Name,
                            SectionCode = section.SectionCode,
                            SectionName = section.Name,
                            StoreFee = rma.StoreFee,
                            CustomFee = rma.CustomFee,
                            ServiceAgreeDate = rma.CreatedDate,
                            RMACount = rma.Count ?? 0,
                            OrderProductType = order.OrderProductType ?? 1,
                            OrderTransactions = otpss.Select(s => new OrderTransactionDto
                            {
                                PaymentCode = s.payment.Code,
                                PaymentName = s.payment.Name,
                                TransNo = s.tran.TransNo,
                                Amount = s.tran.Amount
                            })
                        };

                var total = q.Count();
                var datas = q.OrderByDescending(v => v.Id).Skip(request.PagerRequest.SkipCount).Take(request.PagerRequest.PageSize).ToList();

                return new PagerInfo<RMADto>(request.PagerRequest, total, datas);
            }
        }

        /// <summary>
        /// 获取RMA
        /// </summary>
        /// <param name="rmano"></param>
        /// <returns></returns>
        public RMADto GetItem(string rmano)
        {
            using (var db = new YintaiHZhouContext())
            {
                var rmas = db.OPC_RMAs.Where(v => v.RMANo == rmano);
                var orders = db.Orders;
                var sales = db.OPC_Sales;
                var stores = db.Stores;
                var sections = db.Sections;
                var ordertrans = db.OrderTransactions;
                var paymentMethods = db.PaymentMethods;

                var otps = from tran in ordertrans
                           join payment in paymentMethods on tran.PaymentCode equals payment.Code
                           select new
                           {
                               tran,
                               payment
                           };

                //var storesections = sections.Join(stores, s => s.StoreId,
                //       x => x.Id, (section, store) => new { section, store });


                var q = from rma in rmas
                        join order in orders on rma.OrderNo equals order.OrderNo
                        join sale in sales on rma.SaleOrderNo equals sale.SaleOrderNo
                        join section in sections on rma.SectionId equals section.Id
                        join store in stores on rma.StoreId equals store.Id
                        let otpss = (from otp in otps
                                     where rma.OrderNo == otp.tran.OrderNo
                                     select otp
                   )
                        select new RMADto
                        {
                            Id = rma.Id,
                            OrderNo = rma.OrderNo,
                            SaleOrderNo = rma.SaleOrderNo,
                            CashDate = sale.CashDate,
                            CashNum = sale.CashNum,
                            Count = rma.Count,
                            CreatedDate = rma.CreatedDate,
                            RMAAmount = rma.RMAAmount,
                            RMANo = rma.RMANo,
                            BackDate = rma.BackDate,
                            CompensationFee = rma.CompensationFee,
                            RecoverableSumMoney = rma.RecoverableSumMoney,
                            RMAReason = rma.Reason,
                            RMAType = rma.RMAType,
                            RefundAmount = rma.RefundAmount,
                            RmaCashDate = rma.RmaCashDate,
                            RmaCashNum = rma.RmaCashNum,

                            BuyDate = order.CreateDate,

                            // RmaCashStatusName = ((EnumRMACashStatus)(rma.RMACashStatus ?? 0)).GetDescription(),
                            RmaCashStatus = rma.RMACashStatus ?? 0,
                            Status = rma.Status,
                            //StatusName = ((EnumRMAStatus)rma.Status).GetDescription(),
                            SourceDesc = rma.SaleRMASource,
                            //RmaStatusName = ((EnumReturnGoodsStatus)(rma.RMAStatus ?? 0)).GetDescription(),
                            RmaStatus = rma.RMAStatus ?? 0,
                            StoreName = store.Name,
                            SectionCode = section.SectionCode,
                            SectionName = section.Name,
                            StoreFee = rma.StoreFee,
                            CustomFee = rma.CustomFee,
                            ServiceAgreeDate = rma.CreatedDate,
                            RMACount = rma.Count ?? 0,
                            OrderProductType = order.OrderProductType ?? 1,
                            OrderTransactions = otpss.Select(s => new OrderTransactionDto
                            {
                                PaymentCode = s.payment.Code,
                                PaymentName = s.payment.Name,
                                TransNo = s.tran.TransNo,
                                Amount = s.tran.Amount
                            })
                        };

                return q.FirstOrDefault();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rma"></param>
        public void SetStatus(RMA rma)
        {

        }

        private readonly DateTime _benchTime = new DateTime(2014, 4, 22);
        public PageResult<OPC_RMA> GetByReturnGoods(ReturnGoodsInfoRequest request)
        {
            //todo 为实现
            return null;
        }

        public PageResult<RMADto> GetAll(string orderNo, string saleOrderNo, DateTime startTime, DateTime endTime,
            int? rmaStatus, EnumReturnGoodsStatus returnGoodsStatus, int pageIndex, int pageSize)
        {

            using (var db = new YintaiHZhouContext())
            {
                var query = db.OPC_RMAs.AsQueryable();//.Where(t => t.CreatedDate >= startTime && t.CreatedDate < endTime);
                ;
                if (rmaStatus.HasValue && rmaStatus != -1)
                {
                    query = query.Where(t => t.Status == rmaStatus);
                }

                if (returnGoodsStatus != EnumReturnGoodsStatus.NoProcess)
                {
                    query = query.Where(t => t.RMAStatus == (int)returnGoodsStatus);
                }

                if (orderNo.IsNotNull())
                {
                    query = query.Where(t => t.OrderNo == orderNo);
                }
                if (saleOrderNo.IsNotNull())
                {
                    query = query.Where(t => t.SaleOrderNo == saleOrderNo);
                }

                var lst2 = query
                  .Join(db.Stores.Where(t => t.CreatedDate > _benchTime), t => t.StoreId, o => o.Id,
                      (t, o) => new { Rma = t, StoreName = o.Name })
                  .Join(db.OPC_Sales, t => t.Rma.SaleOrderNo, o => o.SaleOrderNo,
                      (t, o) => new { t.Rma, t.StoreName, Sale = o })
                  .Join(db.Orders.Where(t => t.CreateDate >= startTime && t.CreateDate < endTime), t => t.Rma.OrderNo, o => o.OrderNo,
                      (t, o) =>
                          new
                          {
                              t.Rma,
                              t.StoreName,
                              t.Sale,
                              payType = o.PaymentMethodName,
                              buyDate = o.CreateDate
                          }).Join(db.Sections.Where(s => s.CreateDate > _benchTime), x => x.Rma.SectionId, s => s.Id, (x, s) => new { x.Rma, x.StoreName, x.Sale, x.payType, s.SectionCode, x.buyDate, SectionName = s.Name })
                  .OrderByDescending(t => t.Rma.CreatedDate);

                var lst = lst2.ToPageResult(pageIndex, pageSize);
                var lstSaleRma = lst.Result.Select(t => CreateRMADto(t)).ToList();

                return new PageResult<RMADto>(lstSaleRma, lst.TotalCount);
            }
        }

        public PageResult<RMADto> GetByRmaNo(string rmaNo)
        {
            var listRmas = new List<RMADto>();
            using (var db = new YintaiHZhouContext())
            {
                var lst2 = db.OPC_RMAs.Where(r => r.RMANo == rmaNo)
                 .Join(db.Stores.Where(t => t.CreatedDate > _benchTime), t => t.StoreId, o => o.Id,
                     (t, o) => new { Rma = t, StoreName = o.Name })
                 .Join(db.OPC_Sales, t => t.Rma.SaleOrderNo, o => o.SaleOrderNo,
                     (t, o) => new { t.Rma, t.StoreName, Sale = o })
                 .Join(db.Orders, t => t.Rma.OrderNo, o => o.OrderNo,
                     (t, o) =>
                         new
                         {
                             t.Rma,
                             t.StoreName,
                             t.Sale,
                             payType = o.PaymentMethodName,
                             buyDate = o.CreateDate
                         }).Join(db.Sections.Where(s => s.CreateDate > _benchTime), x => x.Rma.SectionId, s => s.Id, (x, s) => new { x.Rma, x.StoreName, x.Sale, x.payType, s.SectionCode, x.buyDate, SectionName = s.Name })
                 .OrderByDescending(t => t.Rma.CreatedDate);

                var lst = lst2.ToList();

                listRmas.AddRange(lst.Select(t => CreateRMADto(t)));
            }

            return new PageResult<RMADto>(listRmas, 1);
        }

        public PageResult<RMADto> GetByPackPrintPress(string orderNo, string saleOrderNo, DateTime startTime, DateTime endTime,
            int? rmaStatus, int pageIdex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var query =
                    db.OPC_RMAs.Where(
                        t =>
                            t.CreatedDate >= startTime && t.CreatedDate < endTime &&
                            (t.RMAStatus == (int)EnumReturnGoodsStatus.PayVerify ||
                             t.RMAStatus == (int)EnumReturnGoodsStatus.CompensateVerifyPass));
                if (rmaStatus.HasValue)
                {
                    query = query.Where(t => t.Status == rmaStatus.Value);
                }
                if (orderNo.IsNotNull())
                {
                    query = query.Where(t => t.OrderNo.Contains(orderNo));
                }
                if (saleOrderNo.IsNotNull())
                {
                    query = query.Where(t => t.SaleOrderNo.Contains(saleOrderNo));
                }

                var lst2 = query
                 .Join(db.Stores.Where(t => t.CreatedDate > _benchTime), t => t.StoreId, o => o.Id,
                     (t, o) => new { Rma = t, StoreName = o.Name })
                 .Join(db.OPC_Sales, t => t.Rma.SaleOrderNo, o => o.SaleOrderNo,
                     (t, o) => new { t.Rma, t.StoreName, Sale = o })
                 .Join(db.Orders, t => t.Rma.OrderNo, o => o.OrderNo,
                     (t, o) =>
                         new
                         {
                             t.Rma,
                             t.StoreName,
                             t.Sale,
                             payType = o.PaymentMethodName,
                             buyDate = o.CreateDate
                         }).Join(db.Sections.Where(s => s.CreateDate > _benchTime), x => x.Rma.SectionId, s => s.Id, (x, s) => new { x.Rma, x.StoreName, x.Sale, x.payType, s.SectionCode, x.buyDate, SectionName = s.Name })
                 .OrderByDescending(t => t.Rma.CreatedDate);

                var lst = lst2.ToPageResult(pageIdex, pageSize);
                var lstSaleRma = lst.Result.Select(t => CreateRMADto(t)).ToList();
                return new PageResult<RMADto>(lstSaleRma, lst.TotalCount);
            }
        }

        public PageResult<RMADto> GetRmaReturnByExpress(string orderNo, DateTime startTime, DateTime endTime, int pageIndex, int pageSize)
        {
            /*改为包裹审核通过的*/
            using (var db = new YintaiHZhouContext())
            {
                var query = db.OPC_RMAs.Where(t => t.CreatedDate >= startTime && t.CreatedDate < endTime && t.Status == (int)EnumRMAStatus.ShipVerifyPass);

                if (orderNo.IsNotNull())
                {
                    query = query.Where(t => t.OrderNo.Contains(orderNo));
                }

                var lst2 = query
                 .Join(db.Stores.Where(t => t.CreatedDate > _benchTime), t => t.StoreId, o => o.Id,
                     (t, o) => new { Rma = t, StoreName = o.Name })
                 .Join(db.OPC_Sales, t => t.Rma.SaleOrderNo, o => o.SaleOrderNo,
                     (t, o) => new { t.Rma, t.StoreName, Sale = o })
                 .Join(db.Orders, t => t.Rma.OrderNo, o => o.OrderNo,
                     (t, o) =>
                         new
                         {
                             t.Rma,
                             t.StoreName,
                             t.Sale,
                             payType = o.PaymentMethodName,
                             buyDate = o.CreateDate
                         }).Join(db.Sections.Where(s => s.CreateDate > _benchTime), x => x.Rma.SectionId, s => s.Id, (x, s) => new { x.Rma, x.StoreName, x.Sale, x.payType, s.SectionCode, x.buyDate, SectionName = s.Name })
                 .OrderByDescending(t => t.Rma.CreatedDate);

                var lst = lst2.ToPageResult(pageIndex, pageSize);
                var lstSaleRma = lst.Result.Select(t => CreateRMADto(t)).ToList();
                return new PageResult<RMADto>(lstSaleRma, lst.TotalCount);
            }
        }

        public PageResult<RMADto> GetRmaPrintByExpress(string orderNo, DateTime startTime, DateTime endTime, int pageIndex, int pageSize)
        {
            //（物流入库+通知单品+付款确认）  2014-5-28 讨论确认 打印退货单后，财务付款确认
            using (var db = new YintaiHZhouContext())
            {
                var query = db.OPC_RMAs.Where(t => t.CreatedDate >= startTime && t.CreatedDate < endTime && (t.Status == (int)EnumRMAStatus.ShipInStorage || t.Status == (int)EnumRMAStatus.NotifyProduct || t.Status == (int)EnumRMAStatus.ShoppingGuideReceive));     //|| t.Status == (int)EnumRMAStatus.PayVerify

                if (orderNo.IsNotNull())
                {
                    query = query.Where(t => t.OrderNo.Contains(orderNo));
                }

                var stores = db.Stores.Where(t => t.CreatedDate > _benchTime);
                if (!CurrentUser.IsSystem)
                {
                    stores = stores.Where(x => CurrentUser.StoreIds.Contains(x.Id));
                }

                var lst2 = query
                 .Join(db.Stores.Where(t => t.CreatedDate > _benchTime), t => t.StoreId, o => o.Id,
                     (t, o) => new { Rma = t, StoreName = o.Name })
                 .Join(db.OPC_Sales, t => t.Rma.SaleOrderNo, o => o.SaleOrderNo,
                     (t, o) => new { Rma = t.Rma, StoreName = t.StoreName, Sale = o })
                 .Join(db.Orders, t => t.Rma.OrderNo, o => o.OrderNo,
                     (t, o) =>
                         new
                         {
                             t.Rma,
                             t.StoreName,
                             t.Sale,
                             payType = o.PaymentMethodName,
                             buyDate = o.CreateDate
                         }).Join(db.Sections.Where(s => s.CreateDate > _benchTime), x => x.Rma.SectionId, s => s.Id, (x, s) => new { x.Rma, x.StoreName, x.Sale, x.payType, s.SectionCode, x.buyDate, SectionName = s.Name })
                 .OrderByDescending(t => t.Rma.CreatedDate);

                var lst = lst2.ToPageResult(pageIndex, pageSize);
                var lstSaleRma = lst.Result.Select(t => CreateRMADto(t)).ToList();

                return new PageResult<RMADto>(lstSaleRma, lst.TotalCount);
            }
        }

        public PageResult<RMADto> GetRmaByShoppingGuide(string orderNo, DateTime startTime, DateTime endTime, int pageIndex, int pageSize)
        {
            CheckUser();
            using (var db = new YintaiHZhouContext())
            {
                var query = db.OPC_RMAs.Where(t => t.CreatedDate >= startTime && t.CreatedDate < endTime && t.RMAStatus == (int)EnumReturnGoodsStatus.Valid && CurrentUser.StoreIds.Contains(t.StoreId));

                if (orderNo.IsNotNull())
                {
                    query = query.Where(t => t.OrderNo.Contains(orderNo));
                }

                var lst2 = query
                 .Join(db.Stores.Where(t => t.CreatedDate > _benchTime), t => t.StoreId, o => o.Id,
                     (t, o) => new { Rma = t, StoreName = o.Name })
                 .Join(db.OPC_Sales, t => t.Rma.SaleOrderNo, o => o.SaleOrderNo,
                     (t, o) => new { t.Rma, t.StoreName, Sale = o })
                 .Join(db.Orders, t => t.Rma.OrderNo, o => o.OrderNo,
                     (t, o) =>
                         new
                         {
                             t.Rma,
                             t.StoreName,
                             t.Sale,
                             payType = o.PaymentMethodName,
                             buyDate = o.CreateDate
                         }).Join(db.Sections.Where(s => s.CreateDate > _benchTime), x => x.Rma.SectionId, s => s.Id, (x, s) => new { x.Rma, x.StoreName, x.Sale, x.payType, s.SectionCode, x.buyDate, SectionName = s.Name })
                 .OrderByDescending(t => t.Rma.CreatedDate);

                var lst = lst2.ToPageResult(pageIndex, pageSize);
                var lstSaleRma = lst.Result.Select(t => CreateRMADto(t)).ToList();

                return new PageResult<RMADto>(lstSaleRma, lst.TotalCount);
            }
        }

        public PageResult<RMADto> GetRmaByAllOver(string orderNo, DateTime startTime, DateTime endTime, int pageIndex, int pageSize)
        {
            CheckUser();

            int rma = EnumRMAStatus.ShoppingGuideReceive.AsId();
            using (var db = new YintaiHZhouContext())
            {
                var query = db.OPC_RMAs.Where(t => t.CreatedDate >= startTime && t.CreatedDate < endTime && t.Status == (int)EnumRMAStatus.ShoppingGuideReceive && CurrentUser.StoreIds.Contains(t.StoreId));

                if (orderNo.IsNotNull())
                {
                    query = query.Where(t => t.OrderNo.Contains(orderNo));
                }

                var lst2 = query
                 .Join(db.Stores.Where(t => t.CreatedDate > _benchTime), t => t.StoreId, o => o.Id,
                     (t, o) => new { Rma = t, StoreName = o.Name })
                 .Join(db.OPC_Sales, t => t.Rma.SaleOrderNo, o => o.SaleOrderNo,
                     (t, o) => new { t.Rma, t.StoreName, Sale = o })
                 .Join(db.Orders, t => t.Rma.OrderNo, o => o.OrderNo,
                     (t, o) =>
                         new
                         {
                             t.Rma,
                             t.StoreName,
                             t.Sale,
                             payType = o.PaymentMethodName,
                             buyDate = o.CreateDate
                         }).Join(db.Sections.Where(s => s.CreateDate > _benchTime), x => x.Rma.SectionId, s => s.Id, (x, s) => new { x.Rma, x.StoreName, x.Sale, x.payType, s.SectionCode, x.buyDate, SectionName = s.Name })
                 .OrderByDescending(t => t.Rma.CreatedDate);

                var lst = lst2.ToPageResult(pageIndex, pageSize);
                var lstSaleRma = lst.Result.Select(t => CreateRMADto(t)).ToList();

                return new PageResult<RMADto>(lstSaleRma, lst.TotalCount);
            }
        }

        private RMADto CreateRMADto(dynamic t)
        {
            var o = new RMADto
            {
                Id = t.Rma.Id,
                OrderNo = t.Rma.OrderNo,
                SaleOrderNo = t.Rma.SaleOrderNo,
                CashDate = t.Sale.CashDate,
                CashNum = t.Sale.CashNum,
                Count = t.Rma.Count,
                CreatedDate = t.Rma.CreatedDate,
                RMAAmount = t.Rma.RMAAmount,
                RMANo = t.Rma.RMANo,
                BackDate = t.Rma.BackDate,
                CompensationFee = t.Rma.CompensationFee,
                RecoverableSumMoney = t.Rma.RecoverableSumMoney,
                RMAReason = t.Rma.Reason,
                RMAType = t.Rma.RMAType,
                RefundAmount = t.Rma.RefundAmount,
                RmaCashDate = t.Rma.RmaCashDate,
                RmaCashNum = t.Rma.RmaCashNum,
                PayType = t.payType,
                BuyDate = t.buyDate,
                PaymentMethodName = t.payType,
                RmaCashStatus = t.Rma.RMACashStatus
            };

            //var status2 = (EnumRMAStatus)(t.Rma.Status);
            //o.StatusName = status2.GetDescription();
            o.Status = t.Rma.Status;
            o.SourceDesc = t.Rma.SaleRMASource;
            //o.RmaStatusName = ((EnumReturnGoodsStatus)t.Rma.RMAStatus).GetDescription();
            o.RmaStatus = t.Rma.RMAStatus;


            o.StoreName = t.StoreName;
            o.SectionCode = t.SectionCode;
            o.SectionName = t.SectionName;
            o.StoreFee = t.Rma.StoreFee;
            o.CustomFee = t.Rma.CustomFee;
            o.ServiceAgreeDate = t.Rma.CreatedDate;
            o.RecoverableSumMoney = t.Rma.RecoverableSumMoney;
            o.CompensationFee = t.Rma.CompensationFee;
            return o;
        }

        public OPC_RMA GetByRmaNo2(string rmaNo)
        {
            return Select(t => t.RMANo == rmaNo).FirstOrDefault();
        }
    }
}