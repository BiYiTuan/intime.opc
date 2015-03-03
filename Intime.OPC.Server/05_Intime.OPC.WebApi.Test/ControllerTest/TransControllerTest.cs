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
    public class TransControllerTest : BaseControllerTest
    {
        private TransController _controller;

        public TransController GetController()
        {
            _controller = GetInstance<TransController>();

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
        public void GetShipping_OK()
        {
            _controller.Request.Method = HttpMethod.Post;
            var actual = _controller.GetShipping("", "", DateTime.Now.AddYears(-1), DateTime.Now, "", null, null, null, null, 1, 40
            , new UserProfile
            {
                Id = 9999

            }) as OkNegotiatedContentResult<PageResult<ShippingSaleDto>>;

            Assert.IsNotNull(actual);
        }

    }
}
