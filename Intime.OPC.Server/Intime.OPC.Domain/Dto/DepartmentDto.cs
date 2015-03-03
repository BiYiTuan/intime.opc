using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int SortOrder { get; set; }
    }
}
