using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Service
{
    public interface IQueryCriteria
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }

        string BuildQueryString();
    }
}
