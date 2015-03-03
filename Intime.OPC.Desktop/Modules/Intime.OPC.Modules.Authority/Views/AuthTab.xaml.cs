using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace OPCApp.Order.Views
{
    /// <summary>
    ///     OrderSearch.xaml 的交互逻辑
    /// </summary>
    [Export("UserListWindow")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AuthTab : UserControl
    {
        public AuthTab()
        {
            InitializeComponent();
        }
    }
}