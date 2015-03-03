using System;
using Intime.OPC.Job.Product.ProductSync.Supports.Intime.Jobs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intime.OPC.Job.Product.StockAggregate;
using Intime.OPC.Job.Trade.SplitOrder;

namespace Intime.OPC.Job.Tests.Product
{
    [TestClass]
    public class AllSyncJobTest
    {
        [TestMethod]
        public void SyncTest()
        {
            //var splitOrder = new SplitOrderJob();
            //splitOrder.Execute(null);

            var job = new AllSyncJob();
            job.Execute(null);
            //var picJob = new ProductPicSyncJob();
            //picJob.Execute(null);
            //var stockJob = new StockAttregateJob();
            //stockJob.Execute(null);

        }

        //[TestMethod]
        //public void PropertyTest()
        //{
        //    var job = new ProductPropertySyncJob();
        //    job.Execute(null);
        //}
    }
}
