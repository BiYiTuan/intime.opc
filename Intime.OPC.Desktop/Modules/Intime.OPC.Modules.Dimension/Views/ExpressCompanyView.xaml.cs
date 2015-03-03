using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OPCApp.BasicsManage.ViewModels;

namespace OPCApp.BasicsManage.Views
{
    //[Export("ExpressCompany")]
    //[PartCreationPolicy(CreationPolicy.NonShared)]
    /// <summary>
    /// ExpressCompany.xaml 的交互逻辑
    /// </summary>
    public partial class ExpressCompanyView : UserControl
    {
        public ExpressCompanyView()
        {
            InitializeComponent();
            this.Loaded += (sender, e) => { this.DataContext = new ExpressCompanyViewModel(); };
        }
    }
}
