using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Intime.OPC.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;

namespace Intime.OPC.Desktop.ViewModels
{
    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : BindableBase
    {
        private string _errorMessage;

        public LoginViewModel()
        {
            LogonCommand = new DelegateCommand<object>(OnLogon);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        public string UserName { get; set; }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage,value); }
        }

        public ICommand LogonCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private void OnCancel()
        {
            throw new NotImplementedException();
        }

        private void OnLogon(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;

            if (string.IsNullOrEmpty(UserName))
            {
                ErrorMessage = "用户名不能为空";
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                ErrorMessage = "密码不能为空";
                return;
            }
            bool suceeded = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                Task<bool> task = Task.Run<bool>(() => AppEx.Login(UserName, password));
                suceeded = task.Result;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("发生未知错误:{0}", ex.Message);
            }

            Task.WaitAll();
            Mouse.OverrideCursor = null;

            if (!suceeded)
            {
                ErrorMessage = "用户名或者密码错误";
                return;
            }
        }
    }
}
