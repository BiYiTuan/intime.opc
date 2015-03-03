using System;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Modules.Finance.Criteria
{
    public class CashedCommisionStatisticsQueryCriteria : QueryCriteria
    {
        public CashedCommisionStatisticsQueryCriteria()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            StoreId = -1;
        }

        [UriParameter("storeid")]
        public int StoreId { get; set; }

        [UriParameter("sectioncode")]
        public string SectionCode { get; set; }

        [UriParameter("PickUpStartDate")]
        public DateTime StartDate { get; set; }

        [UriParameter("PickUpEndDate")]
        public DateTime EndDate { get; set; }

        [UriParameter("bankcode")]
        public string BankCode { get; set; }
    }
}
