using Intime.OPC.Domain.Enums;
using Intime.OPC.Job.Order.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Job.RMASync
{
    public abstract class AbstractRMASaleStatusProcessor
    {
        protected EnumRMAStatus _status;
        protected AbstractRMASaleStatusProcessor(EnumRMAStatus status)
        {
            this._status = status;
        }
        public abstract void Process(string RMANo, OrderStatusResultDto statusResult);

    }
}
