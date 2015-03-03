using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Infrastructure.DataService;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public abstract class BaseViewModel<T> : BindableBase, IViewModel
    {
        private object _model;

        public BaseViewModel(string viewKey)
        {
            OKCommand = new DelegateCommand(OkAction);
            CancelCommand = new DelegateCommand(CancelAction);

            View = GetView(viewKey);
            View.DataContext = this;
        }

        public ICommand OKCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public object Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }


        public IBaseView View { get; set; }

        protected IBaseView GetView(string key)
        {
            return AppEx.Container.GetInstance<IBaseView>(key);
        }

        private void CancelAction()
        {
            doCancelAction();
            View.Cancel();
        }

        public void OkAction()
        {
            BeforeDoOKAction((T) Model);
            IList<ValidationResult> errors = Validate((T) Model);
            if (errors.Count > 0)
            {
                var strB = new StringBuilder();
                foreach (ValidationResult err in errors)
                {
                    strB.AppendLine(err.ErrorMessage);
                }

                MessageBox.Show(strB.ToString(), "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //var service = GetDataService();
            //var msg= service.Add((T)Model);
            //if (!msg.IsSuccess)
            //{
            //    MessageBox.Show(msg.Msg, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            View.CloseView();
            AfterDoOKAction();
        }

        public virtual bool BeforeDoOKAction(T t)
        {
            return true;
        }

        protected abstract IBaseDataService<T> GetDataService();

        protected IList<ValidationResult> Validate(T entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity, null, null);
            Validator.TryValidateObject(entity, context, results);
            return results;
        }


        protected virtual bool AfterDoOKAction()
        {
            return true;
        }

        protected virtual bool doCancelAction()
        {
            return true;
        }
    }
}