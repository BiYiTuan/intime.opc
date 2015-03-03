// ***********************************************************************
// Assembly         : Intime.OPC.DataService
// Author           : Liuyh
// Created          : 03-14-2014 21:16:01
//
// Last Modified By : Liuyh
// Last Modified On : 03-14-2014 21:14:18
// ***********************************************************************
// <copyright file="IMenuDataService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.DataService.Interface
{
    /// <summary>
    /// Interface IMenuDataService
    /// </summary>
    public interface IMenuDataService
    {
        /// <summary>
        /// 获得登录用户的所有菜单
        /// </summary>
        /// <returns>IEnumerable{MenuGroup}.</returns>
        IEnumerable<MenuGroup> GetMenus();

        IEnumerable<OPC_AuthMenu> GetMenuList();
    }
}