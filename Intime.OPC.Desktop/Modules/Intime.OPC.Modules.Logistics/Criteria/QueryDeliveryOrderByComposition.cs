using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Logistics.Criteria
{
    public class QueryDeliveryOrderByComposition : QueryCriteria
    {
        public QueryDeliveryOrderByComposition()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        [UriParameter("startdate")]
        public DateTime StartDate { get; set; }

        [UriParameter("enddate")]
        public DateTime EndDate { get; set; }

        [UriParameter("orderno")]
        public string OrderNo { get; set; }

        [UriParameter("status")]
        public EnumSaleOrderStatus Status { get; set; }
    }
}
