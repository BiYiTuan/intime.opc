using System;
using System.Collections;
using System.Collections.Generic;

namespace Intime.OPC.DataService.Utilities
{
    /// <summary>
    /// The query executor.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class QueryExecutor<T> where T: new()
    {
        public IList<T> QueryAll(IPaging paging)
        {
            throw new NotImplementedException();
        }
    }
}
