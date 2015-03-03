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
    public class BanksControllerTest : BaseControllerTest<BankController>
    {
        [Test()]
        public void GetListTest()
        {
            Controller.Request.Method = HttpMethod.Get;

            var actual = Controller.GetList(new BankQueryRequest(), new UserProfile()) as OkNegotiatedContentResult<PagerInfo<BankDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetTest([Values(4)]int bankId)
        {
            Controller.Request.Method = HttpMethod.Get;

            var actual = Controller.Get(bankId, new UserProfile()) as OkNegotiatedContentResult<BankDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(bankId == actual.Content.Id);
        }
    }
}
