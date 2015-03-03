using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface ISaleDetailRepository : IRepository<OPC_SaleDetail>
    {
        /// <summary>
        ///     根据销售单号获得销售单明细
        /// </summary>
        /// <param name="saleOrderNo">The sale order no.</param>
        /// <returns>IList{OPC_SaleDetail}.</returns>
        PageResult<OPC_SaleDetail> GetBySaleOrderNo(string saleOrderNo, int pageIndex, int pageSize);

        /// <summary>
        ///     根据订单号获得销售单明细
        /// </summary>
        /// <param name="orderNo">The order no.</param>
        /// <returns>IList{OPC_SaleDetail}.</returns>
        PageResult<OPC_SaleDetail> GetByOrderNo(string orderNo, int pageIndex, int pageSize);
    }
}