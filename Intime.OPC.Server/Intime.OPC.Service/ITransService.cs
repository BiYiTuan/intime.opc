using System;
using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    public interface ITransService : IService
    {

        PageResult<OPC_Sale> Select(DateTime startDate, DateTime endDate, string orderNo, string saleOrderNo,int pageIndex,int pageSize=20);

        IList<OPC_SaleDetail> SelectSaleDetail(IEnumerable<string> saleIDs);

        /// <summary>
        /// 增加订单日志
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AddOrderComment(OPC_OrderComment comment);

        /// <summary>
        /// Gets the shipping sale by order no.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <returns>IList{ShippingSaleDto}.</returns>
        ShippingSaleDto GetShippingSaleBySaleNo(string saleNo);

        /// <summary>
        /// 新增发货单备注
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AddShippingSaleComment(OPC_ShippingSaleComment comment);

        /// <summary>
        /// 读取发货单备注
        /// </summary>
        /// <param name="shippingCode">发货单编号</param>
        /// <returns>IList{OPC_ShippingSaleComment}.</returns>
        IList<OPC_ShippingSaleComment> GetByShippingCommentCode(string shippingCode);

        /// <summary>
        /// Creates the shipping sale.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="shippingSaleDto">The shipping sale dto.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool CreateShippingSale(int userId, ShippingSaleCreateDto shippingSaleDto);

        /// <summary>
        /// 查询快递单
        /// </summary>
        /// <param name="shippingCode">The shipping code.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns>IList{ShippingSaleDto}.</returns>
        PageResult<ShippingSaleDto> GetShippingSale(string shippingCode, System.DateTime startTime, System.DateTime endTime,int pageIndex,int pageSize=20);

        /// <summary>
        /// 通过快递单号 获得销售单
        /// </summary>
        /// <param name="shippingSaleNo">The shipping sale no.</param>
        /// <returns>IList{SaleDto}.</returns>
        IList<SaleDto> GetSaleByShippingSaleNo(string shippingSaleNo);
        /// <summary>
        /// Gets the sale order pickup.
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="userid">The userid.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>PageResult{SaleDto}.</returns>
        PageResult<SaleDto> GetSaleOrderPickup(string orderNo, string saleOrderNo, DateTime startDate, DateTime endDate, int userid, int pageIndex, int pageSize);

        /// <summary>
        /// 查询发货单
        /// </summary>
        /// <param name="saleOrderNo">销售单</param>
        /// <param name="expressNo">The express no.</param>
        /// <param name="startGoodsOutDate">The start goods out date.</param>
        /// <param name="endGoodsOutDate">The end goods out date.</param>
        /// <param name="outGoodsCode">The out goods code.</param>
        /// <param name="sectionId">The section identifier.</param>
        /// <param name="shippingStatus">The shipping status.</param>
        /// <param name="customerPhone">The customer phone.</param>
        /// <param name="brandId">The brand identifier.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>PageResult{SaleDto}.</returns>
        PageResult<ShippingSaleDto> GetShippingSale(string saleOrderNo, string expressNo, DateTime startGoodsOutDate, DateTime endGoodsOutDate, string outGoodsCode, int sectionId, int shippingStatus, string customerPhone, int brandId, int pageIndex, int pageSize);
    }
}