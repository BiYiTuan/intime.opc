using System;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Synchronizers
{
    /// <summary>
    /// 最后更新时间数据存储
    /// </summary>
    public interface IUpdateDateStore
    {
        /// <summary>
        /// 获取最后更新时间
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>最后更新时间</returns>
        DateTime GetLast(string key);

        /// <summary>
        /// 更新最后更新时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="dateTime">更新时间</param>
        void Update(string key, DateTime dateTime);
    }
}
