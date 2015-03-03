using System;

namespace Intime.OPC.Infrastructure.Mvvm.Toast
{
    /// <summary>
    /// Interaction logic for NotificationDialog.xaml
    /// </summary>
    public partial class NotificationDialog : ToastBase
    {
        public NotificationDialog()
        {
            InitializeComponent();

            action.Click += (sender, e) => Close();
        }

        public string Message
        {
            set { message.Content = value; }
        }

        public Action Action
        {
            set 
            {
                if (value != null)
                {
                    action.Click += (sender, e) => value(); 
                }
            }
        }
    }
}
