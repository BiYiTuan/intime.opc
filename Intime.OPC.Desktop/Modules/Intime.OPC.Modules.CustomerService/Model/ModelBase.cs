using System;
using System.ComponentModel;

namespace OPCApp.Order.Model
{
    public abstract class ModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}