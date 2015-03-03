using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Exception;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;

namespace Intime.OPC.WebApi.Controllers
{
#if DEBUG
    [RoutePrefix("api/tests")]
    public class TestController : BaseController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetList([UserId] int userId)
        {
            throw new OpcException("测试的");
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int? id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("a")]
        public IHttpActionResult GetLista([UserId] int userId)
        {
            throw new SaleOrderNotExistsException("测试的");
        }

    }
#endif
}
