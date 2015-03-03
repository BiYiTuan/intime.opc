using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.RMASync
{
    public class PaidRMANotificationEntity : AbstractRMANotificationEntity
    {
        public PaidRMANotificationEntity(OPC_RMA saleRMA) : base(saleRMA)
        {
        }

        public override NotificationStatus Status
        {
            get { return NotificationStatus.Paid; }
        }

        public override NotificationType Type
        {
            get { return NotificationType.RMA; }
        }

        public override string PaymentType
        {
            get { return "C0"; }
        }
    }
}