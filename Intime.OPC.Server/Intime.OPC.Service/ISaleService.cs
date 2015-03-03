// ***********************************************************************
// Assembly         : 02_Intime.OPC.Service
// Author           : Liuyh
// Created          : 03-19-2014 20:11:42
//
// Last Modified By : Liuyh
// Last Modified On : 03-23-2014 23:16:55
// ***********************************************************************
// <copyright file="ISaleService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;
using System;
using System.Collections.Generic;

namespace Intime.OPC.Service
{
    /// <summary>
    ///     Interface ISaleService
    /// </summary>
    public interface ISaleService : IService
    {
        /// <summary>
        ///     Selects this instance.
        /// </summary>
        /// <returns>IList{OPC_Sale}.</returns>
        IList<OPC_Sale> Select();

        /// <summary>
        ///     Gets the remarks by sale no.
        /// </summary>
        /// <param name="saleNo">The sale no.</param>
        /// <returns>IList{OPC_SaleComment}.</returns>
        IList<OPC_SaleComment> GetRemarksBySaleNo(string saleNo);

        /// <summary>
        ///     打印销售单
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool PrintSale(string orderNo, int userId);

        /// <summary>
        ///     Gets the sale order details.
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IList{OPC_SaleDetail}.</returns>
        PageResult<SaleDetailDto> GetSaleOrderDetails(string saleOrderNo, int userId, int pageIndex, int pageSize);

        /// <summary>
        ///     销售提货
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool SetSaleOrderPickUp(string saleOrderNo, int userId);

        /// <summary>
        ///     物流入库
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="dataRolesStores">数据权限</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool SetShipInStorage(string saleOrderNo, int userId, List<int> dataRolesStores);

        /// <summary>
        ///     打印发货单
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool PrintInvoice(string orderNo, int userId);

        /// <summary>
        ///     完成打印销售单
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="userId">The user identifier.</param>
        bool FinishPrintSale(string orderNo, int userId);

        /// <summary>
        ///     打印快递单
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool PrintExpress(string orderNo, int userId);

        /// <summary>
        ///     发货
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool Shipped(string orderNo, int userId);

        /// <summary>
        ///     缺货
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool StockOut(string orderNo, int userId);

        /// <summary>
        ///     获得 已发货 的数据
        /// </summary>
        /// <param name="saleOrderNo"></param>
        /// <param name="userId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        PageResult<SaleDto> GetPickUp(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd, int userID, int pageIndex, int pageSize);

        /// <summary>
        ///     获得 未提货 的数据
        /// </summary>
        /// <param name="saleOrderNo"></param>
        /// <param name="userId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        PageResult<SaleDto> GetNoPickUp(string saleOrderNo, int userId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize);

        /// <summary>
        ///     获得已完成 打印销售单 的数据
        /// </summary>
        /// <param name="saleId"></param>
        /// <param name="userId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        PageResult<SaleDto> GetPrintSale(string saleId, int userId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize);

        /// <summary>
        ///     获得 已发货 的数据
        /// </summary>
        /// <param name="saleId"></param>
        /// <param name="userId"></param>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        PageResult<SaleDto> GetShipped(string saleOrderNo, int userId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize);
        /// <summary>
        ///     获得已完成 打印快递单 的数据
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetPrintExpress(string saleOrderNo, int userId, string orderNo, DateTime dtStart,
            DateTime dtEnd, int pageIndex, int pageSize);

        /// <summary>
        ///     获得已完成 打印发货单 的数据
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetPrintInvoice(string saleOrderNo, int userId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize);

        /// <summary>
        ///     获得已完成 物流入库 的数据
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetShipInStorage(string saleOrderNo, int userId, string orderNo, DateTime dtStart,
            DateTime dtEnd, int pageIndex, int pageSize);

        bool WriteSaleRemark(OPC_SaleComment comment);

        /// <summary>
        ///     根据订单号获得销售单信息
        /// </summary>
        /// <param name="orderID">The order identifier.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetByOrderNo(string orderID, int userid, int pageIndex, int pageSize);

        /// <summary>
        /// 获得销售单信息
        /// </summary>
        /// <param name="shippingCode">快递单号</param>
        /// <returns>SaleDto.</returns>
        IList<SaleDto> GetByShippingCode(string shippingCode);


        /// <summary>
        /// 根据销售单号获取
        /// </summary>
        /// <param name="saleOrderNo">销售单号</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="storeIds">门店列表</param>
        /// <returns>销售单列表</returns>
        PagerInfo<SaleDetailDto> GetSalesDetailsBySaleOrderNo(string saleOrderNo, IEnumerable<int> storeIds, int pageIndex, int pageSize);

        /// <summary>
        /// 设置销售单收银状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ExectueResult SetSalesOrderCash(SalesOrderCashRequest request);
    }
}