using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace Intime.OPC.WebApi.Controllers
{
    [RoutePrefix("api/adminusers")]
    public class AdminUsersController : BaseController
    {
        [HttpGet]
        [Route("logininfo")]
        public IHttpActionResult GetLoginInfo([UserProfile] UserProfile userProfile)
        {
            return RetrunHttpActionResult(userProfile);
        }
    }
}
