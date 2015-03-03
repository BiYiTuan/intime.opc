using System;
using System.Windows;
using Intime.OPC.Domain;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public class ViewModelBase : ValidatableBindableBase
    {
        protected void OnException(Exception exception)
        {
            MvvmUtility.OnException(exception);
        }

        protected void PerformAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                OnException(exception);
            }
        }

        protected void PerformActionOnUIThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
