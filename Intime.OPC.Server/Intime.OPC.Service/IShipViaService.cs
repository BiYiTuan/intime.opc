
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    public interface IShipViaService : IService
    {
        PageResult<ShipVia> GetAll(int pageIndex, int pageSize = 20);
    }
}
