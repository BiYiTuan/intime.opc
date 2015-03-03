// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-19-2014 20:11:42
//
// Last Modified By : Liuyh
// Last Modified On : 03-24-2014 01:28:54
// ***********************************************************************
// <copyright file="ISaleRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    /// <summary>
    ///     Interface ISaleRepository
    /// </summary>
    public interface ISaleRepository : IRepository<OPC_Sale>
    {
        /// <summary>
        ///     Selects this instance.
        /// </summary>
        /// <returns>IList{OPC_Sale}.</returns>
        IList<OPC_Sale> Select();

        ///// <summary>
        /////     Updates the satus.
        ///// </summary>
        ///// <param name="saleNos">The sale nos.</param>
        ///// <param name="saleOrderStatus">The sale order status.</param>
        ///// <param name="userID">The user identifier.</param>
        ///// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        //bool UpdateSatus(IEnumerable<string> saleNos, EnumSaleOrderStatus saleOrderStatus, int userID);

        /// <summary>
        ///     Updates the satus.
        /// </summary>
        /// <param name="saleNos">The sale nos.</param>
        /// <param name="saleOrderStatus">The sale order status.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool UpdateSatus(string saleNo, EnumSaleOrderStatus saleOrderStatus, int userID);

        /// <summary>
        ///     Gets the by sale no.
        /// </summary>
        /// <param name="saleNo">The sale no.</param>
        /// <returns>OPC_Sale.</returns>
        OPC_Sale GetBySaleNo(string saleNo);

        /// <summary>
        ///     Gets the sale order details.
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <returns>IList{OPC_SaleDetail}.</returns>
        PageResult<SaleDetailDto> GetSaleOrderDetails(string saleOrderNo, int pageIndex, int pageSize);

        /// <summary>
        ///     获得 未提货 的数据
        /// </summary>
        /// <param name="saleId">The sale identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetNoPickUp(string saleId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize, params int[] sectionIds);

        /// <summary>
        ///     获得 已发货 的数据
        /// </summary>
        /// <param name="saleId">The sale identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetPickUped(string saleId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize, params int[] sectionIds);

        /// <summary>
        ///     获得已完成 打印销售单 的数据
        /// </summary>
        /// <param name="saleId">The sale identifier.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetPrintSale(string saleId, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize, params int[] sectionIds);

        /// <summary>
        ///     获得 打印快递单 的数据
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetPrintExpress(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize, params int[] sectionIds);

        /// <summary>
        ///     获得 打印发货单 的数据
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetPrintInvoice(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize, params int[] sectionIds);

        /// <summary>
        ///     获得 物流入库 的数据
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="orderNo">The order no.</param>
        /// <param name="dtStart">The dt start.</param>
        /// <param name="dtEnd">The dt end.</param>
        /// <returns>IList{OPC_Sale}.</returns>
        PageResult<SaleDto> GetShipInStorage(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize, params int[] sectionIds);

        /// <summary>
        /// 读取某个专柜下 某个订单的销售单
        /// </summary>
        /// <param name="orderID">The order identifier.</param>
        /// <param name="sectinID">当-1时，查询所有专柜的销售单</param>
        /// <returns>IList{OPC_Sale}.</returns>
        IList<OPC_Sale> GetByOrderNo(string orderID, int sectinID);

        IList<SaleDto> GetByOrderNo2(string orderID);


        PageResult<SaleDto> GetShipped(string saleOrderNo, string orderNo, DateTime dtStart, DateTime dtEnd, int pageIndex, int pageSize, params int[] sectionIds);

        IList<OPC_Sale> GetByShippingCode(string shippingCode);

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
        /// 分页
        /// </summary>
        /// <param name="pagerRequest">分页请求参数</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="filter">筛选项</param>
        /// <param name="sortOrder">排序项</param>
        /// <returns></returns>
        List<SalesOrderModel> GetPagedList(PagerRequest pagerRequest, out int totalCount, SaleOrderFilter filter,
                                   SaleOrderSortOrder sortOrder);

        /// <summary>
        /// 获取指定条件的 opcsale
        /// </summary>
        /// <param name="salesOrderNos"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<OPC_Sale> GetListByNos(List<string> salesOrderNos, SaleOrderFilter filter);

        /// <summary>
        /// 获取销售单
        /// </summary>
        /// <param name="salesorderno"></param>
        /// <returns></returns>
        SalesOrderModel GetItemModel(string salesorderno);

        /// <summary>
        /// 更新收银状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        void Update4Cash(SalesOrderModel model, int userId);
    }
}