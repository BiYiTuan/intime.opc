using Intime.OPC.Infrastructure.Mvvm.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public class AsyncQueryCommand: AsyncDelegateCommand
    {
        private Func<ICollection> _collectionObserver;
        private string _collectionName;

        public AsyncQueryCommand(Action executeMethod, Func<ICollection> collectionObserver, string collectionName)
            : base(executeMethod)
        {
            _collectionObserver = collectionObserver;
            _collectionName = collectionName;
        }

        public override void OnExecutionCompleted()
        {
            WarnIfEmpty();
        }

        private async void WarnIfEmpty()
        {
            var collection = _collectionObserver();
            if (collection == null || collection.Count == 0)
            {
                await MvvmUtility.ShowMessageAsync(string.Format("没有符合条件的{0}", _collectionName), "提示");
            }
        }
    }
}
