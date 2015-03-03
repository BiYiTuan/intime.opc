using Intime.OPC.Domain.Enums;
using Intime.OPC.Job.Order.DTO;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    public abstract class AbstractSaleOrderStatusProcessor
    {
        protected EnumSaleOrderStatus _status;
        protected AbstractSaleOrderStatusProcessor(EnumSaleOrderStatus status)
        {
            this._status = status;
        }
        public abstract void Process(string saleOrderNo, OrderStatusResultDto statusResult);
    }
}