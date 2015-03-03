using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Repository.Impl;
using Intime.OPC.Repository.Support;
using Intime.OPC.WebApi.Controllers;
using NUnit.Framework;

namespace Intime.OPC.WebApi.Test.ControllerTest
{
    public class SupplierControllerTest : BaseControllerTest
    {
        private SupplierController _controller;

        public SupplierController GetController()
        {
            var supplierRepository = new SupplierRepository();
            var brandRepository = new BrandRepository();

            _controller = new SupplierController(supplierRepository, brandRepository);

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
        public void GetTest([Values(20)]int id)
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.Get(id) as OkNegotiatedContentResult<SupplierDto>;
            var SupplierDto = actual.Content;

            Assert.IsNotNull(actual);
            Assert.IsTrue(SupplierDto != null);
        }


        [Test()]
        public void GetListTest_1()
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.GetList(new SupplierFilter
            {
                Page = 1,
                PageSize = 10
            }, 0) as OkNegotiatedContentResult<PagerInfo<SupplierDto>>;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void GetListTest_3([Values("s")]string namePrefix)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList(new SupplierFilter
            {
                Page = 1,
                PageSize = 20,
                NamePrefix = namePrefix,
            }, 0) as OkNegotiatedContentResult<PagerInfo<SupplierDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.Datas.Count > 0);
        }

        [Test()]
        public void GetListTest_4([Values("SupplierName")]string name)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList(new SupplierFilter
            {
                Page = 1,
                PageSize = 20,
                Name = name
            }, 0) as OkNegotiatedContentResult<PagerInfo<SupplierDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.Datas.Count > 0);
        }

        [Test()]
        public void Post()
        {
            _controller.Request.Method = HttpMethod.Post;

            var actual = _controller.Post(new SupplierDto
            {
                Name = "Test_001",
                Code = "asdfasdfasdf"

            }, 0) as OkNegotiatedContentResult<SupplierDto>;
            var SupplierDto = actual.Content;

            Assert.IsNotNull(actual);
            Assert.IsTrue(SupplierDto != null);
        }

        #region debug

        [Test()]
        public void Put([Values(53)]int id)
        {
            _controller.Request.Method = HttpMethod.Put;

            var actual = _controller.Put(id, new SupplierDto
            {
                Name = "Test_002",
                Id = id,
                Brands = new List<BrandDto>()
                {
                    new BrandDto()
                    {
                        Id=1000265,
                        Name = "kelamiti"
                    },
                                        new BrandDto()
                    {
                        Id=1000266,
                        Name = "adl"
                    },
                }


            }, 0) as OkNegotiatedContentResult<SupplierDto>;

            Assert.IsNotNull(actual);
        }

        [Test]
        public void Delete([Values(53)]int id)
        {
            _controller.Request.Method = HttpMethod.Delete;

            var actual = _controller.Delete(id, 0) as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(actual);
        }

        #endregion
    }
}
