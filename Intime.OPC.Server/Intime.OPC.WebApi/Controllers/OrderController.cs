// ***********************************************************************
// Assembly         : 03_Intime.OPC.WebApi
// Author           : Liuyh
// Created          : 03-25-2014 13:43:29
//
// Last Modified By : Liuyh
// Last Modified On : 03-26-2014 00:26:13
// ***********************************************************************
// <copyright file="OrderController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Intime.OPC.WebApi.Controllers
{
    /// <summary>
    ///     Class OrderController.
    /// </summary>
    public class OrderController : BaseController
    {
        /// <summary>
        ///     The _order service
        /// </summary>
        private readonly IOrderService _orderService;
        private readonly IRmaService _rmaService;


        /// <summary>
        ///     Initializes a new instance of the <see cref="OrderController" /> class.
        /// </summary>
        /// <param name="orderService">The order service.</param>
        public OrderController(IOrderService orderService, IRmaService rmaService)
        {
            _orderService = orderService;
            _rmaService = rmaService;
        }

        [NonAction]
        private IHttpActionResult GetPagedList4RmaPagedResult(RmaQueryRequest request, UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var dto = _rmaService.GetPagedList(request);

            var restResult = new PageResult<RMADto>(dto.Datas, dto.TotalCount);

            return RetrunHttpActionResult(restResult);
        }


        [HttpPost]
        [HttpGet]
        public IHttpActionResult GetOrder([FromUri] OrderQueryRequest request, [UserProfile] UserProfile userProfile)
        {
            if (request == null)
            {
                return BadRequest("参数为空");
            }

            //#if DEBUG


            //#else
            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            request.DataRoleStores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();
            //#endif

            request.ArrangeParams();

            return DoFunction(() =>
            {
                //_orderService.UserId = uid;
                return _orderService.GetPagedList(request);
            });
        }


        [HttpPost]
        [HttpGet]
        public IHttpActionResult GetAllSatusOrder([FromUri] OrderQueryRequest request, [UserProfile] UserProfile userProfile)
        {
            if (request == null)
            {
                return BadRequest("参数为空");
            }

            //#if DEBUG


            //#else
            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            request.DataRoleStores = userProfile.StoreIds.ToList();
            //#endif

            request.ArrangeParams();

            return DoFunction(() =>
            {
                //_orderService.UserId = uid;
                return _orderService.GetPagedListExcludeSalesOrder(request);
            });
        }

        ///// <summary>
        /////     获得未提货的数据
        ///// </summary>
        ///// <param name="orderNo">The order no.</param>
        ///// <param name="orderSource">The order source.</param>
        ///// <param name="startCreateDate">The start create date.</param>
        ///// <param name="endCreateDate">The end create date.</param>
        ///// <param name="storeId">The store identifier.</param>
        ///// <param name="brandId">The brand identifier.</param>
        ///// <param name="status">The status.</param>
        ///// <param name="paymentType">Type of the payment.</param>
        ///// <param name="outGoodsType">Type of the out goods.</param>
        ///// <param name="shippingContactPhone">The shipping contact phone.</param>
        ///// <param name="expressDeliveryCode">The express delivery code.</param>
        ///// <param name="expressDeliveryCompany">The express delivery company.</param>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>IHttpActionResult.</returns>
        //[HttpPost]
        ////[Route("api/order/getorder")]
        //public IHttpActionResult GetOrder(string orderNo,
        //    string orderSource,
        //    DateTime startCreateDate,
        //    DateTime endCreateDate,
        //    int storeId,
        //    int brandId,
        //    int status,
        //    string paymentType,
        //    string outGoodsType,
        //    string shippingContactPhone,
        //    string expressDeliveryCode,
        //    int expressDeliveryCompany, int pageIndex, int pageSize, [UserId] int uid)
        //{
        //    try
        //    {
        //        if (pageSize <= 0)
        //        {
        //            pageSize = 20;
        //        }

        //        _orderService.UserId = uid;

        //        var lst = _orderService.GetOrder(orderNo, orderSource, startCreateDate, endCreateDate, storeId, brandId,
        //            status, paymentType, outGoodsType, shippingContactPhone, expressDeliveryCode, expressDeliveryCompany,
        //            uid, pageIndex, pageSize);

        //        return Ok(lst);
        //    }
        //    catch (Exception ex)
        //    {
        //        GetLog().Error(ex);
        //        return InternalServerError();
        //    }
        //}


        [HttpPost]
        public IHttpActionResult GetOrderItemsByOrderNo(string orderNo, int pageIndex, int pageSize, [UserId] int uid)
        {
            //todo 查询订单明细 未实现
            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetOrderItems(orderNo, pageIndex, pageSize);
            }, "读取订单明细失败");
        }

        [HttpPost]
        public IHttpActionResult GetOrderByShippingSaleNo([FromUri]OrderQueryRequest request, [UserProfile] UserProfile userProfile)
        {
            //_orderService.UserId = uid;
            //return DoFunction(() => _orderService.GetOrderByShippingNo(shippingNo, pageIndex, pageSize), "通过快递单查询订单失败");

            return GetOrder(request, userProfile);
        }

        /// <summary>
        /// Gets the order by oder no.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetOrderByOderNo(string orderNo, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetOrderByOrderNo(orderNo);
            });
            //using (var db = new Repository.YintaiHZhouContext())
            //{
            //    var order = db.Orders.Where(x => x.OrderNo == orderNo)
            //        .Join(db.OrderTransactions, o => o.OrderNo, x => x.OrderNo, (o, t)=>new
            //        {
            //            Id=o.Id,
            //            OrderNo = o.OrderNo,
            //            OrderChannelNo = t.TransNo,
            //            PaymentMethodName = o.PaymentMethodName,
            //            OrderSouce = o.OrderSource,
            //            Status = o.Status,
            //            Quantity = 0,
            //            TotalAmount = o.TotalAmount,
            //            CustomerFreight  = o.ShippingFee,
            //            MustPayTotal = t.Amount,
            //            BuyDate  = o.CreateDate,
            //            CustomerName  = o.ShippingContactPerson,
            //            CustomerAddress = o.ShippingAddress,
            //            CustomerPhone = o.ShippingContactPhone,
            //            CustomerRemark = o.Memo,
            //            IfReceipt = o.NeedInvoice,
            //            ReceiptHead = o.InvoiceSubject,
            //            ReceiptContent = o.InvoiceDetail,
            //            OutGoodsType = 1,
            //            PostCode = o.ShippingZipCode,
            //            ShippingNo = o.ShippingNo,
            //            ExpressNo  = string.Empty,
            //            ExpressCompany = o.ShippingVia,

            //        });
            //    return Ok(order);
            //}
        }

        /// <summary>
        /// 查询订单 根据时间，和订单编码
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="starTime">The star time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetOrderByOderNoTime(string orderNo, DateTime starTime, DateTime endTime, int pageIndex, int pageSize, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetOrderByOderNoTime(orderNo, starTime, endTime, pageIndex, pageSize);
            });
        }

        #region 客服退货查询-退货信息
        [HttpPost]
        public IHttpActionResult GetByReturnGoodsInfo([FromUri] ReturnGoodsInfoRequest request, [UserId] int uid, [UserProfile] UserProfile userProfile)
        {
            var queryRequest = AutoMapper.Mapper.Map<ReturnGoodsInfoRequest, RmaQueryRequest>(request);

            return GetPagedList4RmaPagedResult(queryRequest, userProfile);

            //return DoFunction(() =>
            //{
            //    _orderService.UserId = uid;
            //    return _orderService.GetByReturnGoodsInfo(request);
            //}, "查询订单信息失败");
        }

        #endregion

        #region 客服退货查询-物流退回



        [HttpPost]
        public IHttpActionResult GetShippingBackByReturnGoodsInfo([FromUri] ReturnGoodsInfoRequest request, [UserId] int uid)
        {

            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetShippingBackByReturnGoodsInfo(request);
            }, "查询订单信息失败");
        }

        #endregion

        #region 客服退货查询-退货赔偿退回
        [HttpPost]
        public IHttpActionResult GetByReturnGoodsCompensate([FromUri] ReturnGoodsInfoRequest request, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetSaleRmaByReturnGoodsCompensate(request);
            });
        }

        //ReturnGoodsInfoRequest
        #endregion

        #region 缺货提醒-缺货订单

        /// <summary>
        /// Gets the order by oder no time.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetOrderByOutOfStockNotify([FromUri] OutOfStockNotifyRequest request, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetOrderByOutOfStockNotify(request);
            });
        }

        [HttpPost]
        public IHttpActionResult SetSaleOrderVoid([FromBody] IEnumerable<string> saleOrderNos, [UserId] int uid)
        {
            return DoAction(() =>
            {
                _orderService.UserId = uid;
                foreach (var saleOrderNo in saleOrderNos)
                {
                    _orderService.SetSaleOrderVoid(saleOrderNo);
                }
            });
        }

        #endregion

        #region 缺货提醒-已取消订单

        /// <summary>
        /// Gets the order by oder no time.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetOrderOfVoid([FromUri] OutOfStockNotifyRequest request, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetOrderOfVoid(request);
            });
        }

        #endregion


        #region 网站销售明细统计
        //SearchStatRequest

        [System.Obsolete("过期，请使用StatisticsController.GetList4SalesDetailsReport,新接口，返回值不同")]
        [HttpPost]
        public IHttpActionResult WebSiteStatSaleDetail([FromUri] SearchStatRequest request, [UserProfile] UserProfile userProfile)
        {

            //#if !DEBUG
            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            request.DataRoleStores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();
            //#endif
            request.ArrangeParams();

            return DoFunction(() =>
            {
                //_orderService.UserId = uid;
                return _orderService.WebSiteStatSaleDetail(request);
            });
        }

        #endregion

        #region 网站退货明细统计
        //SearchStatRequest

        [System.Obsolete("过期，请使用StatisticsController.GetList4RmaDetailsReport,新接口，返回值不同")]
        [HttpPost]
        public IHttpActionResult WebSiteStatReturnDetail([FromUri] SearchStatRequest request, [UserProfile] UserProfile userProfile)
        {
            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            request.DataRoleStores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();
            request.ArrangeParams();

            return DoFunction(() =>
            {
                //_orderService.UserId = uid;
                return _orderService.WebSiteStatReturnDetail(request);
            });
        }

        #endregion

        #region  网上收银流水对账查询
        //SearchStatRequest

        [System.Obsolete("过期，请使用StatisticsController.GetList4CashierDetailsReport,新接口，返回值不同")]
        [HttpPost]
        public IHttpActionResult WebSiteCashier([FromUri] SearchCashierRequest request, [UserProfile] UserProfile userProfile)
        {
            request.StoreId = CheckStoreId(request.StoreId);
            var result = CheckRole4Store(userProfile, request.StoreId);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            request.DataRoleStores = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();

            request.ArrangeParams();

            return DoFunction(() =>
            {
                //_orderService.UserId = uid;
                return _orderService.WebSiteCashier(request);
            });
        }

        #endregion

        #region 备注

        /// <summary>
        ///  增加订单备注
        /// </summary>
        /// <param name="sale">The sale.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult AddOrderComment([FromBody] OPC_OrderComment comment, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                comment.CreateDate = DateTime.Now;
                comment.CreateUser = uid;
                comment.UpdateDate = comment.CreateDate;
                comment.UpdateUser = comment.CreateUser;
                return _orderService.AddOrderComment(comment);

            }, "增加订单备注失败");
        }
        /// <summary>
        /// 根据订单编号读取订单备注
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <returns>IHttpActionResult.</returns>
        [HttpPost]
        public IHttpActionResult GetCommentByOderNo(string orderNo, [UserId] int uid)
        {
            return base.DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetCommentByOderNo(orderNo);

            }, "读取订单备注失败！");
        }


        #endregion



        /// <summary>
        /// Gets the order by oder no.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <returns>IHttpActionResult.</returns>
        [Route("api/orders/{orderno}")]
        [HttpGet]
        public IHttpActionResult GetOrderss(string orderNo, [UserId] int uid)
        {
            return DoFunction(() =>
            {
                _orderService.UserId = uid;
                return _orderService.GetOrderByOrderNo(orderNo);
            });

        }
    }
}