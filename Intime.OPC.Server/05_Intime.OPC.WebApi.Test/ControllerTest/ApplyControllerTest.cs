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
    public class ApplyControllerTest : BaseControllerTest
    {
        private ShopApplicationController _controller;

        public ShopApplicationController GetController()
        {
            _controller = GetInstance<ShopApplicationController>();

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


        [Flags]
        private enum MyEnum
        {
            None = 0,
            One = 1,
            Two = 2,
            Four = 4
        }

        [Test()]
        public void TestEnum1()
        {
            var a1 = MyEnum.One | MyEnum.Two | MyEnum.Four;
            var a2 = (MyEnum)7;
            Assert.IsTrue(((int)a1) == 7);
            Assert.IsTrue(((int)a2) == 7);

            a1 = a1 | MyEnum.Four;
            Assert.IsTrue(((int)a1) == 7);

            var a3 = MyEnum.One | MyEnum.Two;
            a3 = a3 | MyEnum.Four;
            Assert.IsTrue(((int)a3) == 7);

        }

        [Test()]
        public void TestEnum2()
        {
            var a1 = MyEnum.One | MyEnum.Two | MyEnum.Four;
            a1 = a1 & (~MyEnum.Four);
            Assert.IsTrue(((int)a1) == 3);

            var a2 = MyEnum.One;
            a2 = a2 & (~MyEnum.Four);
            Assert.IsTrue(((int)a2) == 1);

        }

        [Test()]
        public void TestEnum3()
        {
            var a1 = MyEnum.One | MyEnum.Two | MyEnum.Four;
            var r = (a1 & MyEnum.Four) != 0;
            Assert.IsTrue(r);

            var a2 = MyEnum.One;
            var r2 = (a2 & MyEnum.Four) != 0;
            Assert.IsTrue(!r2);

        }

        //[Test()]
        public void GetTest()
        {
            _controller.Request.Method = HttpMethod.Get;



        }

        [Test()]
        public void GetListTest()
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList(new ApplyQueryCriteriaRequest() { }, new UserProfile()) as OkNegotiatedContentResult<PagerInfo<ShopApplicationDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetTest([Values(0)]int applyId)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.Get(applyId, new UserProfile()) as OkNegotiatedContentResult<ShopApplicationDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(applyId == actual.Content.Id);

        }


        [Test()]
        public void PutApproved_OK_Test([Values(0)]int applyId)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.PutApproved(applyId, new ApplyApprovedRequest()
            {
                Approved = 1,
                Reason = "Ok",
            }, new UserProfile()) as OkNegotiatedContentResult<ShopApplicationDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(applyId == actual.Content.Id);

        }


        [Test()]
        public void PutApproved_No_Test([Values(0)]int applyId)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.PutApproved(applyId, new ApplyApprovedRequest()
            {
                Approved = 2,
                Reason = "不审批",
            }, new UserProfile()) as OkNegotiatedContentResult<ShopApplicationDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(applyId == actual.Content.Id);

        }

        [Test()]
        public void PutNotification_No_Test([Values(0)]int applyId)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.PutNotification(applyId, new ApplyNotifyRequest()
            {
                
            }
           , new UserProfile()) as OkNegotiatedContentResult<ShopApplicationDto>;

            Assert.IsNotNull(actual);

            Assert.IsTrue(applyId == actual.Content.Id);

        }
    }
}
