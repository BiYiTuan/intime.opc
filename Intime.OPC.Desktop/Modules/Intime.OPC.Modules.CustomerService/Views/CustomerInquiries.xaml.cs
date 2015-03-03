using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.CustomerService.ViewModels;

namespace Intime.OPC.Modules.CustomerService.Views
{
    /// <summary>
    ///     PrintInvoiceViewModel.xaml 的交互逻辑
    /// </summary>
    [Export("CustomerInquiries", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CustomerInquiries
    {
        public CustomerInquiries()
        {
            InitializeComponent();
        }

        [Import(typeof(CustomerInquiriesViewModel))]
        public CustomerInquiriesViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as CustomerInquiriesViewModel; }
        }
    }
}