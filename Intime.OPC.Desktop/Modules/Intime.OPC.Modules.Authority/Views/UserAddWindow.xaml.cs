using System.ComponentModel.Composition;
using MahApps.Metro.Controls;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Modules.Authority.Views
{
    [Export("UserView", typeof (IBaseView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class UserAddWindow : MetroWindow, IBaseView
    {
        public UserAddWindow()
        {
            InitializeComponent();
        }

        public void Cancel()
        {
            DialogResult = false;
            Close();
        }

        public void CloseView()
        {
            DialogResult = true;
            Close();
        }
    }
}