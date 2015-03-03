using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Modules.Authority.Views
{
    /// <summary>
    ///     RoleWindow.xaml 的交互逻辑
    /// </summary>
    [Export("RoleListWindow", typeof (IBaseView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class RoleListWindow : UserControl, IBaseView
    {
        public RoleListWindow()
        {
            InitializeComponent();
        }

        public void CloseView()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }


        public bool? ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}