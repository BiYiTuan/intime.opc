using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IAssociateRepository : IOPCRepository<int, IMS_Associate>
    {
        PagerInfo<AssociateDto> GetPagedList(AssociateQueryRequest request, Domain.PagerRequest pagerRequest);

        AssociateDto GetDto(int id);

        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="request"></param>
        void SetOperate(SetAssociateOperateRequest request);
    }
}
