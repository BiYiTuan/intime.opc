using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    public class SaleOrderStatusProcessorFactory
    {
        public static AbstractSaleOrderStatusProcessor Create(int status)
        {
            switch (status)
            {
                case 31:
                    return new CashedSaleOrderStatusProcessor(EnumSaleOrderStatus.None);
                case 32:
                    return new ShoppingGuidePickUpSaleOrderStatusProcessor(EnumSaleOrderStatus.ShoppingGuidePickUp);
                default: return new NoneOperationStatusProcessor(EnumSaleOrderStatus.None);
            }
        }
    }
}