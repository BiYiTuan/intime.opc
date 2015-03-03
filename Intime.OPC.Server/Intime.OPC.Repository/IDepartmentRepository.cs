using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IDepartmentRepository : IOPCRepository<int, Department>, IRepository<Department>
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pagerRequest">分页请求参数</param>
        /// <param name="request"></param>
        /// <returns></returns>
        PagerInfo<DepartmentDto> GetPagedList(PagerRequest pagerRequest, DepartmentQueryRequest request);

        DepartmentDto GetDto(int id);
    }
}
