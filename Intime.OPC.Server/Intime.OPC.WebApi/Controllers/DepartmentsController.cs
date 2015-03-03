using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums.SortOrder;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace Intime.OPC.WebApi.Controllers
{
    [RoutePrefix("api/departments")]
    public class DepartmentsController : BaseController
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }


        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id, [UserProfile] UserProfile userProfile)
        {
            var dto = _departmentRepository.GetDto(id);

            var r = CheckRole4Store(userProfile, dto.StoreId);

            return !r.Result ? BadRequest(r.Error) : RetrunHttpActionResult(dto);
        }

        // [ActionName("list")]/brand?page=1,2&sortorder=0&prekeyname=11&storeid=1&status=2
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetList([FromUri]DepartmentQueryRequest request, [UserProfile] UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var dto = _departmentRepository.GetPagedList(request.PagerRequest, request);

            return RetrunHttpActionResult(dto);
        }
    }
}
