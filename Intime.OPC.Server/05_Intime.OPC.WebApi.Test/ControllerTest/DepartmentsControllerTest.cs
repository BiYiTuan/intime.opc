using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.WebApi.Controllers;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using NUnit.Framework;

namespace Intime.OPC.WebApi.Test.ControllerTest
{

    [TestFixture]
    public class DepartmentsControllerTest : BaseControllerTest
    {
        private DepartmentsController _controller;

        public DepartmentsController GetController()
        {
            _controller = GetInstance<DepartmentsController>();

            _controller.Request = new HttpRequestMessage();
            _controller.Request.SetConfiguration(new HttpConfiguration());

            return _controller;
        }

        [SetUp]
        public void TestInit()
        {
            _controller = GetController();
        }

        [TearDown]
        public void TestCleanUp()
        {
            _controller = null;
        }


        [Test()]
        public void GetListTest()
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList(new DepartmentQueryRequest(),  new UserProfile()) as OkNegotiatedContentResult<PagerInfo<DepartmentDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetTest([Values(0)]int applyId)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.Get(applyId, new UserProfile()) as OkNegotiatedContentResult<DepartmentDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(applyId == actual.Content.Id);

        }
    }
}
