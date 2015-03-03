using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Modules.Logistics.Criteria
{
    public class QuerySalesOrderByComposition : QueryCriteria
    {
        public QuerySalesOrderByComposition()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            Status = EnumSaleOrderStatus.ShipInStorage;
        }

        [UriParameter("startdate")]
        public DateTime StartDate { get; set; }

        [UriParameter("enddate")]
        public DateTime EndDate { get; set; }

        [UriParameter("orderno")]
        public string OrderNo { get; set; }

        [UriParameter("salesorderno")]
        public string SalesOrderNo { get; set; }

        [UriParameter("status")]
        public EnumSaleOrderStatus Status { get; set; }
    }
}
