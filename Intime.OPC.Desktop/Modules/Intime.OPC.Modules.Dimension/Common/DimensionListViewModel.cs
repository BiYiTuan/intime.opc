using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Infrastructure.Service;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Intime.OPC.Modules.Dimension.Common
{
    public class DimensionListViewModel<TDimension, TDetailViewModel, TDimensionService> : ViewModelBase
        where TDimension : Intime.OPC.Domain.Models.Dimension, new()
        where TDetailViewModel : ModalDialogViewModel<TDimension>, new()
        where TDimensionService : IService<TDimension>
    {
        private const int MaxRecord = 10000;

        private ObservableCollection<TDimension> _models;
        private IQueryCriteria _queryCriteria;

        public DimensionListViewModel()
        {
            Models = new ObservableCollection<TDimension>();

            SelectAllCommand = new DelegateCommand<bool?>(OnSelectAll);
            EditCommand = new DelegateCommand<int?>(OnEdit);
            CreateCommand = new DelegateCommand(OnCreate);
            DeleteCommand = new AsyncDelegateCommand(OnDelete, CanExecute);
            QueryCommand = new AsyncDelegateCommand<string>(OnQuery);
            LoadNextPageCommand = new AsyncDelegateCommand(OnNextPageLoad);

            EditRequest = new InteractionRequest<TDetailViewModel>();
            CreateRequest = new InteractionRequest<TDetailViewModel>();
        }

        #region Properties

        [Import]
        public TDimensionService Service { get; set; }

        public ObservableCollection<TDimension> Models 
        { 
            get {return _models;}
            private set { SetProperty(ref _models, value); }
        }

        public int TotalCount { get; set; }

        public int LoadedCount { get; set; }

        public int MinLoadedPageIndex { get; set; }

        public int MaxLoadedPageIndex { get; set; }

        #endregion

        public InteractionRequest<TDetailViewModel> EditRequest { get; set; }

        public InteractionRequest<TDetailViewModel> CreateRequest { get; set; }

        #region Commands

        public ICommand SelectAllCommand { get; private set; }

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        public ICommand QueryCommand { get; private set; }

        public ICommand LoadNextPageCommand { get; private set; }

        public ICommand LoadPreviousPageCommand { get; private set; }

        #endregion

        #region Command handler

        protected bool CanExecute()
        {
            return _models != null && _models.Any(model => model.IsSelected);
        }

        private void OnSelectAll(bool? selected)
        {
            if (_models == null && !_models.Any()) return;

            _models.ForEach(model => model.IsSelected = selected.Value);
        }

        private void OnQuery(string name)
        {
            _queryCriteria = CreateQueryCriteria(name);

            var result = Service.Query(_queryCriteria);
            
            Models = new ObservableCollection<TDimension>(result.Data);

            LoadedCount = result.Data.Count;
            TotalCount = result.TotalCount;
            MinLoadedPageIndex = MaxLoadedPageIndex = 1;

            if (result.TotalCount == 0)
            {
                MvvmUtility.ShowMessageAsync("没有符合条件的记录", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void OnDelete()
        {
            var dialogSettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                AnimateShow = true,
                AnimateHide = false
            };
            var dialogResult = await MvvmUtility.ShowMessageAsync("确定要删除吗？", "删除", MessageBoxButton.YesNo, MessageBoxImage.Question,dialogSettings,MessageDialogStyle.AffirmativeAndNegative);
            if (dialogResult != MessageDialogResult.Affirmative) return;

            Models.ForEach(model =>
            {
                if (model.IsSelected) Service.Delete(model.Id);
            });

            while (Models.Any(brand => brand.IsSelected))
            {
                var selectedModel = Models.Where(model => model.IsSelected).FirstOrDefault();
                PerformActionOnUIThread(() => { Models.Remove(selectedModel); });
            }
        }

        private void OnCreate()
        {
            var detailViewModel = AppEx.Container.GetInstance<TDetailViewModel>();
            detailViewModel.Title = "新增";
            detailViewModel.Model = new TDimension();

            CreateRequest.Raise(detailViewModel, (viewModel) =>
            {
                if (viewModel.Accepted)
                {
                    Action create = () =>
                    {
                        var model = Service.Create(viewModel.Model);
                        _models.Insert(0, model);
                    };

                    PerformAction(create);
                }
            });
        }

        private void OnEdit(int? id)
        {
            var detailViewModel = AppEx.Container.GetInstance<TDetailViewModel>();
            detailViewModel.Title = "编辑";
            detailViewModel.Model = Service.Query(id.Value);

            EditRequest.Raise(detailViewModel, (viewModel) => 
            {
                if (viewModel.Accepted)
                {
                    Action edit = () =>
                    {
                        var updatedModel = Service.Update(viewModel.Model);
                        var modelToUpdate = _models.Where(model => model.Id == viewModel.Model.Id).FirstOrDefault();
                        int index = Models.IndexOf(modelToUpdate);

                        _models.Remove(modelToUpdate);
                        _models.Insert(index, updatedModel);
                    };

                    PerformAction(edit);
                }
            });
        }

        private void OnNextPageLoad()
        {
            if (_queryCriteria == null || MaxLoadedPageIndex * _queryCriteria.PageSize >= TotalCount) return;

            MaxLoadedPageIndex++;

            if (LoadedCount >= MaxRecord)
            {
                PerformActionOnUIThread(() => 
                {
                    Models.Clear();
                });
                LoadedCount = 0;
                MinLoadedPageIndex = MaxLoadedPageIndex;
            }

            _queryCriteria.PageIndex = MaxLoadedPageIndex;

            var result = Service.Query(_queryCriteria);
            foreach (var model in result.Data)
            {
                PerformActionOnUIThread(() => { Models.Add(model); });
            }

            LoadedCount += result.Data.Count;
        }

        private void OnPrevioustPageLoad()
        {
            if (_queryCriteria == null || MinLoadedPageIndex <= 1) return;

            MinLoadedPageIndex--;

            if (LoadedCount >= MaxRecord)
            {
                PerformActionOnUIThread(() =>
                {
                    Models.Clear();
                });
                LoadedCount = 0;
                MaxLoadedPageIndex = MinLoadedPageIndex;
            }

            _queryCriteria.PageIndex = MinLoadedPageIndex;

            var result = Service.Query(_queryCriteria);
            int position = 0;
            foreach (var model in result.Data)
            {
                PerformActionOnUIThread(() => { Models.Insert(position++, model); });
            }

            LoadedCount += result.Data.Count;
        }

        #endregion

        protected virtual IQueryCriteria CreateQueryCriteria(string name)
        {
            IQueryCriteria queryCriteria;

            if (!string.IsNullOrEmpty(name))
            {
                queryCriteria = new QueryByName { Name = name };
            }
            else
            {
                queryCriteria = new QueryAll();
            }

            return queryCriteria;
        }
    }
}
