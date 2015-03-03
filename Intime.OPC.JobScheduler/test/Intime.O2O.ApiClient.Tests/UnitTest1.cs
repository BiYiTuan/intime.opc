using Intime.O2O.ApiClient.Request;
using Intime.O2O.ApiClient.Yintai;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Intime.O2O.ApiClient.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestYintaiApiClient()
        {
            var client = new YintaiApiClient("http://60.247.44.101:9100", "1000000", "12345678", "C", "1000000");
            var para = new Dictionary<string, string> {{"channelID", "111111"}};
            var rsp = client.Post(para, "Yintai.OpenApi.Item.GetVirtualCategoryByChannel");
            Assert.IsNotNull(rsp);
        }

        [TestMethod]
        public void TestYintaiItemCode()
        {
            var client = new YintaiApiClient("http://60.247.44.101:9100", "1000000", "12345678", "C", "1000000");
            var para = new Dictionary<string, string> { { "itemCode", "05-02D-0027" } };
            var rsp = client.Post(para, "Yintai.OpenApi.Inventory.GetInventoryByItemCode");
            Assert.IsNotNull(rsp);
        }

        [TestMethod]
        public void TestMethod1()
        {
            var client = new DefaultApiClient("http://guide.intime.com.cn:8008/production-o2o-rs/api/", "intime", "intime");

            var reponse = client.Post(new GetProductsRequest()
            {
                Data = new GetProductsRequestData()
                {
                    PageIndex = 1,
                    LastUpdate = "2013-11-11",
                    PageSize = 20
                }
            });

            client.Post(new GetProductsRequest()
            {
                Data = new GetProductsRequestData()
                {
                    PageIndex = 1,
                    LastUpdate = "2013-11-15",
                    PageSize = 20
                }
            });

            client.Post(new GetStoresRequest()
            {
                Data = new GetStoresRequestData()
                {
                    PageIndex = 1,
                    PageSize = 20
                }
            });

            client.Post(new GetProductImagesRequest()
            {
                Data = new GetProductImagesRequestData()
                {
                    PageIndex = 1,
                    PageSize = 20,
                    LastUpdate = "2013-11-15"
                }
            });

            String message = reponse.Message;
        }
    }
}
