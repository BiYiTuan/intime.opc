using System;

namespace Intime.OPC.Domain.BusinessModel
{
    public class DateRangeFilter
    {
        public DateTime? StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }
    }
}