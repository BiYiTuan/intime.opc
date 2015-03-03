// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-25-2014 13:36:49
//
// Last Modified By : Liuyh
// Last Modified On : 03-25-2014 13:37:17
// ***********************************************************************
// <copyright file="IOrderRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;
using System;
using PagerRequest = Intime.OPC.Domain.PagerRequest;

namespace Intime.OPC.Repository
{
    /// <summary>
    ///     Interface IOrderRepository
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        PageResult<Order> GetOrder(string orderNo, string orderSource, DateTime dtStart, DateTime dtEnd, int storeId,
            int brandId, int status, string paymentType, string outGoodsType, string shippingContactPhone,
            string expressDeliveryCode, int expressDeliveryCompany, int pageIndex, int pageSize = 20);

        Order GetOrderByOrderNo(string orderNo);

        PageResult<Order> GetOrderByOderNoTime(string orderNo, DateTime starTime, DateTime endTime, int pageIndex, int pageSize);
        PageResult<Order> GetOrderByShippingNo(string shippingNo, int pageIndex, int pageSize);

        PageResult<Order> GetByReturnGoodsInfo(Domain.Dto.Custom.ReturnGoodsInfoRequest request);

        PageResult<Order> GetBySaleRma(ReturnGoodsInfoRequest request, int? rmaStatus, EnumReturnGoodsStatus status);
        PageResult<Order> GetByOutOfStockNotify(OutOfStockNotifyRequest request, int orderstatus);

        OrderModel GetItemByOrderNo(string orderno);

        /// <summary>
        /// order list
        /// </summary>
        /// <param name="pagerRequest">分页请求对象</param>
        /// <param name="request">筛选项</param>
        /// <param name="sortOrder">排序项</param>
        /// <returns></returns>
        PagerInfo<OrderModel> GetPagedList(PagerRequest pagerRequest, OrderQueryRequest request,
            OrderSortOrder sortOrder);


        /// <summary>
        /// 业务上 不关联销售单
        /// </summary>
        /// <param name="pagerRequest"></param>
        /// <param name="request"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        PagerInfo<OrderModel> GetPagedListExcludeSalesOrder(PagerRequest pagerRequest, OrderQueryRequest request,
                                                            OrderSortOrder sortOrder);
    }
}