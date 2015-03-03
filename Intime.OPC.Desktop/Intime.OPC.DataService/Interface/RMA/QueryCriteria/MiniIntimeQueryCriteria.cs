using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.DataService.Interface.RMA
{
    public class MiniIntimeQueryCriteria : QueryCriteria
    {
        public MiniIntimeQueryCriteria()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            OrderProductType = SalesOrderType.MiniIntime;
            Status = EnumRMAStatus.PrintRMA;
        }

        [UriParameter("orderproducttype")]
        public SalesOrderType OrderProductType { get; set; }

        [UriParameter("orderno")]
        public string OrderNo { get; set; }

        [UriParameter("receiptstartdate")]
        public DateTime StartDate { get; set; }

        [UriParameter("receiptenddate")]
        public DateTime EndDate { get; set; }

        [UriParameter("status")]
        public EnumRMAStatus Status { get; set; }
    }
}
