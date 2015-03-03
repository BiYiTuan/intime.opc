// ***********************************************************************
// Assembly         : Intime.OPC.DataService
// Author           : Liuyh
// Created          : 03-14-2014 21:16:36
//
// Last Modified By : Liuyh
// Last Modified On : 03-14-2014 21:18:45
// ***********************************************************************
// <copyright file="MenuDataService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.DataService.Impl
{
    /// <summary>
    ///     Class MenuDataService.
    /// </summary>
    [Export(typeof (IMenuDataService))]
    public class MenuDataService : IMenuDataService
    {
        /// <summary>
        ///     获得登录用户的所有菜单
        /// </summary>
        /// <returns>IEnumerable{MenuGroup}.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<MenuGroup> GetMenus()
        {
            try
            {
                string paras = "";// string.Format("UserId={0}", AppEx.LoginModel.UserID);
                    //AppEx.LoginModel.UserID); //1 update curUserId 
                IList<OPC_AuthMenu> listMenu = RestClient.Get<OPC_AuthMenu>("menu/loadmenu", paras);
                if (listMenu!=null)
                {
                    listMenu = GetDistinct(listMenu.ToList());
                }
              
                List<OPC_AuthMenu> groupMenu1 = listMenu.Where(e => e.PraentMenuId == e.Id).ToList();
                List<MenuGroup> groupMenu =
                    groupMenu1.Distinct().Select(e => new MenuGroup {Id = e.Id, Sort = e.Sort, MenuName = e.MenuName})
                        .OrderBy(e => e.Sort)
                        .ToList();
                foreach (MenuGroup grpMenu in groupMenu)
                {
                    List<OPC_AuthMenu> menus =
                        listMenu.Where(e => e.Id != e.PraentMenuId && e.PraentMenuId == grpMenu.Id)
                            .OrderBy(e => e.Sort)
                            .ToList();
                    if (menus.Count > 0)
                    {
                        grpMenu.Items = menus;
                    }
                }
                return groupMenu;
            }
            catch (Exception ex)
            {
                return new List<MenuGroup>();
            }
        }
        /// <summary>
        /// 待修改
        /// </summary>
        /// <param name="listMenu"></param>
        /// <returns></returns>
        private List<OPC_AuthMenu> GetDistinct(List<OPC_AuthMenu> listMenu)
        {
            var list = new List<OPC_AuthMenu>();
            var listKey = new List<int>();
            foreach (var item in listMenu)
            {
                if (listKey.Contains(item.Id))
                {
                    continue;
                }
                else
                {
                    list.Add(item);
                    listKey.Add(item.Id);
                }
            }
            return list;
        }

        public IEnumerable<OPC_AuthMenu> GetMenuList()
        {
            try
            {
                IList<OPC_AuthMenu> listMenu = RestClient.Get<OPC_AuthMenu>("menu/GetMenuList");

                return listMenu;
            }
            catch (Exception ex)
            {
                return new List<OPC_AuthMenu>();
            }
        }
    }
}