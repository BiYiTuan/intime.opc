// ***********************************************************************
// Assembly         : 00_Intime.OPC.Domain
// Author           : Liuyh
// Created          : 03-28-2014 23:24:21
//
// Last Modified By : Liuyh
// Last Modified On : 03-28-2014 23:25:14
// ***********************************************************************
// <copyright file="UserIdConverException.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Intime.OPC.Domain.Exception
{
    /// <summary>
    /// Class UserIdConverException.
    /// </summary>
    public class UserIdConverException : OpcApiException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdConverException"/> class.
        /// </summary>
        /// <param name="userid">The userid.</param>
        public UserIdConverException(string userid)
            : base(userid)
        {
            UserID = userid;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public string UserID { get; private set; }
    }
}