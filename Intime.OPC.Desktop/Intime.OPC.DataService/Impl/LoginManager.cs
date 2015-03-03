// ***********************************************************************
// Assembly         : Intime.OPC.DataService
// Author           : Liuyh
// Created          : 03-15-2014 
//
// Last Modified By : Liuyh
// Last Modified On : 03-16-2014 
// ***********************************************************************
// <copyright file="LoginManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel.Composition;
using Intime.OPC.DataService.Common;
using Intime.OPC.Domain;
using Intime.OPC.Infrastructure.Interfaces;
using Intime.OPC.Infrastructure.Rest;

namespace Intime.OPC.DataService.Impl
{
    /// <summary>
    ///     Class LoginManager
    /// </summary>
    [Export(typeof (ILoginManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LoginManager : ILoginManager
    {
        /// <summary>
        ///     The password
        /// </summary>
        private string password;

        /// <summary>
        ///     The user name
        /// </summary>
        private string userName;

        /// <summary>
        ///     Logins the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>ILoginModel.</returns>
        public ILoginModel Login(string userId, string password)
        {
            IsLogin = false;
            userName = userId;
            this.password = password;
            var info = new LoginInfo();
            info.UserName = userName;
            info.Password = this.password;
            TokenModel tk = RestClient.Post<LoginInfo, TokenModel>("account/token", info);
            if (null == tk)
            {
                RestClient.SetToken(null);
                return new LoginModel { ErrorCode = RestClient.CurStatusCode };
            }
            IsLogin = true;
            RestClient.SetToken(tk.AccessToken);
            return new LoginModel(tk.UserId, tk.UserName, tk.AccessToken, "", tk.Expires);
        }

        [Import]
        public TokenManager TokenManager { get; set; }

        /// <summary>
        ///     Res the login.
        /// </summary>
        /// <returns>ILoginModel.</returns>
        public ILoginModel ReLogin()
        {
            return Login(userName, password);
        }

        /// <summary>
        /// Logout
        /// </summary>
        public void LogOut()
        {
            RestClient.SetToken(null);
            IsLogin = false;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance is login.
        /// </summary>
        /// <value><c>true</c> 如果实例 is login; 否则, <c>false</c>.</value>
        public bool IsLogin { get; private set; }

        /// <summary>
        ///     Class LoginInfo.
        /// </summary>
        internal class LoginInfo
        {
            /// <summary>
            ///     Gets or sets the name of the user.
            /// </summary>
            /// <value>The name of the user.</value>
            public String UserName { get; set; }

            /// <summary>
            ///     Gets or sets the password.
            /// </summary>
            /// <value>The password.</value>
            public String Password { get; set; }
        }
    }
}