using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Repository;

namespace Intime.OPC.Service.Impl
{
    public class AssociateService : IAssociateService
    {
        private readonly IAssociateRepository _repository;

        public AssociateService(IAssociateRepository repository)
        {
            _repository = repository;
        }

        public ExectueResult<PagerInfo<AssociateDto>> GetPagedList(AssociateQueryRequest request)
        {
            var dto = _repository.GetPagedList(request, request.PagerRequest);

            return new OkExectueResult<PagerInfo<AssociateDto>>(dto);
        }

        public ExectueResult<AssociateDto> GetDto(int id)
        {
            var dto = _repository.GetDto(id);

            return new OkExectueResult<AssociateDto>(dto);
        }

        /// <summary>
        /// 降权
        /// </summary>
        /// <param name="request"></param>
        public ExectueResult SetDemotion(SetAssociateOperateRequest request)
        {
            const UserOperatorRight operateRight = UserOperatorRight.GiftCard | UserOperatorRight.SystemProduct;
            request.OperateRight = operateRight;

            _repository.SetOperate(request);

            return new OkExectueResult();
        }
    }
}
