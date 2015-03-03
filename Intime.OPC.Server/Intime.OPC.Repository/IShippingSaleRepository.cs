using System;
using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.Partials.Models;

namespace Intime.OPC.Repository
{
    public interface IShippingSaleRepository : IRepository<OPC_ShippingSale>
    {
        OPC_ShippingSale GetBySaleOrderNo(string saleNo);

        PageResult<OPC_ShippingSale> GetByShippingCode(string shippingCode, int pageIndex, int pageSize = 20);


        PageResult<OPC_ShippingSale> Get(string shippingCode, DateTime startTime, DateTime endTime, int shippingStatus,
            int pageIndex, int pageSize = 20);

        /// <summary>
        /// 获得发货单
        /// </summary>
        /// <param name="saleOrderNo">销售单编号</param>
        /// <param name="expressNo">快递单号.</param>
        /// <param name="startGoodsOutDate">发货时间1</param>
        /// <param name="endGoodsOutDate">发货时间2</param>
        /// <param name="outGoodsCode">The out goods code.</param>
        /// <param name="sectionId">专柜ID</param>
        /// <param name="shippingStatus">The shipping status.</param>
        /// <param name="customerPhone">客户电话</param>
        /// <param name="brandId">品牌ID</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>PageResult{OPC_ShippingSale}.</returns>
        PageResult<OPC_ShippingSale> GetShippingSale(string saleOrderNo, string expressNo, DateTime startGoodsOutDate,
            DateTime endGoodsOutDate,
            string outGoodsCode, int sectionId, int shippingStatus, string customerPhone, int brandId, int pageIndex,
            int pageSize);

        OPC_ShippingSale GetByRmaNo(string rmaNo);

        PageResult<OPC_ShippingSale> GetByOrderNo(string orderNo, DateTime startDate, DateTime endDate, int pageIndex,
            int pageSize, int shippingStatus);



        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagerRequest">分页请求参数</param>
        /// <param name="totalCount">记录总数</param>
        /// <param name="filter">筛选项</param>
        /// <param name="sortOrder">排序项</param>
        /// <returns></returns>
        List<ShippingOrderModel> GetPagedList(PagerRequest pagerRequest, out int totalCount, ShippingOrderFilter filter,
                                   ShippingOrderSortOrder sortOrder);

        /// <summary>
        /// 获取 MODEL
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ShippingOrderModel GetItemModel(int id);

        /// <summary>
        /// 更新 物流信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId">操作人</param>
        /// <returns></returns>
        void Update4ShippingCode(OPC_ShippingSale entity, int userId);

        /// <summary>
        /// 创建出库单，
        /// </summary>
        /// <param name="entity">shipping</param>
        /// <param name="saleOrderModels">销售单</param>
        /// <param name="userId">操作人</param>
        /// <param name="shippingRemark">物流备注</param>
        /// <returns></returns>
        ShippingOrderModel CreateBySaleOrder(OPC_ShippingSale entity, List<OPC_Sale> saleOrderModels, int userId, string shippingRemark);

        /// <summary>
        /// 创建备注
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        OPC_ShippingSaleComment CreateComment(OPC_ShippingSaleComment entity, int userId);

        /// <summary>
        /// 修改备注
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        void UpdateComment(OPC_ShippingSaleComment entity, int userId);

        /// <summary>
        /// 修改打印次数 及设置状态
        /// </summary>
        /// <param name="id">shipping</param>
        /// <param name="times">增加的次数</param>
        /// <param name="userId">用户ID</param>
        void Update4Print(ShippingOrderModel model, Intime.OPC.Domain.Dto.Request.DeliveryOrderPrintRequest request, int userId);

        /// <summary>
        /// 同步状态
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        void Sync4Status(ShippingOrderModel model, int userId);
    }
}