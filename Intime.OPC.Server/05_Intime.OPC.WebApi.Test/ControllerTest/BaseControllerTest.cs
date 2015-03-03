using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Http.Controllers;
using Intime.OPC.Service;
using Intime.OPC.WebApi.App_Start;
using System.Composition.Convention;
using Intime.OPC.WebApi.Core;
using NUnit.Framework;

namespace Intime.OPC.WebApi.Test.ControllerTest
{

    [TestFixture]
    public class BaseControllerTest
    {
        [TestFixtureSetUp]
        public static void ClassInit()
        {
            RegisterMefDependencyResolver();
            MapConfig.Config();
            AutoMapperConfig.Config();
        }

        [TestFixtureTearDown]
        public void ClassClear()
        {

        }

        private static void RegisterMefDependencyResolver()
        {
            /**----------------------------------------
             * -- ˵����������ݹ����Զ��ĵ������� 
             * -----------------------------------------
             * 1. ���еļ̳�IHttpController����
             * 2. �����ռ��а���.Support��������
             * ----------------------------------------
             */
            var conventions = new ConventionBuilder();

            // Export ����IHttpController������
            conventions.ForTypesDerivedFrom<IHttpController>()
                .Export();

            // Export namespace {*.Support.*}
            conventions.ForTypesMatching(t => t.Namespace != null &&
                                              (t.Namespace.EndsWith(".Support") || t.Namespace.Contains(".Support.") || t.Namespace.EndsWith("Impl") || t.Namespace.Contains(".Impl.")))
                .Export()
                .ExportInterfaces();


            var lstAssemlby = new List<Assembly>();


            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            foreach (var item in dir.GetFiles("Intime.OPC.*.dll"))
            {
                lstAssemlby.Add(Assembly.LoadFrom(item.FullName));
            }


            //lstAssemlby.Add(Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "\\Intime.OPC.Repository.dll"));
            //lstAssemlby.Add(Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "\\Intime.OPC.Service.dll"));
            lstAssemlby.Add(Assembly.GetExecutingAssembly());

            container = new ContainerConfiguration()
                .WithDefaultConventions(conventions)

                .WithAssemblies(lstAssemlby)
               .CreateContainer();

            //var a = container.GetExport<IRmaService>();
            //Console.WriteLine(a);


            // ����WebApi��DependencyResolver
            // GlobalConfiguration.Configuration.DependencyResolver = new MefDependencyResolver(container);

        }

        private static CompositionHost container;

        protected T GetInstance<T>()
        {
            return container.GetExport<T>();
        }

    }

    [TestFixture]
    public class BaseControllerTest<T> : BaseControllerTest where T : BaseController
    {
        protected T Controller;

        public T GetController()
        {
            Controller = GetInstance<T>();
            Controller.Request = new HttpRequestMessage();

            Controller.Request.SetConfiguration(new HttpConfiguration());

            return Controller;
        }

        [SetUp]
        public void TestInit()
        {
            Controller = GetController();
        }

        [TearDown]
        public void TestCleanUp()
        {
            Controller = null;
        }
    }
}