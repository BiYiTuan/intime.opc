using Intime.OPC.Job.Product.ProductSync.Models;

namespace Intime.OPC.Job.Product.ProductSync
{
    /// <summary>
    /// 渠道信息和本地映射器
    /// </summary>
    public interface IChannelMapper
    {
        /// <summary>
        /// 获取本地映射的Id值
        /// </summary>
        /// <param name="channelValue">渠道数据的值</param>
        /// <param name="mapType">映射类型</param>
        /// <returns>映射的对象</returns>
        ChannelMap GetMapByChannelValue(string channelValue, ChannelMapType mapType);

        /// <summary>
        /// 获取渠道的值
        /// </summary>
        /// <param name="localValue">本地Id的值</param>
        /// <param name="mapType">映射类型</param>
        /// <returns>映射的对象</returns>
        ChannelMap GetMapByLocal(string localValue, ChannelMapType mapType);

        /// <summary>
        /// 创建映射关系
        /// </summary>
        /// <param name="channelMap">映射关系</param>
        /// <returns>操作结果</returns>
        bool CreateMap(ChannelMap channelMap);

        bool UpdateMapByLocal(string localValue, ChannelMapType mapType, string channelValue);

        /// <summary>
        /// 删除映射关系
        /// </summary>
        /// <param name="channelMap"></param>
        void RemoveMap(ChannelMap channelMap);
    }
}
