using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Events;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Intime.OPC.Desktop.ViewModels
{
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : BindableBase, IPartImportsSatisfiedNotification
    {
        [Import(AllowRecomposition = false)]
        private IModuleManager _moduleManager;
        [Import(AllowRecomposition = false)]
        private IRegionManager _regionManager;
        [Import(AllowRecomposition = false)]
        private GlobalEventAggregator _eventAggregator;

        private string _logoutButtonText;
        private bool _hasLoggedIn;
        private string _applicationTitle;

        public ShellViewModel()
        {
            OpenSettingsCommand = new DelegateCommand(OnSettingsOpen);
            LogonCommand = new DelegateCommand(OnLogon);
        }

        public void OnImportsSatisfied()
        {
            SubscribeAuthenticationEvent();
            SubscribeNavigatingEvent();

            ApplicationTitle = "Intime OPC v" + Assembly.GetExecutingAssembly().GetName().Version;
            LogoutButtonText = "登陆";
        }

        public ICommand LogoutCommand { get; set; }
        public ICommand OpenSettingsCommand { get; set; }
        public ICommand LogonCommand { get; set; }

        public string LogoutButtonText
        {
            get { return _logoutButtonText; }
            set { SetProperty(ref _logoutButtonText, value); }
        }

        public string ApplicationTitle
        {
            get { return _applicationTitle; }
            set { SetProperty(ref _applicationTitle, value); }
        }

        private void SubscribeAuthenticationEvent()
        {
            var authenticationEvent = _eventAggregator.GetEvent<AuthenticationEvent>();
            authenticationEvent.Subscribe(OnAuthenticated, ThreadOption.UIThread);
        }

        private void SubscribeNavigatingEvent()
        {
            var navigatingToViewEvent = this._eventAggregator.GetEvent<NavigatingToViewEvent>();
            navigatingToViewEvent.Subscribe(arg => OnNavigatingToView(arg.AuthorizedMenu), ThreadOption.UIThread);
        }

        private void OnNavigatingToView(OPC_AuthMenu viewMenu)
        {
            var mainWindow = Application.Current.MainWindow as MetroWindow;
            if (mainWindow == null) return;

            mainWindow.Show();
            mainWindow.WindowState = WindowState.Maximized;
        }

        private void OnLogon()
        {
            if (_hasLoggedIn)
            {
                AppEx.Logout();
                ClearContentRegion();
            }
            ShowLoginDailog();
        }

        private void OnSettingsOpen()
        {
            var mainWindow = Application.Current.MainWindow as MetroWindow;
            if (mainWindow == null) return;

            var settingsFlyout = mainWindow.Flyouts.Items[0] as Flyout;
            if (settingsFlyout == null) return;

            settingsFlyout.IsOpen = !settingsFlyout.IsOpen;
        }

        private void OnAuthenticated(ILoginModel loginModel)
        {
            _hasLoggedIn = (loginModel != null) && !string.IsNullOrEmpty(loginModel.Token);
            LogoutButtonText = _hasLoggedIn ? "注销" : "登录...";
        }

        private async void ShowLoginDailog()
        {
            var loginDialog = AppEx.Container.GetInstance<Login>();
            var mainWindow = Application.Current.MainWindow as MetroWindow;
            if (mainWindow == null) return;

            await mainWindow.ShowMetroDialogAsync(loginDialog);
        }

        private void ClearContentRegion()
        {
            while (_regionManager.Regions[RegionNames.MainContentRegion].Views.Count() > 0)
            {
                _regionManager.Regions[RegionNames.MainContentRegion].Remove(
                    _regionManager.Regions[RegionNames.MainContentRegion].Views.FirstOrDefault());
            }
        }
    }
}
