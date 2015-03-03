using System;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using System.Web.Http;

namespace Intime.OPC.WebApi.Controllers
{

    [RoutePrefix("api/associates")]
    public class AssociatesController : BaseController
    {
        private readonly IAssociateService _service;
        public AssociatesController(IAssociateService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取LIST
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetList([FromUri]AssociateQueryRequest request, [UserProfile] UserProfile userProfile)
        {
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            var exectueResult = _service.GetPagedList(request);

            return RetrunHttpActionResult4ExectueResult(exectueResult);
        }

        /// <summary>
        /// 获取 ITEM
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id, [UserProfile] UserProfile userProfile)
        {
            var result = _service.GetDto(id);

            if (!result.IsSuccess) return BadRequest(result.Message);

            if (result.Data == null)
            {
                return NotFound();
            }
            var r = CheckRole4Store(userProfile, result.Data.StoreId);

            return !r.Result ? BadRequest(r.Error) : RetrunHttpActionResult(result.Data);
        }

        /// <summary>
        /// 降权
        /// </summary>
        /// <param name="request"></param>
        /// <param name="id"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/demotion")]
        public IHttpActionResult PutDemotion(int id, [FromBody]SetAssociateOperateRequest request, [UserProfile] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (request == null)
            {
                request = new SetAssociateOperateRequest();
            }
            IHttpActionResult httpActionResult;
            var result = CheckDataRoleAndArrangeParams(request, userProfile, out httpActionResult);
            if (!result)
            {
                return httpActionResult;
            }

            request.AssociateId = id;
            var s = _service.GetDto(id);
            if (s == null)
            {
                return BadRequest(String.Format("合伙人Id:({0})未找到", id));
            }

            var r = CheckRole4Store(userProfile, s.Data.StoreId);

            if (!r.Result)
            {
                BadRequest(r.Error);
            }

            var exectueResult = _service.SetDemotion(request);

            return RetrunHttpActionResult(exectueResult);
        }
    }
}
