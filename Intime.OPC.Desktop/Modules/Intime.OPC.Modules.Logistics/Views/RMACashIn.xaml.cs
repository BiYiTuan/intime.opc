using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Intime.OPC.Modules.Logistics.Views
{
    /// <summary>
    ///     PrintInvoiceViewModel.xaml 的交互逻辑
    /// </summary>
    [Export("RMACashIn", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class RMACashIn
    {
        public RMACashIn()
        {
            InitializeComponent();
        }

        [Import("RMACashInViewModel")]
        public object ViewModel
        {
            set { DataContext = value; }
            get { return DataContext; }
        }
    }
}