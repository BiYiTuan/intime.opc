using System.ComponentModel.Composition;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Modules.Dimension.Views
{
    /// <summary>
    ///     StoreManage.xaml 的交互逻辑
    /// </summary>
    [Export("StoreManageWindow", typeof (IBaseView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class StoreManage
    {
        public StoreManage()
        {
            InitializeComponent();
        }

        public void CloseView()
        {
        }

        public void Cancel()
        {
        }

        public bool? ShowDialog()
        {
            return false;
        }
    }
}