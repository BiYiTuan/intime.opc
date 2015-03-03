using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Modules.Authority.ViewModels;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.DataService;
using Intime.OPC.Infrastructure.Mvvm;

namespace Intime.OPC.Modules.Authority
{
    public class NodeViewModel : INotifyPropertyChanged
    {
        /*
         * 省去构造函数，公共方法和一些静态成员
         * */

        public NodeViewModel(NodeInfo info)
        {
            children = new ObservableCollection<NodeViewModel>();
            NodeInfo = info;
        }

        public NodeViewModel(NodeViewModel parent, OPC_OrgInfo orgInfo)
        {
            OPC_OrgInfo = orgInfo;
            Parent = parent;
            Name = orgInfo.OrgName;
            ParentId = orgInfo.ParentID;
            OrgId = orgInfo.OrgID;
            OrgType = orgInfo.OrgType;
            StoreOrSectionId = orgInfo.StoreOrSectionID;
            StoreOrSectionName = orgInfo.StoreOrSectionName;
            NodeInfo = parent.NodeInfo;
            children = new ObservableCollection<NodeViewModel>();
        }

        public OPC_OrgInfo OPC_OrgInfo { get; set; }

        public IOrganizationService GetDataService()
        {
            return AppEx.Container.GetInstance<IOrganizationService>();
        }

