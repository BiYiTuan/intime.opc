using System.ComponentModel.Composition;
using System.Windows;
using Intime.OPC.Modules.CustomerService.ViewModels;
using System.Windows.Controls;

namespace Intime.OPC.Modules.CustomerService.Views
{
    [Export(typeof(ProductImageView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ProductImageView : UserControl
    {
        public ProductImageView()
        {
            InitializeComponent();
        }
    }
}
