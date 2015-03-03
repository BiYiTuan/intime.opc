using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.CustomerService.ViewModels;

namespace Intime.OPC.Modules.CustomerService.Views
{
    /// <summary>
    ///     PrintInvoiceViewModel.xaml 的交互逻辑
    /// </summary>
    [Export("CustomerReturnSearch", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CustomerReturnSearchMain
    {
        public CustomerReturnSearchMain()
        {
            InitializeComponent();
        }

        [Import("CustomerReturnGoodsMainViewModel")]
        public CustomerReturnGoodsMainViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as CustomerReturnGoodsMainViewModel; }
        }
    }
}