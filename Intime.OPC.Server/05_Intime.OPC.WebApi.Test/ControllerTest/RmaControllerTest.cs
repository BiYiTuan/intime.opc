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
using Intime.OPC.Service;
using Intime.OPC.Service.Support;
using Intime.OPC.WebApi.Controllers;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using NUnit.Framework;

namespace Intime.OPC.WebApi.Test.ControllerTest
{

    [TestFixture]
    public class RmaControllerTest : BaseControllerTest
    {
        private RMAController _controller;

        public RMAController GetController()
        {
            _controller = GetInstance<RMAController>();

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
        public void GetListTest_OK()
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.GetList(new RmaQueryRequest()
            {

            }, new UserProfile()
            {
                Id = 9999

            }) as OkNegotiatedContentResult<PagerInfo<RMADto>>;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void PutCashNo_OK([Values("1140423109330002")]string rmano)
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.PutCashNo(rmano, new RmaSetCashNoRequest()
            {
                CashNo = "wark_hard_test_cashno2"
            }, new UserProfile()
            {
                Id = 9999

            }) as OkResult;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void PutReceipt_OK([Values("1140423109330002")]string rmano)
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.PutReceipt(rmano, new RmaReceiptRequest()
            {
                IsConfirm = true
            }, new UserProfile()
            {
                Id = 9999

            }) as OkResult;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void PutReturnOfGoods_OK([Values("1140423109330002")]string rmano)
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.PutReturnOfGoods(rmano, new RmaReturnOfGoodsRequest()
            {
                IsConfirm = true
            }, new UserProfile()
            {
                Id = 9999

            }) as OkResult;

            Assert.IsNotNull(actual);
        }
    }
}
