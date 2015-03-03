using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IRmaDetailRepository : IRepository<OPC_RMADetail>
    {
        PageResult<RmaDetail> GetByRmaNo(string rmaNo, int pageIndex, int pageSize);

        List<OPC_RMADetail> GetListByRmano(string rmano);
    }
}