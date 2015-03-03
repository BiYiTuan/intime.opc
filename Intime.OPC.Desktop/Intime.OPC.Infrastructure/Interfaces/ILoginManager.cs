// ***********************************************************************
// Assembly         : Intime.OPC.Desktop.Infrastructure
// Author           : Liuyh
// Created          : 03-15-2014 23:15:02
//
// Last Modified By : Liuyh
// Last Modified On : 03-15-2014 23:26:19
// ***********************************************************************
// <copyright file="ILoginManager.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Intime.OPC.Domain;
namespace Intime.OPC.Infrastructure.Interfaces
{
    /// <summary>
    ///     Interface ILoginManager
    /// </summary>
    public interface ILoginManager
    {
        bool IsLogin { get; }

        /// <summary>
        ///     Logins the specified user identifier.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns>ILoginModel.</returns>
        ILoginModel Login(string userName, string password);


        /// <summary>
        ///     Res the login.
        /// </summary>
        /// <returns>ILoginModel.</returns>
        ILoginModel ReLogin();

        /// <summary>
        /// Logout
        /// </summary>
        void LogOut();
    }
}