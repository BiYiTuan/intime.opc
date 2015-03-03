using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Logistics.Criteria
{
    public class QuerySalesOrderByDeliveryOrderId : QueryCriteria
    {
        [UriParameter("deliveryorderid")]
        public int DeliveryOrderId { get; set; }
    }
}
