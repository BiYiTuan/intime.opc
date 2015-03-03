using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Intime.OPC.Modules.Logistics.Views
{
    /// <summary>
    ///     PrintInvoiceViewModel.xaml 的交互逻辑
    /// </summary>
    [Export("StoreOut", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class StoreOut
    {
        public StoreOut()
        {
            InitializeComponent();
        }

        [Import("StoreOutViewModel")]
        public object ViewModel
        {
            set { DataContext = value; }
            get { return DataContext; }
        }
    }
}