using Intime.OPC.Infrastructure.Mvvm.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public static class ObservableCollectionExtension
    {
        public static void SafelyRemove<T>(this ObservableCollection<T> collection, Expression<Func<T,bool>> match)
        {
            MvvmUtility.PerformActionOnUIThread(() => collection.Remove(match));
        }

        public static void SafelyInsert<T>(this ObservableCollection<T> collection, int index, T item)
        {
            MvvmUtility.PerformActionOnUIThread(() => collection.Insert(index, item));
        }
    }
}
