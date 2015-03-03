using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IRmaCommentRepository : IRepository<OPC_RMAComment>
    {
        IList<OPC_RMAComment> GetByRmaID(string rmaNo);
    }
}