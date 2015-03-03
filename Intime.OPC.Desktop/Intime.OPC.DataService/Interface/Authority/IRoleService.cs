using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Interface
{
    public interface IRoleService : IBaseDataService<OPC_AuthRole>
    {
        bool SetIsEnable(OPC_AuthRole role);
    }
}