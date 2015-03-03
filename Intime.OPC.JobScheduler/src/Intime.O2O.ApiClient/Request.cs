
using System;
using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient
{
    /// <summary>
    /// 请求抽象类
    /// </summary>
    /// <typeparam name="TRequest">请求类型</typeparam>
    /// <typeparam name="TResponse">返回类型</typeparam>
    [DataContract]
    public abstract class Request<TRequest, TResponse>
    {
        /// <summary>
        /// 客户端名称
        /// </summary>
        [DataMember(Name = "from")]
        public string From { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }

        /// <summary>
        /// 程序随机数0-99
        /// </summary>
        [DataMember(Name = "nonce")]
        public int Nonce { get; set; }

        /// <summary>
        /// 数据签名
        /// </summary>
        [DataMember(Name = "sign")]
        public string Sign { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        [DataMember(Name = "data")]
        public TRequest Data { get; set; }

        /// <summary>
        /// 资源地址
        /// </summary>
        public abstract string GetResourceUri();
    }
}
