using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Job.RMASync
{
    public class RMAStatusProcessorFactory
    {
        public static AbstractRMASaleStatusProcessor Create(int status)
        {
            switch (status)
            {
                case 31:
                    return new CashedRMASaleStatusProcessor(EnumRMAStatus.None);
                case 32:
                    return new ShoppingGuidePickUpRMASaleStatusProcessor(EnumRMAStatus.ShoppingGuideReceive);
                default: return new NoneOperationRMAStatusProcessor(EnumRMAStatus.None);
            }
        }
    }
}
