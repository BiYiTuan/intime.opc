using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Models;
using Intime.OPC.Job.Trade;
using Intime.OPC.Job.Trade.SplitOrder;
using Intime.OPC.Job.Trade.SplitOrder.Models;
using Intime.OPC.Job.Trade.SplitOrder.Supports.Strategys;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intime.OPC.Job.Tests.Trade.OrderSplit.Strategys
{
    [TestClass]
    public class OneStoreHasEnoughSplitStrategyTests
    {
        private ISplitStrategy _splitStrategy;

        [TestInitialize]
        public void Setup()
        {
            _splitStrategy = new DefaultSplitStrategy();
            long id = DateTime.Now.ToFileTime();


            var current = DateTime.Now;

            long ln1 = current.ToFileTime();
            long ln2 = current.ToFileTime();


        }

        [TestCleanup]
        public void CleanUp()
        {
            _splitStrategy = null;
        }

        [TestMethod]
        public void Support()
        {
            // 构建数据
            var items = new List<OrderItemModel>()
            {
                new OrderItemModel()
                {
                    SkuId = 10001,
                    Quantity = 10
                }
            };
            var orderInfo = new OrderModel(items);

            var vendors = new SectionStocksModel
            {
                {
                    10001, new List<OPC_Stock>()
                    {
                        new OPC_Stock()
                        {
                            Count = 10
                        }
                    }
                }
            };

            Assert.IsTrue(_splitStrategy.Support(orderInfo, vendors));
        }


        [TestMethod]
        public void NotSupportForVendorIsNotAllHasStock()
        {
            // 构建数据
            var items = new List<OrderItemModel>()
            {
                new OrderItemModel()
                {
                    SkuId = 10001,
                    Quantity = 10
                },
                new OrderItemModel()
                {
                    SkuId = 10002,
                    Quantity = 10
                }
            };
            var orderInfo = new OrderModel(items);

            var vendors = new SectionStocksModel
            {
                {
                    10001, new List<OPC_Stock>()
                    {
                        new OPC_Stock()
                        {
                            Count = 10
                        }
                    }
                }
            };

            Assert.IsFalse(_splitStrategy.Support(orderInfo, vendors));
        }

        [TestMethod]
        public void NotSupport()
        {
            var splitStrategy = new DefaultSplitStrategy();

            // 构建数据
            var items = new List<OrderItemModel>()
            {
                new OrderItemModel()
                {
                    SkuId = 10001,
                    Quantity = 10
                }
            };
            var orderInfo = new OrderModel(items);

            var vendors = new SectionStocksModel
            {
                {
                    10001, new List<OPC_Stock>()
                    {
                        new OPC_Stock()
                        {
                            Count = 9
                        }
                    }
                }
            };

            Assert.IsFalse(splitStrategy.Support(orderInfo, vendors));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestArgumentThrowException()
        {
            _splitStrategy.Support(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullItemsThrowException()
        {
            _splitStrategy.Support(new OrderModel(null), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullVendorsThrowException()
        {
            _splitStrategy.Support(new OrderModel(new List<OrderItemModel>()
            {
                new OrderItemModel()
                {
                    SkuId = 10001,
                    Quantity = 10
                }
            }), null);
        }

        [TestMethod]
        public void Test()
        {
            var job = new SplitOrderJob();
            job.Execute(null);
        }

        [TestMethod]
        public void SpitProcessorTest()
        {
            var processor = new SplitOrderProcessor(new List<ISplitStrategy>()
            {
                new DefaultSplitStrategy(),
                new StockShortageSplitStrategy()
            });


            for (int i = 0; i < 100; i++)
            {
                processor.Process();
            }


        }
    }
}
