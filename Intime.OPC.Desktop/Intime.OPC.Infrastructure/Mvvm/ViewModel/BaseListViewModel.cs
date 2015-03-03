using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Infrastructure.DataService;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Infrastructure.Mvvm
{
    /// <summary>
    ///     Class BaseCollectionViewModel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseListViewModel<T> : BindableBase, IViewModel where T : new()
    {
        /// <summary>
        ///     The view key
        /// </summary>
        private readonly string ViewKey = "";

        /// <summary>
        ///     The _ collection
        /// </summary>
        private readonly ObservableCollection<T> _Collection;

        private int _total;

        /// <summary>
        ///     The _view
        /// </summary>
        private IBaseView _view;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseListViewModel{T}" /> class.
        /// </summary>
        /// <param name="viewKey">对应的view 导出时的键值</param>
        protected BaseListViewModel(string viewKey)
        {
            AddCommand = new DelegateCommand(AddAction);
            SearchCommand = new DelegateCommand(SearchAction);
            EditCommand = new DelegateCommand<T>(EditAction);
            DeleteCommand = new DelegateCommand<T>(DeleteAction);
            LoadCommand = new DelegateCommand(Load);
            ViewKey = viewKey;

            _Collection = new ObservableCollection<T>();
            Models = new ListCollectionView(_Collection);
            View.DataContext = this;
        }

        private int Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }


        /// <summary>
        ///     Gets or sets the edit view mode key.
        /// </summary>
        /// <value>The edit view mode key.</value>
        protected string EditViewModeKey { get; set; }

        /// <summary>
        ///     Gets or sets the add view mode key.
        /// </summary>
        /// <value>The add view mode key.</value>
        protected string AddViewModeKey { get; set; }

        // public IFilterViewModel FilterViewModel { get; set; }

        /// <summary>
        ///     Gets or sets the models.
        /// </summary>
        /// <value>The models.</value>
        public ICollectionView Models { get; set; }

        #region Methods

        #region Action

        protected virtual void Load()
        {
        }

        /// <summary>
        ///     Adds the action.
        /// </summary>
        public virtual void AddAction()
        {
            var w = AppEx.Container.GetInstance<IViewModel>(AddViewModeKey);
            if (!BeforeAdd((T) w.Model)) return;
            if (w.View.ShowDialog() == true)
            {
                IBaseDataService<T> service = GetDataService();
                ResultMsg resultMsg = service.Add((T) w.Model);
                if (resultMsg.IsSuccess)
                {
                    _Collection.Clear();
                    if (SearchCommand.CanExecute(null))
                    {
                        SearchCommand.Execute(null);
                    }
                }
                else
                {
                    MessageBox.Show("添加失败", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public virtual bool BeforeAdd(T t)
        {
            return true;
        }

        public virtual bool BeforeEdit(IViewModel t, T model)
        {
            return true;
        }

        /// <summary>
        ///     Edits the action.
        /// </summary>
        /// <param name="model">The model.</param>
        public void EditAction(T model)
        {
            if (model == null)
            {
                MessageBox.Show("请选择要编辑的记录", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var w = AppEx.Container.GetInstance<IViewModel>(EditViewModeKey);
            w.Model = model;
            BeforeEdit(w, model);
            if (w.View.ShowDialog() == true)
            {
                IBaseDataService<T> service = GetDataService();
                ResultMsg resultMsg = service.Edit((T) w.Model);
                if (resultMsg.IsSuccess)
                {
                    _Collection.Clear();
                    if (SearchCommand.CanExecute(null))
                    {
                        SearchCommand.Execute(null);
                    }
                }
                else
                {
                    MessageBox.Show("修改失败", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        ///     Deletes the action.
        /// </summary>
        /// <param name="model">The model.</param>
        public void DeleteAction(T model)
        {
            MessageBoxResult msg = MessageBox.Show("确定要删除吗？", "删除", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msg == MessageBoxResult.Yes)
            {
                IBaseDataService<T> service = GetDataService();
                ResultMsg r = service.Delete(model);
                if (r.IsSuccess)
                {
                    _Collection.Remove(model);
                }
                else
                {
                    MessageBox.Show("删除失败", "删除", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion

        #endregion

        /// <summary>
        ///     Gets or sets the view.
        /// </summary>
        /// <value>The view.</value>
        public IBaseView View
        {
            get
            {
                if (_view == null)
                {
                    _view = AppEx.Container.GetInstance<IBaseView>(ViewKey);
                }
                return _view;
            }
            set { _view = value; }
        }


        /// <summary>
        ///     Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public object Model
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        ///     Searches the action.
        /// </summary>
        public virtual void SearchAction()
        {
            IBaseDataService<T> service = GetDataService();
            PageResult<T> c = service.Search(GetFilter());
            _Collection.Clear();
            foreach (T item in c.Result)
            {
                _Collection.Add(item);
            }
            Total = c.TotalCount;
        }

        protected virtual IDictionary<string, object> GetFilter()
        {
            return null;
        }

        /// <summary>
        ///     Gets the data service.
        /// </summary>
        /// <returns>IBaseDataService{`0}.</returns>
        protected abstract IBaseDataService<T> GetDataService();

        #region Commands

        /// <summary>
        ///     添加
        /// </summary>
        /// <value>The add command.</value>
        public ICommand AddCommand { get; set; }


        /// <summary>
        ///     修改
        /// </summary>
        /// <value>The edit command.</value>
        public ICommand EditCommand { get; set; }


        /// <summary>
        ///     删除
        /// </summary>
        /// <value>The delete command.</value>
        public ICommand DeleteCommand { get; set; }

        /// <summary>
        ///     Gets or sets the search command.
        /// </summary>
        /// <value>The search command.</value>
        public ICommand SearchCommand { get; set; }

        public ICommand LoadCommand { get; set; }

        #endregion
    }
}