using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Intime.OPC.Domain.Base
{
    public class DomainBase : INotifyDataErrorInfo, INotifyPropertyChanged
    {
        private readonly Dictionary<string, List<string>> _currentErrors;

        public DomainBase()
        {
            _currentErrors = new Dictionary<string, List<string>>();
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return (_currentErrors.Values);
            }

            MakeOrCreatePropertyErrorList(propertyName);
            return _currentErrors[propertyName];
        }

        public bool HasErrors
        {
            get { return (_currentErrors.Where(c => c.Value.Count > 0).Count() > 0); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void FireErrorsChanged(string property)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(property));
            }
        }

        public void ClearErrorFromProperty(string property)
        {
            MakeOrCreatePropertyErrorList(property);
            _currentErrors[property].Clear();
            FireErrorsChanged(property);
        }

        public void AddErrorForProperty(string property, string error)
        {
            MakeOrCreatePropertyErrorList(property);
            _currentErrors[property].Add(error);
            FireErrorsChanged(property);
        }

        private void MakeOrCreatePropertyErrorList(string propertyName)
        {
            if (!_currentErrors.ContainsKey(propertyName))
            {
                _currentErrors[propertyName] = new List<string>();
            }
        }
    }
}