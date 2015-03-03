
using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using System;

namespace Intime.OPC.Repository
{
    //[System.Obsolete("salerma 过期 请使用rma")]
    public interface ISaleRMARepository:IRepository<OPC_SaleRMA>
    {
        /// <summary>
        /// 统计同一销售单的数量
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <returns>System.Int32.</returns>
        int Count(string saleOrderNo);

        PageResult<SaleRmaDto> GetAll(string orderNo, string payType, int? bandId, System.DateTime startTime, System.DateTime endTime, string telephone,int pageIndex,int pageSize);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="payType">Type of the pay.</param>
        /// <param name="rmaNo">The rma no.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <param name="rmaStatus">The rma status.</param>
        /// <param name="storeId">The store identifier.</param>
        /// <param name="rmaStatus">The rma status.</param>
        /// <param name="returnGoodsStatus">The return goods status.</param>
        /// <returns>IList{SaleRmaDto}.</returns>
        PageResult<SaleRmaDto> GetAll(string orderNo, string saleOrderNo, string payType, string rmaNo, System.DateTime startTime, System.DateTime endTime, int? rmaStatus, int? storeId, EnumReturnGoodsStatus returnGoodsStatus, int pageIndex, int pageSize);
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
        PageResult<SaleRmaDto> GetByReturnGoodPay(string orderNo, string saleOrderNo, string payType, string rmaNo, DateTime startTime, DateTime endTime, int? storeId, int pageIndex, int pageSize);

        OPC_SaleRMA GetByRmaNo(string rmaNo);
        PageResult<SaleRmaDto> GetOrderAutoBack(ReturnGoodsRequest request);
        void SetVoidBySaleOrder(string saleOrderNo);
    }
}
