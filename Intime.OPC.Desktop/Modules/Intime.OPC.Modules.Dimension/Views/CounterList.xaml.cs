using Intime.OPC.Modules.Dimension.ViewModels;
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

namespace Intime.OPC.Modules.Dimension.Views
{
    /// <summary>
    /// Interaction logic for CounterList.xaml
    /// </summary>
    [Export("CounterList", typeof(UserControl))]
    public partial class CounterList : UserControl
    {
        public CounterList()
        {
            InitializeComponent();
        }

        [Import]
        public CounterListViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}
