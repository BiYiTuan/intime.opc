using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using System.Collections.Generic;
using Intime.OPC.Domain.Partials.Models;

namespace Intime.OPC.Repository
{
    //public interface IShippingOrderRepository : IOPCRepository<int, OPC_ShippingSale>
    //{
    //    /// <summary>
    //    /// 分页
    //    /// </summary>
    //    /// <param name="pagerRequest">分页请求参数</param>
    //    /// <param name="totalCount">记录总数</param>
    //    /// <param name="filter">筛选项</param>
    //    /// <param name="sortOrder">排序项</param>
    //    /// <returns></returns>
    //    List<ShippingOrderModel> GetPagedList(PagerRequest pagerRequest, out int totalCount, ShippingOrderFilter filter,
    //                               ShippingOrderSortOrder sortOrder);

    //    /// <summary>
    //    /// 获取 MODEL
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    ShippingOrderModel GetItemModel(int id);

    //    /// <summary>
    //    /// 更新 物流信息
    //    /// </summary>
    //    /// <param name="entity"></param>
    //    /// <param name="userId">操作人</param>
    //    /// <returns></returns>
    //    void Update4ShippingCode(OPC_ShippingSale entity, int userId);

    //    /// <summary>
    //    /// 创建出库单，
    //    /// </summary>
    //    /// <param name="entity">shipping</param>
    //    /// <param name="saleOrderModels">销售单</param>
    //    /// <param name="userId">操作人</param>
    //    /// <param name="shippingRemark">物流备注</param>
    //    /// <returns></returns>
    //    ShippingOrderModel CreateBySaleOrder(OPC_ShippingSale entity, List<OPC_Sale> saleOrderModels, int userId, string shippingRemark);

    //    /// <summary>
    //    /// 创建备注
    //    /// </summary>
    //    /// <param name="entity"></param>
    //    /// <param name="userId"></param>
    //    /// <returns></returns>
    //    OPC_ShippingSaleComment CreateComment(OPC_ShippingSaleComment entity, int userId);

    //    /// <summary>
    //    /// 修改备注
    //    /// </summary>
    //    /// <param name="entity"></param>
    //    /// <param name="userId"></param>
    //    /// <returns></returns>
    //    void UpdateComment(OPC_ShippingSaleComment entity, int userId);

    //    /// <summary>
    //    /// 修改打印次数 及设置状态
    //    /// </summary>
    //    /// <param name="id">shipping</param>
    //    /// <param name="times">增加的次数</param>
    //    /// <param name="userId">用户ID</param>
    //    void Update4Print(ShippingOrderModel model, Intime.OPC.Domain.Dto.Request.DeliveryOrderPrintRequest request, int userId);

    //    /// <summary>
    //    /// 同步状态
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <param name="userId"></param>
    //    void Sync4Status(ShippingOrderModel model, int userId);
    //}
}
