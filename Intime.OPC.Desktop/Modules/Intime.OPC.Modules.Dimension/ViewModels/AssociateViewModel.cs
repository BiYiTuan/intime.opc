using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Infrastructure.Utilities;
using Intime.OPC.Modules.Dimension.Criteria;
using Intime.OPC.Modules.Dimension.Services;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    [Export(typeof(AssociateViewModel))]
    public class AssociateViewModel : ViewModelBase
    {
        [Import]
        private IAssociateService _service;
        [Import]
        private IService<Department> _departmentService;

        private IList<Associate> _associates;
        private IList<Department> _departments;

        public AssociateQueryCriteria QueryCriteria { get; set; }
        public IList<KeyValue> Stores { get; set; }
        public IList<KeyValuePair<int, string>> PermissionLevels { get; set; }

        public IList<Department> Departments
        {
            get { return _departments; }
            set { SetProperty(ref _departments, value); }
        }

        public IList<Associate> Associates
        {
            get { return _associates; }
            set { SetProperty(ref _associates, value); }
        }

        public ICommand QueryCommand { get; set; }
        public ICommand RenotifyCommand { get; set; }
        public ICommand DemoteCommand { get; set; }
        public ICommand SelectAllCommand { get; set; }
        public ICommand ReloadDepartmentCommand { get; set; }

        [ImportingConstructor]
        public AssociateViewModel(ICommonInfo dimensionService)
        {
            QueryCriteria = new AssociateQueryCriteria();
            Stores = dimensionService.GetStoreList();
            PermissionLevels = EnumerationUtility.ToList<AssociatePermission>();
            QueryCommand = new AsyncDelegateCommand(OnQuery);
            RenotifyCommand = new AsyncDelegateCommand(OnNotify, CanCommandExecute);
            DemoteCommand = new AsyncDelegateCommand(OnDemote, CanCommandExecute);
            SelectAllCommand = new DelegateCommand<bool?>(OnSelectAll);
            ReloadDepartmentCommand = new AsyncDelegateCommand(OnDepartmentReload);
        }

        private bool CanCommandExecute()
        {
            return Associates != null && Associates.Any(application => application.IsSelected == true);
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void OnQuery()
        {
            Associates = _service.QueryAll(QueryCriteria);
            MvvmUtility.WarnIfEmpty(Associates, "导购信息");
        }

        /// <summary>
        /// 降权
        /// </summary>
        private void OnDemote()
        {
            Associates.Where(associate => associate.IsSelected == true)
                .ForEach(associate => _service.Demote(associate));
        }

        /// <summary>
        /// 发送通知
        /// </summary>
        private void OnNotify()
        {
            Associates.Where(associate => associate.IsSelected == true)
                .ForEach(associate => _service.Notify(associate));
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
            if (Associates == null || !Associates.Any()) return;

            Associates.ForEach(salesOrder => salesOrder.IsSelected = isSelected.Value);
        }
    }
}
