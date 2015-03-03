using System.Collections.Generic;

namespace Intime.OPC.Infrastructure.DataService
{
    public interface IFilter
    {
        IDictionary<string, object> GetFilter();
    }
}