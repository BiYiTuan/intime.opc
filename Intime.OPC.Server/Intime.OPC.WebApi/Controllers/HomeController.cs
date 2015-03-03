using System.Web.Mvc;

namespace Intime.OPC.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "<h1>API Server V1.0</h1>";
        }
    }
}