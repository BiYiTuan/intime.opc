using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    public class CreateOrderNotificationEntity:AbstractOrderNotificationEntity
    {
        public CreateOrderNotificationEntity(OPC_Sale saleOrder) : base(saleOrder)
        {
        }

        public override SaleOrderNotificationStatus Status
        {
            get { return SaleOrderNotificationStatus.Create; }
        }

        public override SaleOrderNotificationType Type
        {
            get { return SaleOrderNotificationType.Create; }
        }

        public override string PaymentType
        {
            get { return "C0"; }
        }
    }
}