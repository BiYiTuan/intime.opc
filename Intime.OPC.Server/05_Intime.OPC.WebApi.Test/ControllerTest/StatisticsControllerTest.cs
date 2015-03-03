using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.WebApi.Controllers;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Intime.OPC.WebApi.Test.ControllerTest
{

    [TestFixture]
    public class StatisticsControllerTest : BaseControllerTest
    {
        private StatisticsController _controller;

        public StatisticsController GetController()
        {
            _controller = GetInstance<StatisticsController>();
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
        public void GetList4GiftCardSalesReportTest()
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList4GiftCardSalesReport(new GiftCardSalesStatisticsReportRequest(), new UserProfile()) as OkNegotiatedContentResult<PagerInfo<GiftCardSalesStatisticsReportDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetList4AssociateCommissionReportTest()
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList4AssociateCommissionReport(new AssociateCommissionStatisticsReportRequest(), new UserProfile()) as OkNegotiatedContentResult<PagerInfo<AssociateCommissionStatisticsReportDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetList4CashAssociateCommissionReportTest()
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList4CashAssociateCommissionReport(new CashAssociateCommissionStatisticsReportRequest(), new UserProfile()) as OkNegotiatedContentResult<PagerInfo<CashAssociateCommissionStatisticsReportDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetList4SalesDetailsReportTest()
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList4SalesDetailsReport(new SearchStatRequest(), new UserProfile()) as OkNegotiatedContentResult<PagerInfo<SaleDetailStatDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetList4RmaDetailsReportTest()
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList4RmaDetailsReport(new SearchStatRequest(), new UserProfile()) as OkNegotiatedContentResult<PagerInfo<ReturnGoodsStatDto>>;

            Assert.IsNotNull(actual);

        }

        [Test()]
        public void GetList4CashierDetailsReportTest()
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList4CashierDetailsReport(new SearchCashierRequest(), new UserProfile()) as OkNegotiatedContentResult<PagerInfo<WebSiteCashierSearchDto>>;

            Assert.IsNotNull(actual);

        }
    }
}
