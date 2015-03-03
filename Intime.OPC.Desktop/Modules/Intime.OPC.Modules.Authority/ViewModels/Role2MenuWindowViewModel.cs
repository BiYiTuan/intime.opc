// ***********************************************************************
// Assembly         : Intime.OPC.Modules.Authority
// Author           : Hyx
// Created          : 03-20-2014
//
// Last Modified By : Hyx
// Last Modified On : 03-20-2014
// ***********************************************************************
// <copyright file="Role2MenuWindowViewModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

/// <summary>
/// The ViewModels namespace.
/// </summary>

namespace Intime.OPC.Modules.Authority.ViewModels
{
    /// <summary>
    ///     Class Role2MenuWindowViewModel.
    /// </summary>
    /// <summary>
    ///     Class Role2MenuWindowViewModel.
    /// </summary>
    [Export("Role2MenuViewModel", typeof (Role2MenuWindowViewModel))]
    public class Role2MenuWindowViewModel : BindableBase
    {
        /// <summary>
        ///     The _menulist
        /// </summary>
        private List<OPC_AuthMenu> _menulist;

        /// <summary>
        ///     The _role
        /// </summary>
        private OPC_AuthRole _role;

        /// <summary>
        ///     The _rolelist
        /// </summary>
        private List<OPC_AuthRole> _rolelist;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Role2MenuWindowViewModel" /> class.
        /// </summary>
        public Role2MenuWindowViewModel()
        {
            AuthorizationUserCommand = new DelegateCommand(AuthorizationUser);
            DeleteUserListCommand = new DelegateCommand(DeleteUserList);
            DbGridClickCommand = new DelegateCommand(DBGridClick);
            GetSelectedCommand = new DelegateCommand(GetSelected);
            AuthorizationRefreshCommand = new DelegateCommand(Resfresh);
            Init();
        }

        /// <summary>
        ///     Gets or sets the menu list.
        /// </summary>
        /// <value>The menu list.</value>
        public List<OPC_AuthMenu> MenuList
        {
            get { return _menulist; }
            set { SetProperty(ref _menulist, value); }
        }

        /// <summary>
        ///     Gets or sets the role list.
        /// </summary>
        /// <value>The role list.</value>
        public List<OPC_AuthRole> RoleList
        {
            get { return _rolelist; }
            set { SetProperty(ref _rolelist, value); }
        }

        /*选中的用户Id*/

        /// <summary>
        ///     Gets or sets the selected menu identifier list.
        /// </summary>
        /// <value>The selected menu identifier list.</value>
        private List<int> SelectedMenuIdList { get; set; }

        /**/

        /// <summary>
        ///     Gets or sets the selected role.
        /// </summary>
        /// <value>The selected role.</value>
        public OPC_AuthRole SelectedRole
        {
            get { return _role; }
            set { SetProperty(ref _role, value); }
        }

        /// <summary>
        ///     Gets or sets the delete user list command.
        /// </summary>
        /// <value>The delete user list command.</value>
        public DelegateCommand DeleteUserListCommand { get; set; }

        /// <summary>
        ///     Gets or sets the get selected command.
        /// </summary>
        /// <value>The get selected command.</value>
        public DelegateCommand GetSelectedCommand { get; set; }

        /// <summary>
        ///     Gets or sets the authorization user command.
        /// </summary>
        /// <value>The authorization user command.</value>
        public DelegateCommand AuthorizationUserCommand { get; set; }

        /// <summary>
        ///     Gets or sets the database grid click command.
        /// </summary>
        /// <value>The database grid click command.</value>
        public DelegateCommand DbGridClickCommand { get; set; }

        public DelegateCommand AuthorizationRefreshCommand { get; set; }

        private void Resfresh()
        {
            Init();
        }

        public void Init()
        {
            var roleDataService = AppEx.Container.GetInstance<IRoleService>();
            RoleList = roleDataService.Search(null).Result.ToList();
            var menuDataService = AppEx.Container.GetInstance<IMenuDataService>();
            MenuList = menuDataService.GetMenuList().ToList(); //所有的 还是有权限
        }

        /// <summary>
        ///     Deletes the user list.
        /// </summary>
        private void DeleteUserList()
        {
            //liuyahua
            // var userSeleted = UserList.Where(e => e.IsSelected == true);
            MenuList.Remove(e => e.IsSelected);
            //UserList.Remove()
        }

        /// <summary>
        ///     Databases the grid click.
        /// </summary>
        private void DBGridClick()
        {
            var role2MenuService = AppEx.Container.GetInstance<IRole2MenuService>();
            if (SelectedRole == null) return;
            List<OPC_AuthMenu> menus = role2MenuService.GetMenuList(SelectedRole.Id);
            var menus2xx = new List<OPC_AuthMenu>();
            foreach (OPC_AuthMenu item in MenuList)
            {
                item.IsSelected = false;
                if (menus != null)
                {
                    OPC_AuthMenu menutemp = menus.FirstOrDefault(e => e.Id == item.Id);
                    item.IsSelected = menutemp != null;
                }
                menus2xx.Add(item);
            }
            //SelectedMenus = menus.ToList();
            MenuList = menus2xx;
        }

        /// <summary>
        ///     Gets the selected.
        /// </summary>
        private void GetSelected()
        {
            SelectedMenuIdList = MenuList.Where(n => n.IsSelected).Select(e => e.Id).ToList();
        }

        /// <summary>
        ///     Authorizations the user.
        /// </summary>
        private void AuthorizationUser()
        {
            var role2UserService = AppEx.Container.GetInstance<IRole2MenuService>();
            if (SelectedRole == null || MenuList == null) return;
            SelectedMenuIdList = MenuList.Where(n => n.IsSelected).Select(e => e.Id).ToList();
            var parentIdSelectedMenu = MenuList.Where(e => e.IsSelected).Select(e => e.PraentMenuId).Distinct().ToList();
            parentIdSelectedMenu.AddRange(SelectedMenuIdList);
            var result = role2UserService.SetMenuByRole(SelectedRole.Id, parentIdSelectedMenu);
            if (result.IsSuccess)
            {
                MessageBox.Show("资源授权成功！", "银泰OPC终端", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                MessageBox.Show(result.Msg, "银泰OPC终端", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}