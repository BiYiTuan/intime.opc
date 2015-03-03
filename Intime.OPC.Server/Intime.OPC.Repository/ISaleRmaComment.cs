using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public  interface ISaleRmaCommentRepository:IRepository<OPC_SaleRMAComment>
    {
        IList<OPC_SaleRMAComment> GetByRmaID(string rmaNo);
    }
}
