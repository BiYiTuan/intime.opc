using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Infrastructure.Utilities;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Dimension.Criteria;
using Intime.OPC.Modules.Dimension.Services;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    [Export(typeof(ApprovalViewModel))]
    public class ApprovalViewModel : ViewModelBase
    {
        [Import]
        private IStoreApplicationService _service;
        [Import]
        private IService<Department> _departmentService;
        private IList<ApplicationInfo> _applicationInfos;
        private IList<Department> _departments;

        public ApplicationQueryCriteria QueryCriteria { get; set; }
        public IList<KeyValuePair<int, string>> ApprovalStatuses { get; set; }
        public IList<KeyValue> Stores { get; set; }
        public IList<Department> Departments
        {
            get { return _departments; }
            set { SetProperty(ref _departments, value); }
        }
        public IList<ApplicationInfo> ApplicationInfos
        {
            get { return _applicationInfos; }
            set { SetProperty(ref _applicationInfos, value); }
        }

        public ICommand QueryCommand { get; set; }
        public ICommand ApproveCommand { get; set; }
        public ICommand SelectAllCommand { get; set; }
        public ICommand ReloadDepartmentCommand { get; set; }

        [ImportingConstructor]
        public ApprovalViewModel(ICommonInfo dimensionService)
        {
            QueryCriteria = new ApplicationQueryCriteria();
            Stores = dimensionService.GetStoreList();
            ApprovalStatuses = EnumerationUtility.ToList<ApprovalStatus>();
            QueryCommand = new AsyncDelegateCommand(OnQuery);
            ApproveCommand = new AsyncDelegateCommand(OnApprove, CanCommandExecute);
            SelectAllCommand = new DelegateCommand<bool?>(OnSelectAll);
            ReloadDepartmentCommand = new AsyncDelegateCommand(OnDepartmentReload);
        }

        private bool CanCommandExecute()
        {
            return ApplicationInfos != null && ApplicationInfos.Any(application => application.IsSelected == true);
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void OnQuery()
        {
            ApplicationInfos = _service.QueryAll(QueryCriteria);
            MvvmUtility.WarnIfEmpty(ApplicationInfos,"申请信息");
        }

        /// <summary>
        /// 批准
        /// </summary>
        private void OnApprove()
        {
            ApplicationInfos.Where(application => application.IsSelected == true)
                .ForEach(application => _service.Approve(application));

            ApplicationInfos = _service.QueryAll(QueryCriteria);
        }

        /// <summary>
        /// Load departments by selected store
        /// </summary>
        private void OnDepartmentReload()
        {
            var queryCriteria = new QueryDepartmentByStoreId { StoreId = QueryCriteria.StoreId };
            Departments = _departmentService.QueryAll(queryCriteria);
            QueryCriteria.DepartmentId = Departments.First().Id;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="isSelected">是否已选择</param>
        private void OnSelectAll(bool? isSelected)
        {
            if (ApplicationInfos == null || !ApplicationInfos.Any()) return;

            ApplicationInfos.ForEach(salesOrder => salesOrder.IsSelected = isSelected.Value);
        }
    }
}
