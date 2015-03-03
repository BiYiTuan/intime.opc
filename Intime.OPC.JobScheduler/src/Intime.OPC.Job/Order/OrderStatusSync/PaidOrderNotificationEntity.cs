using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    public class PaidOrderNotificationEntity : AbstractOrderNotificationEntity
    {
        public PaidOrderNotificationEntity(OPC_Sale saleOrder) : base(saleOrder)
        {
        }

        public override SaleOrderNotificationStatus Status
        {
            get { return SaleOrderNotificationStatus.Paid; }
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