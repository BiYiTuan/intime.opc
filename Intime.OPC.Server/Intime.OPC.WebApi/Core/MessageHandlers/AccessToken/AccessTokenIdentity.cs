using System;
using System.Runtime.Serialization;

namespace Intime.OPC.WebApi.Core.MessageHandlers.AccessToken
{
    /// <summary>
    ///     用户访问令牌
    /// </summary>
    [DataContract]
    public class AccessTokenIdentity<T>
    {
        /// <summary>
        ///     过期时间
        /// </summary>
        [DataMember]
        public DateTime Expires { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        [DataMember]
        public T UserId { get; set; }
    }
}