using System.Collections.Generic;
using Microsoft.Practices.Prism.Mvvm;

namespace Intime.OPC.Infrastructure.Mvvm.Model
{
    public class PageDataResult<T> : BindableBase
    {
        private List<T> _models;
        public int _total;

        public PageDataResult()
        {
            Models = new List<T>();
        }

        public int Total
        {
            get { return _total; }
            set { SetProperty(ref _total, value); }
        }

        public List<T> Models
        {
            get { return _models; }
            set { SetProperty(ref _models, value); }
        }
    }
}