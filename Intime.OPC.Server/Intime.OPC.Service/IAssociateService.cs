using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;

namespace Intime.OPC.Service
{
    public interface IAssociateService
    {
        ExectueResult<PagerInfo<AssociateDto>> GetPagedList(AssociateQueryRequest request);

        ExectueResult<AssociateDto> GetDto(int id);

        /// <summary>
        /// 设置降权
        /// </summary>
        /// <param name="request"></param>
        ExectueResult SetDemotion(SetAssociateOperateRequest request);
    }
}
