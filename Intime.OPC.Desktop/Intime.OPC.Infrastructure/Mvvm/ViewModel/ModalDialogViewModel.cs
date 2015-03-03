using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public abstract class ModalDialogViewModel<TModel> : ViewModelBase, INotification, IInteractionRequestAware
        where TModel : Intime.OPC.Domain.Models.Model, new()
    {
        private TModel _model;

        public ModalDialogViewModel()
        {
            OKCommand = new DelegateCommand(() =>
            {
                _model.ValidateProperties();
                FlattenErrors();
                if (!_model.HasErrors)
                {
                    Accepted = true;
                    FinishInteraction();
                }
            });
            CancelCommand = new DelegateCommand(() =>
            {
                Accepted = false;
                FinishInteraction();
            });
        }

        public virtual TModel Model 
        {
            get { return _model; }
            set { SetProperty(ref _model, value); } 
        }

        public ICommand OKCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public bool Accepted { get; set; }

        public Action FinishInteraction { get; set; }

        public INotification Notification { get; set; }

        public object Content { get; set; }

        public string Title { get; set; }

        private List<string> FlattenErrors()
        {
            List<string> errors = new List<string>();
            Dictionary<string, List<string>> allErrors = _model.GetAllErrors();
            foreach (string propertyName in allErrors.Keys)
            {
                foreach (var errorString in allErrors[propertyName])
                {
                    errors.Add(propertyName + ": " + errorString);
                }
            }
            return errors;
        }
    }
}
