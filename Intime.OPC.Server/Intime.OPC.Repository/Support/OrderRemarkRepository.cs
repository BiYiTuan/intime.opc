using System.Collections.Generic;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    public class OrderRemarkRepository : BaseRepository<OPC_OrderComment>, IOrderRemarkRepository
    {
        #region IOrderRemarkRepository Members

        public IList<OPC_OrderComment> GetByOrderNo(string orderNo)
        {
            return Select(t => t.OrderNo == orderNo);
        }

        #endregion
    }
}