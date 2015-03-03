using System.Collections.Generic;

namespace Intime.OPC.Infrastructure.DataService
{
    public interface IBaseDataService<T>
    {
        ResultMsg Add(T model);
        ResultMsg Edit(T model);
        ResultMsg Delete(T model);
        PageResult<T> Search(IDictionary<string, object> iDicFilter);
    }
}