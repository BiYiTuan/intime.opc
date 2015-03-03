using System.ComponentModel.Composition;
using System.Windows;
using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.Commands;
using Intime.OPC.Modules.Authority.ViewModels;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Modules.Authority.Views
{
    [Export(typeof(UserUpdatePwd))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class UserUpdatePwd : MetroWindow
    {
        public UserUpdatePwd()
        {
            InitializeComponent();
        }
        public UserUpdatePwdViewModel ViewModel
        {
            get { return DataContext as UserUpdatePwdViewModel; }
            set { DataContext = value; }
        }

        [ImportingConstructor]
          public UserUpdatePwd(UserUpdatePwdViewModel configView)
        {
            InitializeComponent();
            ViewModel = configView;
            ViewModel.OkCommand=new DelegateCommand(RePassWord);
            ViewModel.CancelCommand=new DelegateCommand(Cancel);
        }

        private void Cancel()
        {
            this.DialogResult = false;
            this.Close();
        }

        private void RePassWord()
        {
            if (ViewModel.Model.NewPassword!=ViewModel.Model.RePassword)
            {
                MessageBox.Show("二次输入的密码不一致", "提示");
                return;
            }
            this.DialogResult = true;
            this.Close();
        }
     
    }
}