using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Infrastructure;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Desktop.ViewModels
{
    public class UserConfig
    {
        public string UserKey { get; set; }
        public string Password { get; set; }
        public string ServiceUrl { get; set; }
        public string Version { get; set; }
    }

    [Export(typeof(SettingsViewModel))]
    public class SettingsViewModel : BindableBase
    {
        private UserConfig modelConfig;
        private bool _isFlyoutVisible;

        public UserConfig Model
        {
            get { return modelConfig; }
            set { SetProperty(ref modelConfig, value); }
        }

        public SettingsViewModel()
        {
            LoadSettings();
            SaveCommand = new DelegateCommand(OnSave);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        public bool IsFlyoutVisible
        {
            get { return _isFlyoutVisible; }
            set { SetProperty(ref _isFlyoutVisible, value); }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        

        private void OnCancel()
        {
            IsFlyoutVisible = false;
        }

        private void OnSave()
        {
            SaveSettings();
            IsFlyoutVisible = false;
        }

        private void LoadSettings()
        {
            Model = new UserConfig
            {
                Password = ConfigurationManager.AppSettings["consumerSecret"],
                UserKey = ConfigurationManager.AppSettings["consumerKey"],
                ServiceUrl = ConfigurationManager.AppSettings["apiAddress"],
                Version = ConfigurationManager.AppSettings["version"]
            };
        }

        private void SaveSettings()
        {
            try
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["consumerKey"].Value = Model.UserKey;
                configuration.AppSettings.Settings["consumerSecret"].Value = Model.Password;
                configuration.AppSettings.Settings["apiAddress"].Value = Model.ServiceUrl;
                configuration.AppSettings.Settings["version"].Value = Model.Version;
                configuration.Save();

                AppEx.Config.Password = Model.Password;
                AppEx.Config.ServiceUrl = Model.ServiceUrl;
                AppEx.Config.UserKey = Model.UserKey;
                AppEx.Config.Version = Model.Version;
            }
            catch (Exception)
            {
                MvvmUtility.ShowMessageAsync("保存配置出错，请联系管理员。", "配置错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
