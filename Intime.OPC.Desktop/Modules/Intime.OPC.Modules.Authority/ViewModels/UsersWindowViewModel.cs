using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.DataService;
using Intime.OPC.Infrastructure.Mvvm.Model;

namespace Intime.OPC.Modules.Authority.ViewModels
{
    [Export(typeof (UsersWindowViewModel))]
    public class UsersWindowViewModel : BindableBase
    {
        /*选择字段*/
        private PageDataResult<OPC_AuthUser> _prResult;
        private List<OPC_AuthUser> _userList;

        public UsersWindowViewModel()
        {
            Init();
        }

        public string SelectedFiled { get; set; }
        /*选择字段的值*/
        public string SelectedFiledValue { get; set; }
        /*查询字段列表*/
        public List<string> FieldList { get; set; }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }
        public DelegateCommand CommandGetDown { get; set; }
        public List<OPC_AuthUser> SelectedUserList { get; set; }

        public List<OPC_AuthUser> UserList
        {
            get { return _userList; }
            set { SetProperty(ref _userList, value); }
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public PageDataResult<OPC_AuthUser> PrResult
        {
            get { return _prResult; }
            set { SetProperty(ref _prResult, value); }
        }

        private void Search()
        {
            int indexFiled = FieldList.IndexOf(SelectedFiled);
            var dicFilter = new Dictionary<string, object>
            {
                {"SearchField", indexFiled == -1 ? 1 : indexFiled},
                {"SearchValue", SelectedFiledValue},
                {"pageIndex", PageIndex},
                {"pageSize", PageSize},
                {"orgid", ""}
            };
            IBaseDataService<OPC_AuthUser> userDataService = GetDataService();
            PageResult<OPC_AuthUser> prResultTemp = userDataService.Search(dicFilter);
            if (prResultTemp == null || prResultTemp.Result == null)
            {
                return;
            }
            PrResult = new PageDataResult<OPC_AuthUser>();
            PrResult.Models = prResultTemp.Result.ToList();
            PrResult.Total = prResultTemp.TotalCount;
        }

        /*初始化页面固有的数据值*/

        private void Init()
        {
            FieldList = new List<string> {"登陆名", "姓名"};
            /*查询初始化*/
            SelectedFiledValue = "";
            SelectedFiled = "";

            PageIndex = 1;
            PageSize = 10;
            PrResult = new PageDataResult<OPC_AuthUser>();

            SearchCommand = new DelegateCommand(Search);
            CommandGetDown = new DelegateCommand(SelectedUser);
        }

        public void SelectedUser()
        {
            if (PrResult == null || PrResult.Models == null || PrResult.Models.Count == 0)
            {
                SelectedUserList = new List<OPC_AuthUser>();
                return;
            }
            SelectedUserList = PrResult.Models.Where(e => e.IsSelected).ToList();
        }

        protected IBaseDataService<OPC_AuthUser> GetDataService()
        {
            return AppEx.Container.GetInstance<IAuthenticationService>();
        }
    }
}