using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Repository;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using System.Web.Http;

namespace Intime.OPC.WebApi.Controllers
{
    [RoutePrefix("api/banks")]
    public class BankController : BaseController
    {
        private readonly IBankRepository _bankRepository;

        public BankController(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetList([FromUri] BankQueryRequest request, [UserProfile] UserProfile userProfile)
        {
            request.ArrangeParams();

            var rst = _bankRepository.GetPagedList(request, request.PagerRequest);

            return RetrunHttpActionResult(rst);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id, [UserProfile] UserProfile userProfile)
        {
            var rst = _bankRepository.GetDto(id);

            return RetrunHttpActionResult(rst);
        }
    }
}
