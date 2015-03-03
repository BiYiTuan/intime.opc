using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IBankRepository : IOPCRepository<int, IMS_Bank>
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request"></param>
        /// <param name="pagerRequest"></param>
        /// <returns></returns>
        PagerInfo<BankDto> GetPagedList(BankQueryRequest request, PagerRequest pagerRequest);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BankDto GetDto(int id);
    }
}
