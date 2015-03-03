using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Intime.OPC.Service;
using Intime.OPC.WebApi.Bindings;
using Intime.OPC.WebApi.Core;

namespace Intime.OPC.WebApi.Controllers
{
    public class ShipViaController : BaseController
    {
        private IShipViaService _shipViaService;
        public ShipViaController(IShipViaService shipViaService)
        {
            _shipViaService = shipViaService;
        }

        [HttpPost]
        public IHttpActionResult GetAll()
        {
            return DoFunction(()=>_shipViaService.GetAll(1,10000).Result, "获取物流信息失败");
            
        }
    }
}
