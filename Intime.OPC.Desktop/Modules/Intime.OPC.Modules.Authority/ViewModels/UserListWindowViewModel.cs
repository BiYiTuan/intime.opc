using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Intime.OPC.Modules.Authority.Views;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.DataService;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Model;

namespace Intime.OPC.Modules.Authority.ViewModels
{
    [Export("UserListViewModel", typeof (IViewModel))]
    // [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserListWindowViewModel : BaseListViewModel<OPC_AuthUser>
    {
        private OPC_AuthUser _curUser;
        public PageDataResult<OPC_AuthUser> _prResult;

        public UserListWindowViewModel()
            : base("UserListWindow")
        {
            EditViewModeKey = "UserViewModel";
            AddViewModeKey = "UserViewModel";
            InitOrg();
            InitUser();
        }

        public DelegateCommand UpdatePwdCommand { get; set; }
        public DelegateCommand AddOrgCommand { get; set; }
        public DelegateCommand EditOrgCommand { get; set; }
        public DelegateCommand DeleteOrgCommand { get; set; }
        public NodeViewModel Nodes { get; private set; }
        public NodeInfo NodeInfo { get; private set; }
        public ReadOnlyCollection<DelegateCommand> Commands { get; private set; }

        public PageDataResult<OPC_AuthUser> PrResult
        {
            get { return _prResult; }
            set { SetProperty(ref _prResult, value); }
        }

        public OPC_AuthUser CurModel
        {
            get { return _curUser; }
            set { SetProperty(ref _curUser, value); }
        }

        /*选择字段*/

        public string SelectedFiled { get; set; }
        /*选择字段的值*/
        public string SelectedFiledValue { get; set; }
        /*查询字段列表*/
        public List<string> FieldList { get; set; }
        /*是否停用*/
        public DelegateCommand SetStopUserCommand { get; set; }
        /*导出用户*/
        public DelegateCommand ExportUserCommand { get; set; }
        /*双击用户列表*/
        public DelegateCommand DelUserCommand { get; set; }
        public DelegateCommand RePasswordCommand { get; set; }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public void AddOrg()
        {
            if (CheckSelection())
            {
                GetOperationNode().AddOrg();
            }
        }

        public void EditOrg()
        {
            if (CheckSelection())
            {
                GetOperationNode().UpdateOrg();
            }
        }

        public void DeleteOrg()
        {
            if (CheckSelection())
            {
                if (NodeInfo.SelectedNode.OrgId == "100")
                {
                    MessageBox.Show("父级组织机构节点不能删除", "提示");
                    return;
                }
                GetOperationNode().DeleteOrg();
            }
        }

        private void InitOrg()
        {
            AddOrgCommand = new DelegateCommand(AddOrg);
            EditOrgCommand = new DelegateCommand(EditOrg);
            DeleteOrgCommand = new DelegateCommand(DeleteOrg);
         
            List<OPC_OrgInfo> orgList = AppEx.Container.GetInstance<IOrganizationService>().Search().ToList();
            Commands = new ReadOnlyCollection<DelegateCommand>(new[]
            {
                new DelegateCommand(SearchAction)
            });

            NodeInfo = new NodeInfo();
            NodeInfo.SelectedNodeChanged += (s, e) => RefreshCommands();
            Nodes = new NodeViewModel(NodeInfo);
            GetNodesTree(Nodes, orgList);
        }

        private void GetNodesTree(NodeViewModel node, List<OPC_OrgInfo> listOrg)
        {
            List<OPC_OrgInfo> orgParent = listOrg.Where(e => e.OrgID == e.ParentID).ToList();
            foreach (OPC_OrgInfo opcOrgInfo in orgParent)
            {
                NodeViewModel nv = node.AddSubNode(opcOrgInfo);
                GetNodesTreeChild(nv, listOrg);
            }
        }

        private void GetNodesTreeChild(NodeViewModel node, List<OPC_OrgInfo> listOrg)
        {
            List<OPC_OrgInfo> orgParent = listOrg.Where(e => e.OrgID != e.ParentID && e.ParentID == node.OrgId).ToList();
            foreach (OPC_OrgInfo opcOrgInfo in orgParent)
            {
                NodeViewModel nv = node.AddSubNode(opcOrgInfo);
                GetNodesTreeChild(nv, listOrg);
            }
        }

        private bool CheckSelection()
        {
            if (NodeInfo.SelectedNode == null) //建议改后台
            {
                MessageBox.Show("请选择组织机构节点", "提示");
                return false;
                ;
            }
            return true;
        }

        private void RefreshCommands()
        {
            foreach (DelegateCommand cmd in Commands)
            {
                cmd.Execute();
            }
        }

        private NodeViewModel GetOperationNode()
        {
            if (NodeInfo.SelectedNode == null)
                return Nodes;
            return NodeInfo.SelectedNode;
        }

        protected override IDictionary<string, object> GetFilter()
        {
            NodeViewModel node = GetOperationNode();
            int indexFiled = FieldList.IndexOf(SelectedFiled);
            var dicFilter = new Dictionary<string, object>
            {
                {"SearchField", indexFiled == -1 ? 1 : indexFiled},
                {"SearchValue", SelectedFiledValue},
                {"pageIndex", PageIndex},
                {"pageSize", PageSize},
                {"orgid", node.OrgId}
            };
            return dicFilter;
        }

        public override void SearchAction()
        {
            PageResult<OPC_AuthUser> PrResultTemp =
                AppEx.Container.GetInstance<IAuthenticationService>().Search(GetFilter());
            if (PrResultTemp == null || PrResultTemp.Result == null)
            {
                return;
            }
            PrResult = new PageDataResult<OPC_AuthUser>();
            PrResult.Models = PrResultTemp.Result.ToList();
            PrResult.Total = PrResultTemp.TotalCount;
        }

        /*重写 在选择组织结构 才能进行用户增加操作*/

        public override bool BeforeAdd(OPC_AuthUser t)
        {
            NodeViewModel curNode = GetOperationNode();
            if (curNode == null || curNode.OrgId == null)
            {
                MessageBox.Show("请选择部门", "提示");
                return false;
            }
            if (t == null) t = new OPC_AuthUser();
            t.OrgId = curNode.OrgId;
            t.DataAuthId = curNode.OrgId;
            t.DataAuthName = curNode.Name;
            return true;
        }

        public override bool BeforeEdit(IViewModel viewModel, OPC_AuthUser model)
        {
            var vm = viewModel as UserAddWindowViewModel;
            vm.OrgInfo = new OPC_OrgInfo {OrgID = model.DataAuthId, OrgName = model.DataAuthName};
            return true;
        }

        /*初始化页面固有的数据值*/

        private void InitUser()
        {
            FieldList = new List<string> {"登陆名", "姓名"};
            /*查询初始化*/
            SelectedFiledValue = "";
            SelectedFiled = "";
            SetStopUserCommand = new DelegateCommand(SetStopUser);
            DelUserCommand = new DelegateCommand(DelUser);
            PageIndex = 1;
            PageSize = 10;
            PrResult = new PageDataResult<OPC_AuthUser>();
            CurModel = new OPC_AuthUser();
            RePasswordCommand = new DelegateCommand(RePassword);
            UpdatePwdCommand = new DelegateCommand(UpdatePassWord);
        }

        private void RePassword()
        {
            OPC_AuthUser user = CurModel;
            if (user == null)
            {
                MessageBox.Show("请选择要操作的用户", "提示");
                return;
            }
            ResultMsg resultMsg = AppEx.Container.GetInstance<IAuthenticationService>().ResetPassword(user);
            MessageBox.Show(resultMsg.IsSuccess ? "重置密码成功" : "操作失败", "提示");
        }

        public void DelUser()
        {
            OPC_AuthUser user = CurModel;
            if (user == null)
            {
                MessageBox.Show("请选择要操作的用户", "提示");
                return;
            }
            DeleteAction(user);
            SearchAction();
        }

        private void SetStopUser()
        {
            OPC_AuthUser user = CurModel;
            if (user == null)
            {
                MessageBox.Show("请选择要操作的用户", "提示");
                return;
            }
            var iauth = GetDataService() as IAuthenticationService;
            iauth.SetIsStop(user);
            SearchAction();
        }

        protected override IBaseDataService<OPC_AuthUser> GetDataService()
        {
            return AppEx.Container.GetInstance<IAuthenticationService>();
        }

        private void UpdatePassWord()
        {
            OPC_AuthUser user = CurModel;
            if (user == null)
            {
                MessageBox.Show("请选择要操作的用户", "提示");
                return;
            }
            var userPadWin = AppEx.Container.GetInstance<UserUpdatePwd>();
            userPadWin.ViewModel.Model.LogonName = user.LogonName;
            if (userPadWin.ShowDialog() == true)
            {
                ResultMsg resultMsg = AppEx.Container.GetInstance<IAuthenticationService>().UpdatePassword(user,userPadWin.ViewModel.Model.NewPassword);
                MessageBox.Show(resultMsg.IsSuccess ? "重置密码成功" : "操作失败", "提示");
            }
        }
    }
}