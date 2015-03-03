// ***********************************************************************
// Assembly         : 02_Intime.OPC.Service
// Author           : Liuyh
// Created          : 03-23-2014 11:38:21
//
// Last Modified By : Liuyh
// Last Modified On : 03-23-2014 12:21:00
// ***********************************************************************
// <copyright file="IMenuService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    /// <summary>
    ///     Interface IMenuService
    /// </summary>
    public interface IMenuService : IService
    {
        /// <summary>
        ///     获得角色的所有菜单
        /// </summary>
        /// <param name="roleID">The role identifier.</param>
        /// <returns>IEnumerable{OPC_AuthMenu}.</returns>
        IEnumerable<OPC_AuthMenu> SelectByRoleID(int roleID);

        /// <summary>
        ///     获得用户的所有菜单
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>IEnumerable{OPC_AuthMenu}.</returns>
        IEnumerable<OPC_AuthMenu> SelectByUserID(int userID);

        /// <summary>
        /// 获得所有二级菜单
        /// </summary>
        /// <returns>IList{OPC_AuthMenu}.</returns>
        IList<OPC_AuthMenu> GetMenuList();
    }
}