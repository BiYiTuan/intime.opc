using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Custom;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestService
{
    [TestClass]
    public class TestSaleRmaService:TestService<ISaleRMAService>
    {
        [TestMethod]
        public void TestGetByReturnGoods()
        {
            ReturnGoodsRequest request=new ReturnGoodsRequest();
            
            request.StartDate = new DateTime(2010, 1, 1);
            request.EndDate = DateTime.Now.Date.AddDays(1);
            Service.UserId = 1;
            var lst=  Service.GetByReturnGoods(request,1);
            AssertList<SaleRmaDto>(lst);
        }

       
        public void TestCreateSaleRma()
        {
            RMARequest request=new RMARequest();
            request.CustomFee = 123123;
            request.StoreFee = 123;
            request.ReturnProducts.Add(new KeyValuePair<int, int>(7,1 ));
            request.ReturnProducts.Add(new KeyValuePair<int, int>(6, 13));
            request.RealRMASumMoney = 123123;

            request.OrderNo = "11420140507925";  //OrderNo=
            request.Remark = "unitTest";

            Service.CreateSaleRMA(1,request);
        }

        [TestMethod]
        public void TestGetByPack_PackageReceiveDto()
        {

            PackageReceiveRequest dto = new PackageReceiveRequest();
            dto.StartDate = new DateTime(2014, 4, 1);
            dto.EndDate = DateTime.Now.Date;
            dto.OrderNo = "114";
            dto.SaleOrderNo = "114";

            var lst = Service.GetByPack(dto);
            Assert.IsNotNull(lst);
            Assert.AreNotEqual(0, lst.TotalCount);
        }

        [TestMethod]
        public void TestGetByReturnGoodPay()
        {
            var dto = new ReturnGoodsPayRequest();
            dto.StartDate = new DateTime(2014, 4, 1);
            dto.EndDate = DateTime.Now.Date;
            dto.pageIndex = 1;
            dto.pageSize = 100;
            Service.UserId = 1;
            var lst = Service.GetByReturnGoodPay(dto);
            Assert.IsNotNull(lst);
            Assert.AreNotEqual(0, lst.TotalCount);
        }
        [TestMethod]
        public void TestGetByRmaNo()
        {

            Service.UserId = 1;
            var lst = Service.GetByRmaNo("1142014041211001", 1, 100);
            Assert.IsNotNull(lst);
            Assert.AreNotEqual(0, lst.TotalCount);
        }
    }
}
