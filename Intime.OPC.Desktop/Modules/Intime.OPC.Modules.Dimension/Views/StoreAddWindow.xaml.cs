using System.ComponentModel.Composition;
using MahApps.Metro.Controls;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Modules.Dimension.Views
{
    /// <summary>
    ///     StoreManage.xaml 的交互逻辑
    /// </summary>
    [Export("StoreView", typeof (IBaseView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class StoreAddWindow : MetroWindow, IBaseView
    {
        public StoreAddWindow()
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