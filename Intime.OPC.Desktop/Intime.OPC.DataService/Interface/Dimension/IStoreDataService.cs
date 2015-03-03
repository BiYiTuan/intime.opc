using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Interface
{
    public interface IStoreDataService : IBaseDataService<Store>
    {
        bool SetIsStop(int StoreId, bool isStop);
    }
}