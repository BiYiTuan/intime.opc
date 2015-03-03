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
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Repository.Impl;
using Intime.OPC.Repository.Support;
using Intime.OPC.Service;
using Intime.OPC.Service.Support;
using Intime.OPC.WebApi.Controllers;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using NUnit.Framework;

namespace Intime.OPC.WebApi.Test.ControllerTest
{
    [TestFixture]
    public class OrderControllerTest : BaseControllerTest
    {
        private OrderController _controller;

        public OrderController GetController()
        {

            _controller = GetInstance<OrderController>(); 
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
            var actual = _controller.GetOrder(new OrderQueryRequest()
            {
                EndCreateDate = DateTime.Now,
                StartCreateDate = DateTime.Now.AddYears(-2),
               // ShippingContactPhone = "1863586525665566"
            }, new UserProfile()
            {
                //StoreIds = new[] { 20, 21 }

            }) as OkNegotiatedContentResult<PageResult<OrderDto>>;

            Assert.IsNotNull(actual);
        }

        //114042236511
        [Test()]
        public void GetTest([Values("114041605690")] string orderno)
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.GetOrderss(orderno, 1) as OkNegotiatedContentResult<OrderDto>;

            Assert.IsNotNull(actual);
        }


        [Test()]
        public void WebSiteStatSaleDetailTest()
        {
            _controller.Request.Method = HttpMethod.Post;
            var actual = _controller.WebSiteStatSaleDetail(new SearchStatRequest()
                {
                    EndDate = DateTime.Now,
                    StartDate = DateTime.Now.AddYears(-2)
                }, new UserProfile()) as OkNegotiatedContentResult<PagedSaleDetailStatListDto>;

            Assert.IsNotNull(actual);
        }


        [Test()]
        public void WebSiteStatReturnDetailTest()
        {
            _controller.Request.Method = HttpMethod.Post;
            var actual = _controller.WebSiteStatReturnDetail(new SearchStatRequest()
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now.AddYears(-2)
            }, new UserProfile()) as OkNegotiatedContentResult<PagedReturnGoodsStatListDto>;

            Assert.IsNotNull(actual);
        }


        [Test()]
        public void WebSiteCashierTest([Values(null, "RMA", "SALES")]string financialType)
        {
            _controller.Request.Method = HttpMethod.Post;
            var actual = _controller.WebSiteCashier(new SearchCashierRequest()
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now.AddYears(-2),
                FinancialType = financialType

            }, new UserProfile()) as OkNegotiatedContentResult<PagedCashierList>;

            Assert.IsNotNull(actual);
        }

    }
}
