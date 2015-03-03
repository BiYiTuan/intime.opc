using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Domain.Dto.Financial;
using Intime.OPC.Domain.Models;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestService
{
    [TestClass]
    public class TestOrderService : TestService<IOrderService>
    {
        [TestMethod]
        public void TestGetByReturnGoodsInfo()
        {



            ReturnGoodsInfoRequest goods = new ReturnGoodsInfoRequest();
            goods.StartDate = new DateTime(2010, 1, 1);
            goods.EndDate = DateTime.Now.Date.AddDays(1);


            var lst = Service.GetByReturnGoodsInfo(goods);
            AssertList<OrderDto>(lst);
        }

        [TestMethod]
        public void TestGetOrderItemsByOrderNo()
        {
            var lst = Service.GetOrderItems("1142014041211", 1, 1000);
            AssertList<OrderItemDto>(lst);
        }

        [TestMethod]
        public void TestLine()
        {
            using (var db = new YintaiHZhouContext())
            {
                var saleDetails = db.OPC_SaleDetails;
                var brands = db.Brands;
                var sections = db.Sections;
                var sales = db.OPC_Sales;
                var orderItems = db.OrderItems;

                var query =
                    from sale in sales
                    from saleDetail in saleDetails
                    from brand in brands
                    from section in sections
                    from orderItem in orderItems
                    where saleDetail.SaleOrderNo == sale.SaleOrderNo
                          && saleDetail.OrderItemId == orderItem.Id
                          && orderItem.BrandId == brand.Id
                          && sale.SectionId == section.Id
                          && sale.SaleOrderNo == "114042236511-002"
                    select new SaleDetailDto()
                    {
                        Id = saleDetail.Id,
                        ProductName = orderItem.ProductName,
                        ProductNo = SqlFunctions.StringConvert((double)orderItem.ProductId).Trim(),
                        Color = orderItem.ColorValueName,
                        Size = orderItem.SizeValueName,
                        Brand = brand.Name,
                        SaleOrderNo = sale.SaleOrderNo,
                        LabelPrice = orderItem.UnitPrice.HasValue ? orderItem.UnitPrice.Value : 0M,
                        Price = orderItem.ItemPrice,
                        SellCount = saleDetail.SaleCount,
                        SectionCode = saleDetail.SectionCode
                    };

                var list = query.ToList();

                var b =
                list.Count();
            }
        }


        [TestMethod]
        public void TestWebSiteStatSaleDetail()
        {
            SearchStatRequest dto = new SearchStatRequest(10000);
            dto.StartTime = new DateTime(2014, 4, 26);
            dto.EndTime = new DateTime(2014, 5, 7);
            //Service.UserId = 1;
            var o = Service.WebSiteStatSaleDetail(dto);
            Assert.IsNotNull(o);
            Assert.AreNotEqual(o.Count, 0);
        }

        [TestMethod]
        public void TestWebSiteStatReturnDetail()
        {
            SearchStatRequest dto = new SearchStatRequest(10000);
            dto.StartTime = new DateTime(2000, 1, 1);
            dto.EndTime = DateTime.Now;
            var o = Service.WebSiteStatReturnDetail(dto);
            Assert.IsNotNull(o);
            Assert.AreNotEqual(o.Count, 0);
        }

        [TestMethod]
        public void TestWebSiteCashier()
        {
            var req = new SearchCashierRequest(10000);
            //dto.StartTime = new DateTime(2000, 1, 1);
            req.StartTime = new DateTime(2000, 1, 1);
            req.EndTime = DateTime.Now;
            req.FinancialType = null;

            var o = Service.WebSiteCashier(req);
            Assert.IsNotNull(o);
            Assert.AreNotEqual(o.Count, 0);
        }


        [TestMethod]
        public void TestWebSiteCashier_1()
        {
            var req = new SearchCashierRequest(10000);
            //dto.StartTime = new DateTime(2000, 1, 1);
            req.StartTime = new DateTime(2000, 1, 1);
            req.EndTime = DateTime.Now;
            req.FinancialType = "RMA";
            req.OrderChannelNo = "1218686401201404233163476538";

            var o = Service.WebSiteCashier(req);
            Assert.IsNotNull(o);
            Assert.AreNotEqual(o.Count, 0);
        }

        [TestMethod]
        public void TestWebSiteCashier_2()
        {
            var req = new SearchCashierRequest(10000);
            //dto.StartTime = new DateTime(2000, 1, 1);
            req.StartTime = new DateTime(2000, 1, 1);
            req.EndTime = DateTime.Now;
            req.FinancialType = "SALES";

            var o = Service.WebSiteCashier(req);
            Assert.IsNotNull(o);
            Assert.AreNotEqual(o.Count, 0);
        }
    }
}
