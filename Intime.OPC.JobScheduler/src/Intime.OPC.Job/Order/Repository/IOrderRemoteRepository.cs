using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Order.DTO;

namespace Intime.OPC.Job.Order.Repository
{
    /// <summary>
    /// 数据接口
    /// </summary>
    public interface IOrderRemoteRepository
    {
        /// <summary>
        /// 根据Id获取订单状态 
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <returns>信息</returns>
        OrderStatusResultDto GetOrderStatusById(OPC_Sale saleOrder);

        OrderStatusResultDto GetRMAStatusById(OPC_RMA saleRMA);

    }
}
