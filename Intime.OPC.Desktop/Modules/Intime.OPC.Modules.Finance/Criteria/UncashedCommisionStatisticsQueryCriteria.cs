using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Modules.Finance.Criteria
{
    public class UncashedCommisionStatisticsQueryCriteria : QueryCriteria
    {
        public UncashedCommisionStatisticsQueryCriteria()
        {
            StoreId = -1;
        }

        [UriParameter("storeid")]
        public int StoreId { get; set; }

        [UriParameter("sectioncode")]
        public string SectionCode { get; set; }

        [UriParameter("contact")]
        public string Contact { get; set; }

        [UriParameter("minisilverno")]
        public string MiniIntimeAccountNo { get; set; }
    }
}
