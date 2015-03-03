using System.Web.Mvc;

namespace Intime.OPC.WebApi
{
    /// <summary>
    ///     过滤器配置
    /// </summary>
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}