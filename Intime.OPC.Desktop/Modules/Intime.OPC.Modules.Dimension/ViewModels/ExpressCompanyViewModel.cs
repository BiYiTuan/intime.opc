using OPCApp.BasicsManage.Command;
using OPCApp.BasicsManage.Model;
using OPCApp.BasicsManage.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPCApp.BasicsManage.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ExpressCompanyViewModel : BaseViewModel
    {
        private List<ExpressCompany> _expressCompanyGroup;
        public List<ExpressCompany> expressCompanyGroup
        {
            get { return _expressCompanyGroup; }
            set
            {
                _expressCompanyGroup = value;
                OnPropertyChanged("expressCompanyGroup");
            }
        }

        private int _selectedIndex;
        public int selectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("selectedIndex");
            }
        }

        private string _searchText;//绑定搜索条件
        public string searchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("searchText");
            }
        }

        public EditCompanyEntryModel _editCompanyEntryModel;
        public EditCompanyEntryModel editCompanyEntryModel
        {
            get { return _editCompanyEntryModel; }
            set
            {
                _editCompanyEntryModel = value;
                OnPropertyChanged("editCompanyEntry");
            }
        }

        private readonly RelayCommand _searchCommand;
        private readonly RelayCommand _addRowCommand;
        private readonly RelayCommand _editRowCommand;
        private readonly RelayCommand _deleteRowCommand;

        /// <summary>
        /// 搜索快递公司
        /// </summary>
        public ICommand SearchCommand
        {
            get { return _searchCommand; }
        }

        /// <summary>
        /// 编辑记录
        /// </summary>
        public ICommand EditRowCommand
        {
            get { return _editRowCommand; }
        }
        /// <summary>
        /// 添加记录
        /// </summary>
        public ICommand AddRowCommand
        {
            get { return _addRowCommand; }
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public ICommand DelectRowCommand
        {
            get { return _deleteRowCommand; }
        }

        public ExpressCompanyViewModel()
        {
            expressCompanyGroup = new ExpressCompanyGroup().initExpressCompanyGroup();
            _searchCommand = new RelayCommand(this.SearchCompany, this.CheckSearchCommandCanExecute);
            _editRowCommand = new RelayCommand(this.EditRow, this.CheckEditRowCommandCanExecute);
            _addRowCommand = new RelayCommand(this.AddRow, this.CheckAddRowCommandCanExecute);
            _deleteRowCommand = new RelayCommand(this.DeleteRow, this.CheckDeleteRowCommandCanExecute);
        }

        /// <summary>
        /// 搜索快递公司
        /// </summary>
        public void SearchCompany()
        {
            MessageBoxResult messageResult = MessageBox.Show("测试版只支持快递公司名称查询", "提示", MessageBoxButton.YesNo);
            if (messageResult == MessageBoxResult.Yes)
            {
                List<ExpressCompany> mylists = null;
                if (!string.IsNullOrEmpty(searchText))
                {
                    mylists = new List<ExpressCompany>();
                    foreach (ExpressCompany p in expressCompanyGroup)
                    {
                        if (p.ExpressName.Equals(searchText))
                            mylists.Add(p);
                    }
                    expressCompanyGroup = mylists;
                }
            }
        }

        /// <summary>
        /// 删除行
        /// </summary>
        protected void DeleteRow()
        {
            MessageBoxResult messageResult = MessageBox.Show("确定删除选中快递公司信息？", "提示", MessageBoxButton.YesNo);
            if (messageResult == MessageBoxResult.Yes)
            {
                if (this.selectedIndex != -1)
                {
                    List<ExpressCompany> companyList = new List<ExpressCompany>();
                    for (int i = 0; i < expressCompanyGroup.Count; i++)
                    {
                        if (i != this.selectedIndex)
                            companyList.Add(expressCompanyGroup[i]);
                    }
                    expressCompanyGroup = companyList;
                }
            }
        }

        /// <summary>
        /// 添加行
        /// </summary>
        protected void AddRow()
        {
            this.editCompanyEntryModel = new EditCompanyEntryModel();
            EditCompanyEntryDialog dialog = new EditCompanyEntryDialog(editCompanyEntryModel);
            this.editCompanyEntryModel.window = dialog;
            bool? isOK = dialog.ShowDialog();
            if (isOK.HasValue)
            {
                if ((bool)isOK == false)
                {
                    ExpressCompany editCompanyEntry = this.editCompanyEntryModel.editCompanyEntry;
                    List<ExpressCompany> companyList = new List<ExpressCompany>();
                    for (int i = 0; i < expressCompanyGroup.Count; i++)
                    {
                        companyList.Add(expressCompanyGroup[i]);
                    }
                    companyList.Add(editCompanyEntry);
                    expressCompanyGroup = companyList;
                }
            }
        }
        
        /// <summary>
        /// 编辑行
        /// </summary>
        /// <param name="row"></param>
        protected void EditRow()
        {
            this.editCompanyEntryModel = new EditCompanyEntryModel(this.expressCompanyGroup[this.selectedIndex]);
            EditCompanyEntryDialog dialog = new EditCompanyEntryDialog(editCompanyEntryModel);
            this.editCompanyEntryModel.window = dialog;
            bool? isOK = dialog.ShowDialog();
            if (isOK.HasValue)
            {
                if ((bool)isOK == false)
                {
                    ExpressCompany editCompanyEntry = this.editCompanyEntryModel.editCompanyEntry;
                    List<ExpressCompany> companyList = new List<ExpressCompany>();
                    for (int i = 0; i < expressCompanyGroup.Count; i++)
                    {
                        if (i != this.selectedIndex)
                            companyList.Add(expressCompanyGroup[i]);
                        else
                            companyList.Add(editCompanyEntry);
                    }
                    expressCompanyGroup = companyList;
                }
            }
        }

        protected bool CheckSearchCommandCanExecute()
        {
            return this.searchText != "";
        }

        protected bool CheckAddRowCommandCanExecute()
        {
            return this.expressCompanyGroup != null;
        }

        protected bool CheckEditRowCommandCanExecute()
        {
            return this.selectedIndex!=-1;
        }

        protected bool CheckDeleteRowCommandCanExecute()
        {
            return this.selectedIndex != -1;
        }
    }
}
