using Intime.OPC.Domain.Attributes;
using Intime.OPC.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Dimension.Criteria
{
    public class QueryDepartmentByStoreId : QueryCriteria
    {
        [UriParameter("storeid")]
        public int StoreId { get; set; }
    }
}
