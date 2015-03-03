using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Intime.OPC.Modules.Logistics.Views
{
    /// <summary>
    /// PrintInvoiceViewModel.xaml 的交互逻辑
    /// </summary>
    [Export("PrintInvoice", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PrintInvoice
    {
        public PrintInvoice()
        {
            InitializeComponent();
        }

        [Import("PrintInvoiceViewModel")]
        public object ViewModel
        {
            set { DataContext = value; }
            get { return DataContext; }
        }
    }
}