using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface ISaleRemarkRepository : IRepository<OPC_SaleComment>
    {
        IList<OPC_SaleComment> GetBySaleOrderNo(string saleOrderNo);
    }
}