using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Intime.OPC.Domain;
using Intime.OPC.Domain.BusinessModel;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Repository.Impl;
using Intime.OPC.Service.Support;
using Intime.OPC.WebApi.App_Start;
using Intime.OPC.WebApi.Controllers;
using NUnit.Framework;

namespace Intime.OPC.WebApi.Test.ControllerTest
{
    [TestFixture]
    public class BrandControllerTest : BaseControllerTest
    {
        private BrandController _controller;

        public BrandController GetController()
        {
            var brandRepository = new BrandRepository();

            _controller = new BrandController(new BrandService(brandRepository), brandRepository);

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
        public void GetTest([Values(1000127)]int id)
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.Get(id) as OkNegotiatedContentResult<BrandDto>;
            var brandDto = actual.Content;

            Assert.IsNotNull(actual);
            Assert.IsTrue(brandDto != null);
        }


        [Test()]
        public void GetListTest_1()
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.GetList(new BrandFilter
            {
                Page = 1,
                PageSize = 10
            }, 0) as OkNegotiatedContentResult<PagerInfo<BrandDto>>;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void GetListTest_2([Values(860, 872)]int? CounterId)
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.GetList(new BrandFilter
            {
                Page = 1,
                PageSize = 20,
                CounterId = CounterId
            }, 0) as OkNegotiatedContentResult<PagerInfo<BrandDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.Datas.Count > 0);
        }

        [Test()]
        public void GetListTest_3([Values("小")]string namePrefix)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList(new BrandFilter
            {
                Page = 1,
                PageSize = 20,
                NamePrefix = namePrefix,
            }, 0) as OkNegotiatedContentResult<PagerInfo<BrandDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.Datas.Count > 0);
        }

        [Test()]
        public void GetListTest_4([Values("小骆驼")]string name)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList(new BrandFilter
            {
                Page = 1,
                PageSize = 20,
                Name = name
            }, 0) as OkNegotiatedContentResult<PagerInfo<BrandDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.Datas.Count > 0);
        }

        [Test()]
        public void Post()
        {
            _controller.Request.Method = HttpMethod.Post;

            var actual = _controller.Post(new BrandDto
            {
                Name = "Test_001",
                Description = "Description_001",
                Enabled = true,
                EnglishName = "EnglishName_001"

            }, 0) as OkNegotiatedContentResult<BrandDto>;
            var brandDto = actual.Content;

            Assert.IsNotNull(actual);
            Assert.IsTrue(brandDto != null);
        }

        [Test()]
        public void Put([Values(1000961)]int id)
        {
            _controller.Request.Method = HttpMethod.Put;

            var actual = _controller.Put(id, new BrandDto
            {
                Name = "Test_002",
                Description = "Description_002",
                Enabled = true,
                EnglishName = "EnglishName_002",
                Id = id,
                Supplier = new SupplierDto
                {
                    Id = 23,
                    Name = "Test修改关系只用ID即可"
                },
                Sections = new List<SectionDto>
                {
                    new SectionDto
                    {
                        Id = 877,
                        Name = "王晓华那个 应该添加不进去"
                    },
                    new SectionDto
                    {
                        Id = 871,
                        Name = "王晓华那个 应该添加不进去,这个可以有"
                    }

                }


            }, 0) as OkNegotiatedContentResult<BrandDto>;

            Assert.IsNotNull(actual);
        }
        [Test()]
        public void Delete([Values(1000961)]int id)
        {
            _controller.Request.Method = HttpMethod.Delete;

            var actual = _controller.Delete(id, 0) as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(actual);
        }
    }
}
