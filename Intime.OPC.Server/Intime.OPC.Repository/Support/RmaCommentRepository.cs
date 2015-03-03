using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    public  class RmaCommentRepository : BaseRepository<OPC_RMAComment>, IRmaCommentRepository
    {
        public IList<OPC_RMAComment> GetByRmaID(string rmaNo)
        {
            return Select(t => t.RMANo == rmaNo);
        }
    }
}
