using System;
using System.Runtime.Serialization;

namespace Intime.OPC.WebApi.Models
{
    /// <summary>
    ///     用户访问令牌
    /// </summary>
    [DataContract]
    public class TokenModel
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        ///     过期时间
        /// </summary>
        [DataMember(Name = ".expires")]
        public DateTime Expires { get; set; }

        /// <summary>
        ///     用户id
        /// </summary>
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [DataMember(Name = "UserName")]
        public string UserName { get; set; }
    }
}