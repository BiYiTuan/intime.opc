using System;

namespace Intime.OPC.Domain
{
    /// <summary>
    ///     Interface ILoginModel
    /// </summary>
    public interface ILoginModel
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        /// <value>The user identifier.</value>
        int UserID { get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        /// <value>The name of the user.</value>
        string UserName { get; }

        /// <summary>
        /// Gets the token.
        /// </summary>
        /// <value>The token.</value>
        string Token { get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        /// <value>The expires.</value>
        DateTime Expires { get; }

        /// <summary>
        /// 专柜ID
        /// </summary>
        /// <value>The shoppe identifier.</value>
        string ShoppeID { get; }
        /// <summary>
        /// 错误码
        /// </summary>
        int ErrorCode { get; }
    }
}