using Intime.OPC.Domain.Attributes;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Modules.Dimension.Criteria
{
    public class QueryCounterByComposition : QueryCriteria
    {
        [UriParameter("storeid")]
        public int? StoreId { get; set; }

        [UriParameter("name")]
        public string Name { get; set; }

        [UriParameter("sectioncode")]
        public string SectionCode { get; set; }
    }
}
