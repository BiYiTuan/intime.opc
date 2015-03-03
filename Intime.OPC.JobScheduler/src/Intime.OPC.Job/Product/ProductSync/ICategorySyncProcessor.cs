using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Product.ProductSync
{
    /// <summary>
    /// 分类同步处理器
    /// </summary>
    public interface ICategorySyncProcessor
    {
        /// <summary>
        /// 同步分类
        /// </summary>
        /// <param name="channelCategoryId">渠道分类编号</param>
        /// <param name="channelCategoryName">渠道分类名称</param>
        /// <returns>同步后的本地分类信息</returns>
        Category Sync(string channelCategoryId, string channelCategoryName);
    }
}
