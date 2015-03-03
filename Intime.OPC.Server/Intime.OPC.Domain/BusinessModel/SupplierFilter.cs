using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Enums.SortOrder;


namespace Intime.OPC.Domain.BusinessModel
{
    public class SupplierFilter : PageFilter
    {
        public string NamePrefix { get; set; }

        public int? Status { get; set; }

        public SupplierSortOrder? SortOrder { get; set; }

        public string Name { get; set; }
    }


    public class StoreFilter : PageFilter
    {
        public string NamePrefix { get; set; }

        public int? Status { get; set; }

        public StoreSortOrder? SortOrder { get; set; }

        public string Name { get; set; }

        public List<int> DataStoreIds { get; set; }
    }
}