        public void AddOrg()
        {
            var w = AppEx.Container.GetInstance<IViewModel>("OrgViewModel");
            w.Model = new OPC_OrgInfo();
            if (w.View.ShowDialog() == true)
            {
                IOrganizationService service = GetDataService();
                var orgInfo = w.Model as OPC_OrgInfo;
                orgInfo.ParentID = OrgId;
                ResultMsg resultMsg = service.Add(orgInfo);
                if (resultMsg.IsSuccess)
                {
                    AddSubNode(resultMsg.Data as OPC_OrgInfo);
                }
                else
                {
                    MessageBox.Show("添加失败", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void UpdateOrg()
        {
            var w = AppEx.Container.GetInstance<IViewModel>("OrgViewModel") as OrgAddWindowViewMode;
            w.GetOrgRefreshStoreOrSection(OPC_OrgInfo.OrgType);
            w.Model = OPC_OrgInfo;
            if (w.View.ShowDialog() == true)
            {
                IBaseDataService<OPC_OrgInfo> service = GetDataService();
                ResultMsg resultMsg = service.Edit((OPC_OrgInfo) w.Model);
                if (resultMsg.IsSuccess)
                {
                    Update(w.Model as OPC_OrgInfo);
                }
                else
                {
                    MessageBox.Show("修改失败", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void DeleteOrg()
        {
            MessageBoxResult msg = MessageBox.Show("确定要删除吗？", "删除", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msg == MessageBoxResult.Yes)
            {
                IBaseDataService<OPC_OrgInfo> service = GetDataService();
                ResultMsg r = service.Delete(OPC_OrgInfo);
                if (r.IsSuccess)
                {
                    Remove();
                }
                else
                {
                    MessageBox.Show("删除失败", "删除", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #region 字段

        private readonly ObservableCollection<NodeViewModel> children;
        private bool _isExpanded;
        private bool _isSelected;
        private string _name;
        private string _orgId;
        private int? _orgType;
        private string _parentId;
        private int? _storeOrSectionId;
        private string _storeOrSectionName;

        #endregion

        #region 属性

        public NodeInfo NodeInfo { get; private set; }
        public NodeViewModel Parent { get; private set; }

        public string StoreOrSectionName
        {
            get { return _storeOrSectionName; }
            set
            {
                if (_storeOrSectionName != value)
                {
                    _storeOrSectionName = value;
                    OnPropertyChanged("StoreOrSectionName");
                }
            }
        }

        public int? StoreOrSectionId
        {
            get { return _storeOrSectionId; }
            set
            {
                if (_storeOrSectionId != value)
                {
                    _storeOrSectionId = value;
                    OnPropertyChanged("StoreOrSectionId");
                }
            }
        }

        public int? OrgType
        {
            get { return _orgType; }
            set
            {
                if (_orgType != value)
                {
                    _orgType = value;
                    OnPropertyChanged("OrgType");
                }
            }
        }

        public string ParentId
        {
            get { return _parentId; }
            set
            {
                if (_parentId != value)
                {
                    _parentId = value;
                    OnPropertyChanged("ParentId");
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string OrgId
        {
            get { return _orgId; }
            set
            {
                if (_orgId != value)
                {
                    _orgId = value;
                    OnPropertyChanged("OrgId");
                }
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                    OnIsExpandedChanged();
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                    OnIsSelectedChanged();
                }
            }
        }

        public ObservableCollection<NodeViewModel> Children
        {
            get { return children; }
        }

        #endregion

        #region 方法

        public void Remove()
        {
            if (Parent != null)
            {
                Parent.children.Remove(this);
                RefreshInfoCount(-1);
            }
        }


        public NodeViewModel AddSubNode(OPC_OrgInfo opcOrgInfo)
        {
            IsExpanded = true;
            var node = new NodeViewModel(this, opcOrgInfo);
            AddNodeViewModel(node);
            return node;
        }

        public void AddNodeViewModel(NodeViewModel nv)
        {
            children.Insert(0, nv);
        }

        public void Append(OPC_OrgInfo opcOrgInfo)
        {
            IsExpanded = true;
            children.Add(new NodeViewModel(this, opcOrgInfo));
        }

        public void Update(OPC_OrgInfo orgInfo)
        {
            Name = orgInfo.OrgName;
            ParentId = orgInfo.ParentID;
            OrgId = orgInfo.OrgID;
            OrgType = orgInfo.OrgType;
            StoreOrSectionId = orgInfo.StoreOrSectionID;
            StoreOrSectionName = orgInfo.StoreOrSectionName;
        }

        public void MoveUp()
        {
            if (Parent != null)
            {
                int idx = Parent.children.IndexOf(this);
                if (idx > 0)
                {
                    Parent.children.RemoveAt(idx);
                    Parent.children.Insert(--idx, this);
                }
                IsSelected = true;
            }
        }

        public void MoveDown()
        {
            if (Parent != null)
            {
                int idx = Parent.children.IndexOf(this);
                if (idx < Parent.children.Count - 1)
                {
                    Parent.children.RemoveAt(idx);
                    Parent.children.Insert(++idx, this);
                }
                IsSelected = true;
            }
        }

        #endregion

        #region 私有方法

        private void RefreshInfoCount(int addition)
        {
            //if (NodeInfo != null)
            //    //NodeInfo.SetCount(NodeInfo.Count + addition);
            //    return;
        }

        #endregion

        #region 事件

        public event EventHandler IsExpandedChanged;
        public event EventHandler IsSelectedChanged;

        protected virtual void OnIsExpandedChanged()
        {
            if (IsExpandedChanged != null)
                IsExpandedChanged(this, EventArgs.Empty);
        }

        protected virtual void OnIsSelectedChanged()
        {
            if (IsSelectedChanged != null)
                IsSelectedChanged(this, EventArgs.Empty);
            if (IsSelected)
                NodeInfo.SelectedNode = this;
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string proName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(proName));
        }

        #endregion
    }

    public class NodeInfo : BindableBase
    {
        private NodeViewModel _selectedNode;

        public NodeViewModel SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                if (_selectedNode != value)
                {
                    _selectedNode = value;
                    OnSelectedNodeChanged();
                    OnPropertyChanged("SelectedNode");
                }
            }
        }


        public event EventHandler SelectedNodeChanged;

        protected virtual void OnSelectedNodeChanged()
        {
            if (SelectedNodeChanged != null)
                SelectedNodeChanged(this, EventArgs.Empty);
        }

        #region INotifyPropertyChanged Members

        public new event PropertyChangedEventHandler PropertyChanged;

        protected new virtual void OnPropertyChanged(string proName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(proName));
        }

        #endregion
    }
}