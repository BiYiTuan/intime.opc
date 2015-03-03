using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;

namespace Intime.OPC.Service.Support
{
    public class ShipViaService : BaseService<ShipVia>, IShipViaService
    {
        public ShipViaService(IShipViaRepository shipViaRepository) : base(shipViaRepository)
        {
        }

        #region IShipViaService Members

        public PageResult<ShipVia> GetAll(int pageIndex, int pageSize = 20)
        {
            return _repository.GetAll(pageIndex, pageSize);
        }

        #endregion
    }
}