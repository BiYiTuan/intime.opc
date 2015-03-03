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
    public class AssociatesControllerTest : BaseControllerTest
    {
        private AssociatesController _controller;

        public AssociatesController GetController()
        {
            _controller = GetInstance<AssociatesController>();
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

            var actual = _controller.GetList(new AssociateQueryRequest { }, new UserProfile()) as OkNegotiatedContentResult<PagerInfo<AssociateDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetTest([Values(0)]int applyId)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.Get(applyId, new UserProfile()) as OkNegotiatedContentResult<AssociateDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(applyId == actual.Content.Id);

        }


        [Test()]
        public void PutDemotion_OK_Test([Values(0)]int applyId)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.PutDemotion(applyId, new SetAssociateOperateRequest
            {

            }, new UserProfile()) as OkNegotiatedContentResult<AssociateDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(applyId == actual.Content.Id);

        }


        [Test()]
        public void PutDemotion_No_Test([Values(0)]int applyId)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.PutDemotion(applyId, new SetAssociateOperateRequest
            {
            }, new UserProfile()) as OkNegotiatedContentResult<AssociateDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(applyId == actual.Content.Id);

        }
    }
}
