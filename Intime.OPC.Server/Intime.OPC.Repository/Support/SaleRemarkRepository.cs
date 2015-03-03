using System.Collections.Generic;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    public class SaleRemarkRepository : BaseRepository<OPC_SaleComment>, ISaleRemarkRepository
    {
        #region ISaleRemarkRepository Members

        public IList<OPC_SaleComment> GetBySaleOrderNo(string saleOrderNo)
        {
            return Select(t => t.SaleOrderNo == saleOrderNo);
        }

        #endregion
    }
}