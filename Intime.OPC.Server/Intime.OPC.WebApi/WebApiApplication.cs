using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Intime.OPC.Service;
using Intime.OPC.WebApi.App_Start;

namespace Intime.OPC.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("~/Config/log4net.config")));

            // MEF配置
            MefConfig.RegisterMefDependencyResolver();

            AreaRegistration.RegisterAllAreas();

            // WebApi配置
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            // 全局过滤器注册
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // 路由注册
            RouteConfig.RegisterRoutes(RouteTable.Routes);


            //初始化映射
            MapConfig.Config();

            AutoMapperConfig.Config();

            FormattersConfig.Config();
        }
    }
}