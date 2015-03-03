using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Repository.Support;
using Intime.OPC.Service.Support;
using Intime.OPC.WebApi.Controllers;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Intime.OPC.WebApi.Test.ControllerTest
{
    [TestFixture]
    public class StoreControllerTest : BaseControllerTest
    {
        private StoreController _controller;

        public StoreController GetController()
        {
            var storeRepository = new StoreRepository();

            _controller = new StoreController(new StoreService(storeRepository));

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
            var actual = _controller.GetList(new StoreRequest()
            {
                Page = 1,
                PageSize = 10
            }, new UserProfile()
            {
                Id = 9999
            }) as OkNegotiatedContentResult<PagerInfo<StoreDto>>;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void GetListTest_3([Values("银泰")]string namePrefix)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList(new StoreRequest
            {
                Page = 1,
                PageSize = 20,
                NamePrefix = namePrefix,
            }, new UserProfile(){Id = 9999}) as OkNegotiatedContentResult<PagerInfo<StoreDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.Datas.Count > 0);
        }

        [Test()]
        public void GetListTest_4([Values("杭州市所有银泰百货商场")]string name)
        {
            _controller.Request.Method = HttpMethod.Get;

            var actual = _controller.GetList(new StoreRequest
            {
                Page = 1,
                PageSize = 20,
                Name = name
            }, new UserProfile()
            {
                Id = 9999
            }) as OkNegotiatedContentResult<PagerInfo<StoreDto>>;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.Datas.Count > 0);
        }


        [Test()]
        public void GetTest([Values(1)]int id)
        {
            _controller.Request.Method = HttpMethod.Get;
            var actual = _controller.Get(id,new UserProfile()) as OkNegotiatedContentResult<StoreDto>;
            var dto = actual.Content;

            Assert.IsNotNull(actual);
            Assert.IsTrue(dto != null);
        }


        [Test()]
        public void Post()
        {
            _controller.Request.Method = HttpMethod.Post;

            var actual = _controller.Post(new StoreDto
            {
                Name = "Test_001",
                Description = "Description_001",


            }, new UserProfile() { Id = 9999, StoreIds = null }) as OkNegotiatedContentResult<StoreDto>;
            var dto = actual.Content;

            Assert.IsNotNull(actual);
            Assert.IsTrue(dto != null);
        }

        [Test()]
        public void Put_bad([Values(12)]int id)
        {
            _controller.Request.Method = HttpMethod.Put;

            var actual = _controller.Put(id, new StoreDto
            {
                Name = "Test_002",
                Description = "Description_002",

                Id = id,
            }, new UserProfile() { Id = 9999, StoreIds = new List<int> { 13 } }) as BadRequestErrorMessageResult;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void Put_bad2([Values(12)]int id)
        {
            _controller.Request.Method = HttpMethod.Put;

            var actual = _controller.Put(id, new StoreDto
            {
                Name = "Test_002",
                Description = "Description_002",

                Id = id,
            }, new UserProfile() { Id = 9999, StoreIds = new List<int> { } }) as BadRequestErrorMessageResult;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void Put_OK([Values(12)]int id)
        {
            _controller.Request.Method = HttpMethod.Put;

            var actual = _controller.Put(id, new StoreDto
            {
                Name = "Test_00555",
                Description = "Descrip55",
                Tel = "122",
                Status = 1,
                Id = id,
            }, new UserProfile() { Id = 9999, StoreIds = new[] { 12 } }) as OkNegotiatedContentResult<StoreDto>;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void Put_OK2([Values(12)]int id)
        {
            _controller.Request.Method = HttpMethod.Put;

            var actual = _controller.Put(id, new StoreDto
            {
                Name = "Test_00555",
                Description = "Descrip55",
                Tel = "122",
                Status = 1,
                Id = id,
            }, new UserProfile() { Id = 9999, StoreIds = null }) as OkNegotiatedContentResult<StoreDto>;

            Assert.IsNotNull(actual);
        }

        [Test()]
        public void Delete([Values(12)]int id)
        {
            _controller.Request.Method = HttpMethod.Delete;

            var actual = _controller.Delete(id, new UserProfile() { Id = 9999, StoreIds = new[] { id } }) as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(actual);
        }
    }
}