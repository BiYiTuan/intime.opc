using System.ComponentModel.Composition;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Dimension.Common;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Modules.Dimension.Services
{
    [Export(typeof(IService<Counter>))]
    public class CounterService : ServiceBase<Counter>
    {
        public override Counter Create(Counter obj)
        {
            if (obj.Store != null) obj.StoreId = obj.Store.Id;
            return base.Create(obj);
        }

        public override Counter Update(Counter obj)
        {
            if (obj.Store != null) obj.StoreId = obj.Store.Id;
            return base.Update(obj);
        }
    }
}
