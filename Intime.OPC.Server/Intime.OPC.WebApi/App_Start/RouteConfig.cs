using System.Web.Mvc;
using System.Web.Routing;

namespace Intime.OPC.WebApi
{
    /// <summary>
    ///     路由配置，目前暂时没有使用到
    /// </summary>
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}