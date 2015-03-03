using System.Linq;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.Filters;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using System.Web.Http;

namespace Intime.OPC.WebApi.Controllers
{
    public class StoreController : BaseController
    {
        private readonly IStoreService _service;
        public StoreController(IStoreService service)
        {
            _service = service;
        }

        [HttpPost]
        public IHttpActionResult GetAll([UserProfile] UserProfile userProfile)
        {
            //return GetList(new StoreRequest(20000)
            //    {
            //        Page = 1,
            //        PageSize = 20000
            //    }, userProfile);

            return DoFunction(() => _service.GetAll(), "获得门店信息失败");
        }

        [Route("api/stores")]
        [HttpGet]
        public IHttpActionResult GetList([FromUri]StoreRequest request, [UserProfile] UserProfile userProfile)
        {
            if (request == null)
            {
                request = new StoreRequest();
            }
            request.ArrangeParams();
            request.DataStoreIds = userProfile.StoreIds == null ? null : userProfile.StoreIds.ToList();

            var dto = _service.GetPagedList(request);

            return RetrunHttpActionResult(dto);
        }

        [HttpGet]
        [Route("api/stores/{id:int}")]
        public IHttpActionResult Get(int id, [UserProfile] UserProfile userProfile)
        {
            var result = CheckRole4Store(userProfile, id);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            var dto = _service.GetItem(id);

            return RetrunHttpActionResult(dto);
        }

        [HttpPut]
        [ModelValidationFilter]
        [Route("api/stores/{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]StoreDto dto, [UserProfile] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = CheckRole4Store(userProfile, id);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            var item = _service.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            dto.Id = id;

            _service.Update(dto, userProfile.Id);

            return Get(id, userProfile);
        }

        [HttpDelete]
        [Route("api/stores/{id:int}")]
        public IHttpActionResult Delete(int id, [UserProfile] UserProfile userProfile)
        {
            var result = CheckRole4Store(userProfile, id);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            var item = _service.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Status = -1;

            _service.Update(item, userProfile.Id);

            return RetrunHttpActionResult("OK");
        }

        [HttpPost]
        [ModelValidationFilter]
        [Route("api/stores")]
        public IHttpActionResult Post([FromBody]StoreDto dto, [UserProfile] UserProfile userProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = CheckRole4Store(userProfile, null);
            if (!result.Result)
            {
                return BadRequest(result.Error);
            }

            var item = _service.Insert(dto, userProfile.Id);

            return RetrunHttpActionResult(item);
        }
    }
}
