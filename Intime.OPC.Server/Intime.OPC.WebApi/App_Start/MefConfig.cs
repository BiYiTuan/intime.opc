using System.Composition.Convention;
using System.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Http.Controllers;
using Intime.OPC.WebApi.Core.DependencyResolver.MEF;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace Intime.OPC.WebApi
{
    /// <summary>
    ///     MEF配置管理
    /// </summary>
    public static class MefConfig
    {
        public static void RegisterMefDependencyResolver()
        {
            /**----------------------------------------
             * -- 说明：程序根据规则自动的导出部件 
             * -----------------------------------------
             * 1. 所有的继承IHttpController的类
             * 2. 命名空间中包含.Support的所有类
             * ----------------------------------------
             */
            var conventions = new ConventionBuilder();

            // Export 所有IHttpController到容器
            conventions.ForTypesDerivedFrom<IHttpController>()
                .Export();

            // Export namespace {*.Support.*}
            conventions.ForTypesMatching(t => t.Namespace != null &&
                                              (t.Namespace.EndsWith(".Support") || t.Namespace.Contains(".Support.") || t.Namespace.EndsWith("Impl") || t.Namespace.Contains(".Impl.")))
                .Export()
                .ExportInterfaces();

            CompositionHost container = new ContainerConfiguration()
                .WithAssemblies(BuildManager.GetReferencedAssemblies().Cast<Assembly>(), conventions)
                .CreateContainer();


            // 设置WebApi的DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new MefDependencyResolver(container);
        }
    }
}