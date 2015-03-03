using System;
using System.Runtime.Serialization;

namespace OPCApp.Infrastructure.REST
{
    /// <summary>
    /// 请求抽象类
    /// </summary>
    /// <typeparam name="TData">请求数据类型</typeparam>
    [DataContract]
    public class Request<TData>
    {
        public string URI { get; set; }

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
        public string Signature { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        [DataMember(Name = "data")]
        public TData Data { get; set; }
    }
}
