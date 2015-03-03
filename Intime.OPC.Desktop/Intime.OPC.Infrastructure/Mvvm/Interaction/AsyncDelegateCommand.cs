using Intime.OPC.CustomControls;
using Intime.OPC.Infrastructure.ErrorHandling;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public class AsyncDelegateCommand<T> : ICommand
    {
        private Action<T> _executeMethod;
        private Func<T, bool> _canExecuteMethod;
        private Action<Exception> _errorHandler;

        public AsyncDelegateCommand(Action<T> executeMethod)
            :this(executeMethod, null, MvvmUtility.OnException)
        {
        }

        public AsyncDelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, MvvmUtility.OnException)
        {
        }

        public AsyncDelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, Action<Exception> errorHandler)
        {
            this._executeMethod = executeMethod;
            this._canExecuteMethod = canExecuteMethod;
            this._errorHandler = errorHandler;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod == null) return true;
            return _canExecuteMethod((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        } 

        public virtual void Execute(object parameter)
        {
            if (_canExecuteMethod != null && !_canExecuteMethod((T)parameter)) return;

            ProgressBarWindow progressDialog = new ProgressBarWindow();
            Task task = new Task(() => 
            {
                try
                {
                    //Set synchronization context to capture the exceptions thrown from async void event handlers.
                    SynchronizationContext.SetSynchronizationContext(new AsyncSynchronizationContext());

                    _executeMethod((T)parameter);
                }
                catch (Exception exception)
                {
                    if (_errorHandler != null) _errorHandler(exception);
                }
                finally
                {
                    CloseDialog(progressDialog);
                }
                
            });

            task.Start();
            progressDialog.ShowDialog();
        }

        private void CloseDialog(Window dialog)
        {
            dialog.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                dialog.Hide();
                dialog.Close();
            }));
        }
    }

    public class AsyncDelegateCommand : ICommand
    {
        private Action _executeMethod;
        private Func<bool> _canExecuteMethod;
        private Action<Exception> _errorHandler;

        public AsyncDelegateCommand(Action executeMethod)
            : this(executeMethod, null, MvvmUtility.OnException)
        {
        }

        public AsyncDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, MvvmUtility.OnException)
        {
        }

        public AsyncDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod,Action<Exception> errorHandler)
        {
            this._executeMethod = executeMethod;
            this._canExecuteMethod = canExecuteMethod;
            this._errorHandler = errorHandler;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteMethod ==null) return true;
            return _canExecuteMethod();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        } 

        public void Execute(object parameter)
        {
            if (_canExecuteMethod != null && !_canExecuteMethod()) return;

            ProgressBarWindow progressDialog = new ProgressBarWindow();
            Task task = new Task(() =>
            {
                try
                {
                    //Set synchronization context to capture the exceptions thrown from async void event handlers.
                    SynchronizationContext.SetSynchronizationContext(new AsyncSynchronizationContext());

                    _executeMethod();

                    OnExecutionCompleted();
                }
                catch (Exception exception)
                {
                    if (_errorHandler == null) throw;

                    _errorHandler(exception);
                }
                finally
                {
                    CloseDialog(progressDialog);
                }
            });
  
            task.Start();
            progressDialog.ShowDialog();
        }

        public virtual void OnExecutionCompleted()
        {}

        private void CloseDialog(Window dialog)
        {
            dialog.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                dialog.Hide();
                dialog.Close();
            }));
        }
    }
}
