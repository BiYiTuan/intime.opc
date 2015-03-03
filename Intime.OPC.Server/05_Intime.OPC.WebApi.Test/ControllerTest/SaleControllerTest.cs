using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Repository;
using Intime.OPC.Repository.Impl;
using Intime.OPC.Repository.Support;
using Intime.OPC.Service;
using Intime.OPC.Service.Contract;
using Intime.OPC.Service.Impl;
using Intime.OPC.Service.Support;
using Intime.OPC.WebApi.Controllers;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Intime.OPC.WebApi.Test.ControllerTest
{
    [TestFixture]
    public class SaleControllerTest : BaseControllerTest
    {
        private SaleController _controller;

        public SaleController GetController()
        {
            ISaleRepository saleRepository = new SaleRepository();
            ISaleRemarkRepository saleRemarkRepository = new SaleRemarkRepository();
            IAccountRepository accountRepository = new AccountRepository();
            IOrgInfoRepository orgInfoRepository = new OrgInfoRepository();
            IRoleUserRepository roleUserRepository = new RoleUserRepository();
            ISectionRepository sectionRepository = new SectionRepository();
            IStoreRepository storeRepository = new StoreRepository();
            IAccountService accountService = new AccountService(accountRepository, orgInfoRepository, roleUserRepository, sectionRepository, storeRepository);
            ISaleService saleService = new SaleService(saleRepository, saleRemarkRepository, accountService);
            //IBrandRepository brandRepository = new BrandRepository();

            IShippingSaleRepository shippingSaleRepository = new ShippingSaleRepository();
            IOrderRepository orderRepository = new OrderRepository();
            //IOrderRemarkRepository orderRemarkRepository = new OrderRemarkRepository();
            //IOrderItemRepository orderItemRepository = new OrderItemRepository();
            //ISaleDetailRepository saleDetailRepository = new SaleDetailRepository();
            ISaleRMARepository saleRmaRepository = new SaleRMARepository();
            // IOrderService orderService = new OrderService(orderRepository, orderRemarkRepository, orderItemRepository, brandRepository, accountService, saleDetailRepository, saleRmaRepository);
            IShippingSaleService shippingSaleService = new ShippingSaleService(shippingSaleRepository, orderRepository, saleRmaRepository, accountService);
            ISaleRepository saleOrderRepository = new SaleRepository();
            ISalesOrderService saleOrderService = new SalesOrderService(saleOrderRepository);
            _controller = new SaleController(saleService, shippingSaleService, saleOrderService);

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
        public void GetListTest_1()
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.GetList(new GetSaleOrderQueryRequest
            {
                Page = 1,
                PageSize = 10,
                //EndDate = DateTime.Now,
                //StartDate = DateTime.Now.AddYears(-1),
                //ShippingOrderId = 29
                //Status = EnumSaleOrderStatus.ShipInStorage
            }, 28, new UserProfile {  }) as OkNegotiatedContentResult<PagerInfo<SaleDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.TotalCount >= 0);
        }

        [Test()]
        public void GetListTest_2()
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.GetList(new GetSaleOrderQueryRequest
            {
                Page = 1,
                PageSize = 10,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now.AddYears(-1),
                Statuses = new List<int> { EnumSaleOrderStatus.PrintSale.AsId(), EnumSaleOrderStatus.ShoppingGuidePickUp.AsId() }
            }, 28, new UserProfile { }) as OkNegotiatedContentResult<PagerInfo<SaleDto>>;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void GetListTest_3()
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.GetList(new GetSaleOrderQueryRequest
            {
                Page = 1,
                PageSize = 10,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now.AddYears(-1),
                //Status = EnumSaleOrderStatus.ShipInStorage
                OrderProductType = (int)OrderProductType.MiniSilver,
            }, 28, new UserProfile { }) as OkNegotiatedContentResult<PagerInfo<SaleDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.TotalCount >= 0);
        }

        [Test()]
        public void SetSalesOrderCashTest([Values("114042236511-001")]string salesorderno)
        {
            //1.迷你银测试OK
            _controller.Request.Method = HttpMethod.Put;
            var userProfile = new UserProfile
            {
                StoreIds = new[] { 21, 20 },
                Id = 9999
            };

            var restult = _controller.SetSalesOrderCash(salesorderno, new SalesOrderCashRequest()
            {
                CashNo = "WORK-HARD-TEST1111"

            }, userProfile);
            var actual = restult as OkResult;

            Assert.IsNotNull(actual);
        }



    }
}
