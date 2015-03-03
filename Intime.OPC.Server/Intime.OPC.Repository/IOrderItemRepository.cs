using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        PageResult<OrderItemDto> GetByOrderNo(string orderNo, int pageIndex, int pageSize);

        /// <summary>
        /// 通过IDs 获得多个实体
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>IList{OrderItem}.</returns>
        IList<OrderItem> GetByIDs(IEnumerable<int> ids);

        SaleDetailStatListDto WebSiteStatSaleDetail(SearchStatRequest request);
        ReturnGoodsStatListDto WebSiteStatReturnGoods(SearchStatRequest request);
        CashierList WebSiteCashier(SearchCashierRequest request);
        PageResult<OrderItemDto> GetOrderItemsAutoBack(string orderNo, int pageIndex, int pageSize);
        void SetSaleOrderVoid(string saleOrderNo);


        /// <summary>
        /// 销售单
        /// </summary>
        /// <param name="request"></param>
        PagedSaleDetailStatListDto WebSiteStatSaleDetailPaged(SearchStatRequest request);

        /// <summary>
        /// RMA
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagedReturnGoodsStatListDto WebSiteStatReturnGoodsPaged(SearchStatRequest request);

        /// <summary>
        /// 收银
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagedCashierList WebSiteCashierPaged(SearchCashierRequest request);

        /// <summary>
        /// 销售单 统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagerInfo<SaleDetailStatDto> GetPagedList4SaleStat(SearchStatRequest request);


        /// <summary>
        /// RMA 明细 统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagerInfo<ReturnGoodsStatDto> GetPagedList4RmaStat(SearchStatRequest request);


        /// <summary>
        /// 收银 明细统计
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PagerInfo<WebSiteCashierSearchDto> GetPagedList4CashierStat(SearchCashierRequest request);
    }
}