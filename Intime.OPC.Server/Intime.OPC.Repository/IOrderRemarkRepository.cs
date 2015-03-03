using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IOrderRemarkRepository : IRepository<OPC_OrderComment>
    {
        IList<OPC_OrderComment> GetByOrderNo(string orderNo);
    }
}