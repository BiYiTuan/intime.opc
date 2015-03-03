using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Intime.OPC.Modules.Logistics.Views
{
    /// <summary>
    ///     PrintInvoiceViewModel.xaml 的交互逻辑
    /// </summary>
    [Export("SaleOrderPickupView", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class SaleOrderPickupView
    {
        public SaleOrderPickupView()
        {
            InitializeComponent();
        }

        [Import("SaleOrderPickupViewModel")]
        public object ViewModel
        {
            set { DataContext = value; }
            get { return DataContext; }
        }
    }
}