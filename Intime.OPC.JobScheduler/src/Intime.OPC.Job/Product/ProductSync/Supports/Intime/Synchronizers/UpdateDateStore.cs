using System;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Synchronizers
{
    public class UpdateDateStore : IUpdateDateStore
    {
        public DateTime GetLast(string key)
        {
#if DEBUG
            //TODO:修改时间
            return DateTime.Now.AddDays(-2);
#endif
            return DateTime.Now.AddHours(-2);
        }

        public void Update(string key, DateTime dateTime)
        {
            //TODO:Save DateTime
        }
    }
}
