using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.CustomerService.ViewModels;

namespace Intime.OPC.Modules.CustomerService.Views
{
    /// <summary>
    ///     PrintInvoiceViewModel.xaml 的交互逻辑
    /// </summary>
    [Export("CustomerStockoutRemind", typeof(UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CustomerStockoutRemind
    {
        public CustomerStockoutRemind()
        {
            InitializeComponent();
        }

        [Import(typeof(CustomerStockoutRemindViewModel))]
        public CustomerStockoutRemindViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as CustomerStockoutRemindViewModel; }
        }
    }
}