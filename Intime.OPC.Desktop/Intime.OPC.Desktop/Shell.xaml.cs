using System.ComponentModel.Composition;
using Intime.OPC.Desktop.ViewModels;
using MahApps.Metro.Controls;

namespace Intime.OPC.Desktop
{
    [Export(typeof(MetroWindow))]
    public partial class Shell : MetroWindow
    {
        public Shell()
        {
            InitializeComponent();
        }

        [Import]
        public ShellViewModel ViewModel
        {
            set { DataContext = value; }
        }
    }
}