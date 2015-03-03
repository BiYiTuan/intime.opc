using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Events;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Mvvm;

namespace Intime.OPC.Modules.Authority.ViewModels
{
    [Export(typeof (AuthNavaeigationItemViewModel))]
    public class AuthNavaeigationItemViewModel : BindableBase
    {
        [Import]
        private IMenuDataService _menuDataService;
        [Import]
        private IRegionManager _regionManager;
        [Import]
        private GlobalEventAggregator _eventAggregator;
        private IEnumerable<MenuGroup> menuGroups;

        [ImportingConstructor]
        public AuthNavaeigationItemViewModel(IRegionManager regionManager, GlobalEventAggregator eventAggregator, IMenuDataService menuDataService)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _menuDataService = menuDataService;
            LoadViewCommand = new DelegateCommand<OPC_AuthMenu>(menu => LoadView(menu));

            SubscribeAuthenticationEvents();

            SubscribeNavigatingEvent();
        }

        public ICommand LoadViewCommand { get; set; }

        public IEnumerable<MenuGroup> GroupItems
        {
            get { return menuGroups; }
            set { SetProperty(ref menuGroups, value); }
        }

        private void SubscribeAuthenticationEvents()
        {
            var authenticationEvent = _eventAggregator.GetEvent<AuthenticationEvent>();
            authenticationEvent.Subscribe(OnAuthenticated, ThreadOption.BackgroundThread);
        }

        private void SubscribeNavigatingEvent()
        {
            var navigatingToViewEvent =_eventAggregator.GetEvent<NavigatingToViewEvent>();
            navigatingToViewEvent.Subscribe(arg => LoadView(arg.AuthorizedMenu, arg.NavigatedCallback), ThreadOption.UIThread);
        }

        private void OnAuthenticated(ILoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Token))
            {
                GroupItems = null;
            }
            else
            {
                GroupItems = _menuDataService.GetMenus();
            }
            
            //Publish the authorized feature retrieved event.
            _eventAggregator.GetEvent<AuthorizedFeatureRetrievedEvent>().Publish(GroupItems);
        }

        private async void LoadView(OPC_AuthMenu menu, Action viewLoadedCallback = null)
        {
            if (menu == null) return;

            SelectCurrentMenu(menu);

            var url = menu.Url;
            var controller = await MvvmUtility.ShowProgressAsync("正在加载，请稍候...");
            try
            {
                Action registerView = () =>
                {
                    ClearContentRegion();

                    if (url.Contains("ViewModel"))
                    {
                        _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion,
                            () => AppEx.Container.GetInstance<IViewModel>(url).View);
                    }
                    else
                    {
                        _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion,
                            () => AppEx.Container.GetInstance<UserControl>(url));
                    }
                };

                Action action = () =>
                {
                    Thread.Sleep(2000);
                    MvvmUtility.PerformActionOnUIThread(registerView);
                };

                await Task.Run(action);

                if (viewLoadedCallback != null) viewLoadedCallback();
            }
            catch (ViewRegistrationException)
            {
                MessageBox.Show("功能正在开发中...", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载过程中出现错误: {0}", ex.Message), "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await controller.CloseAsync();
        }

        private void SelectCurrentMenu(OPC_AuthMenu menu)
        {
            if (GroupItems == null || !GroupItems.Any()) return;

            foreach (var groupItem in GroupItems)
            {
                foreach (var menuItem in groupItem.Items)
                {
                    menuItem.IsSelected = (menuItem.Id == menu.Id);
                }
            }
        }

        private void ClearContentRegion()
        {
            while (_regionManager.Regions[RegionNames.MainContentRegion].Views.Any())
            {
                _regionManager.Regions[RegionNames.MainContentRegion].Remove(
                    _regionManager.Regions[RegionNames.MainContentRegion].Views.FirstOrDefault());
            }
        }
    }
}