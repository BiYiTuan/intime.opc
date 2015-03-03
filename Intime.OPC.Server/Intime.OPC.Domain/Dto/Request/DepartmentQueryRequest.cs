using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto.Request
{
    public class DepartmentQueryRequest : PageRequest,IStoreDataRoleRequest
    {
        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }

        public string NamePrefix { get; set; }

        public string Name { get; set; }
    }
}
