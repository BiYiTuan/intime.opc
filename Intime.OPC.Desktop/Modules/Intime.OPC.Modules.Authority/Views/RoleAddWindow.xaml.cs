using System.ComponentModel.Composition;
using MahApps.Metro.Controls;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Modules.Authority.Views
{
    /// <summary>
    ///     UserAddWindow.xaml 的交互逻辑
    /// </summary>
    [Export("RoleAddView", typeof (IBaseView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class RoleAddWindow : MetroWindow, IBaseView
    {
        public RoleAddWindow()
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