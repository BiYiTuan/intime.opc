using Intime.OPC.Job.Order.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Job.RMASync
{
    public class NoneOperationRMAStatusProcessor :AbstractRMASaleStatusProcessor
    {
        private Domain.Enums.EnumRMAStatus enumRMASaleStatus;

        public NoneOperationRMAStatusProcessor(Domain.Enums.EnumRMAStatus enumRMASaleStatus) : base(enumRMASaleStatus) { }

        public override void Process(string RMANo, OrderStatusResultDto statusResult)
        {
            
        }
    }
}
