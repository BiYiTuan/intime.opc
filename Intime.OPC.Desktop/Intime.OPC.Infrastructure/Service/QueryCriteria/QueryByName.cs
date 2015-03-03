using Intime.OPC.Infrastructure;
using Intime.OPC.Domain.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Service
{
    public class QueryByName : QueryCriteria
    {
        [UriParameter("nameprefix")]
        public string Name { get; set; }
    }
}
