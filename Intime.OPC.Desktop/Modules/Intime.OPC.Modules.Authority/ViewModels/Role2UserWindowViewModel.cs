using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Modules.Authority.Views;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.Authority.ViewModels
{
    [Export("Role2UserViewModel", typeof (Role2UserWindowViewModel))]
    public class Role2UserWindowViewModel : BindableBase
    {
        private OPC_AuthRole _role;
        private List<OPC_AuthRole> _rolelist;
        private List<OPC_AuthUser> _userlist;

        public Role2UserWindowViewModel()
        {
            AuthorizationUserCommand = new DelegateCommand(AuthorizationUser);
            AddUserWindowCommand = new DelegateCommand(AddUsersWindow);
            RefreshCommand = new DelegateCommand(Refresh);
            DbGridClickCommand = new DelegateCommand(DBGridClick);
            GetSelectedCommand = new DelegateCommand(GetSelected);
            DeleteCommand = new DelegateCommand(DeleteUserList);
            Init();
        }

        public List<OPC_AuthUser> UserList
        {
            get { return _userlist; }
            set { SetProperty(ref _userlist, value); }
        }

        public List<OPC_AuthRole> RoleList
        {
            get { return _rolelist; }
            set { SetProperty(ref _rolelist, value); }
        }

        /*选中的用户Id*/
        private List<int> SelectedUserIdList { get; set; }
        /**/

        public OPC_AuthRole SelectedRole
        {
            get { return _role; }
            set { SetProperty(ref _role, value); }
        }

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand GetSelectedCommand { get; set; }
        public DelegateCommand AuthorizationUserCommand { get; set; }
        public DelegateCommand AddUserWindowCommand { get; set; }
        public DelegateCommand DbGridClickCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }

        private void Refresh()
        {
            if (SelectedRole == null)
            {
                if (UserList == null) return;
                UserList.Clear();
                return;
            }
            var menuDataService = AppEx.Container.GetInstance<IRole2UserService>();
            menuDataService.GetUserListByRole(SelectedRole.Id); //所有的 还是有权限
        }

        public void Init()
        {
            var roleDataService = AppEx.Container.GetInstance<IRoleService>();
            IList<OPC_AuthRole> re = roleDataService.Search(null).Result;
            RoleList = re != null ? re.ToList() : new List<OPC_AuthRole>();
            UserList = new List<OPC_AuthUser>();
        }

        private void DeleteUserList()
        {
            if (UserList == null)
            {
                MessageBox.Show("请选择要移除的用户", "提示");
                return;
            }
            if (UserList.Count == 0)
            {
                MessageBox.Show("请选择要移除的用户", "提示");
                return;
            }

            UserList = UserList.Where(e => e.IsSelected == false).ToList();
        }

        private void DBGridClick()
        {
            var role2UserService = AppEx.Container.GetInstance<IRole2UserService>();
            if (SelectedRole == null) return;
            UserList = role2UserService.GetUserListByRole(SelectedRole.Id);
        }

        private void GetSelected()
        {
            SelectedUserIdList = UserList.Where(n => n.IsSelected).Select(e => e.Id).ToList();
        }

        private void AuthorizationUser()
        {
            var role2UserService = AppEx.Container.GetInstance<IRole2UserService>();
            if (SelectedRole == null)
            {
                MessageBox.Show("请选择要授权的角色", "提示");
                return;
            }
            if (UserList == null || UserList.Count == 0)
            {
                MessageBox.Show("请选择要授权的用户", "提示");
                return;
            }
            List<int> userIDs = UserList.Select(e => e.Id).ToList();
            role2UserService.SetUserByRole(SelectedRole.Id, userIDs);
        }


        private void AddUsersWindow()
        {
            var obj = AppEx.Container.GetInstance<UsersWindow>("UsersView");
            if (obj.ShowDialog() == true)
            {
                List<OPC_AuthUser> seletedUsers = obj.ViewModel.SelectedUserList;
                if (seletedUsers != null)
                {
                    var temp = new List<OPC_AuthUser>();
                    temp.AddRange(seletedUsers);
                    temp.AddRange(UserList);
                    UserList = temp;
                    //UserList.AddRange(seletedUsers); 有问题不会刷新
                }
            }
            ;
        }
    }
}