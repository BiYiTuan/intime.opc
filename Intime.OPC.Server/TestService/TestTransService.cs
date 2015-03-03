using System;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Converters;

namespace TestService
{
    [TestClass]
    public class TestTransService : TestService<ITransService>
    {
        [TestMethod]
        public void TestGetSaleByShippingSaleN()
        {
            var lst= Service.GetSaleByShippingSaleNo("20140409-001");
            Assert.IsNotNull(lst);

            Assert.AreNotEqual(lst.Count,0);
        }

        [TestMethod]
        public void TestGetShippingSale()
        {
            Service.UserId = 1;
            var lst = Service.GetShippingSale("", "", DateTime.Now, DateTime.Now, "", -1, -1,
                "", -1, 1, 1000);
            Assert.IsNotNull(lst);

            Assert.AreNotEqual(lst.TotalCount, 0);
        }

        [TestMethod]
        public void TestGetEnum()
        {
            var lst = Enum.GetValues(typeof (EnumOderStatus));
            var lst2 = Enum.GetNames(typeof (EnumOderStatus));
            foreach (var v in lst)
            {
                Console.WriteLine(v);
            }
        }

        [TestMethod]
        public void TestCreate()
        {
            ShippingSaleCreateDto dto=new ShippingSaleCreateDto();
            dto.OrderNo = "114042236511";
            dto.SaleOrderIDs.Add("114042236511-001");
            dto.ShipViaID = 38;
            dto.ShipViaName = "中通";
            dto.ShippingFee = 123123;
            dto.ShippingCode = "werdf";
          var bl=  Service.CreateShippingSale(1, dto);
        }
    }
}
