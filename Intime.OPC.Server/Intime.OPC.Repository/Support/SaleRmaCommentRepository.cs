using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    public class SaleRmaCommentRepository :BaseRepository<OPC_SaleRMAComment>,ISaleRmaCommentRepository
    {
        public IList<OPC_SaleRMAComment> GetByRmaID(string rmaNo)
        {
            return Select(t => t.RMANo == rmaNo);
        }
    }
}
