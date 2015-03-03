using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Intime.OPC.Infrastructure;
using System.Windows.Threading;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Intime.OPC.Desktop
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class Login : SimpleDialog
    {
        public Login()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            Logon();
        }

        private void Logon()
        {
            string name = logonName.Text;
            string pwd = logonPwd.Password;

            if (string.IsNullOrEmpty(name))
            {
                errorMessage.Text = "用户名不能为空";
                return;
            }
            if (string.IsNullOrEmpty(pwd))
            {
                errorMessage.Text = "密码不能为空";
                return;
            }
            bool suceeded = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                Task<bool> task = Task.Run<bool>(() => AppEx.Login(name, pwd));
                suceeded = task.Result;
            }
            catch(Exception ex)
            {
                errorMessage.Text = string.Format("发生未知错误:{0}", ex.Message);
            }

            Task.WaitAll();
            Mouse.OverrideCursor = null;

            if (!suceeded)
            {
                errorMessage.Text = "用户名或者密码错误";
                return;
            }

            CloseDialog();
        }

        private void OnAuthenticationFieldsPreviewKeyDown(object sender, KeyEventArgs e)
        {
            errorMessage.Text = string.Empty;

            if (e.Key == Key.Enter)
            {
                Logon();
            }
        }

        private void OnExitButtonClick(object sender, RoutedEventArgs e)
        {
            CloseDialog();
        }

        private async void CloseDialog()
        {
            var metroWindow = (MetroWindow)System.Windows.Application.Current.MainWindow;
            await DialogManager.HideMetroDialogAsync(metroWindow, this);
        }
    }
}