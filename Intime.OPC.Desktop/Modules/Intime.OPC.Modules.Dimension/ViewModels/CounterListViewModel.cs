using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Modules.Dimension.Common;
using Intime.OPC.Domain.Models;
using Intime.OPC.Modules.Dimension.Criteria;
using Intime.OPC.Domain.Dto;
using Intime.OPC.DataService.Interface.Trans;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    [Export(typeof(CounterListViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CounterListViewModel : DimensionListViewModel<Counter,CounterViewModel,IService<Counter>>
    {
        [ImportingConstructor]
        public CounterListViewModel(ICommonInfo dimensionService)
        {
            EnableCommand = new AsyncDelegateCommand(() => OnEnable(false), CanExecute);
            DisableCommand = new AsyncDelegateCommand(() => OnEnable(true), CanExecute);

            Stores = dimensionService.GetStoreList();
            QueryCriteria = new QueryCounterByComposition();
        }

        public QueryCounterByComposition QueryCriteria { get; set; }

        public IList<KeyValue> Stores { get; set; }

        protected override IQueryCriteria CreateQueryCriteria(string name)
        {
            return QueryCriteria;
        }

        #region Commands

        public ICommand DisableCommand { get; private set; }

        public ICommand EnableCommand { get; private set; }

        #endregion

        #region Command handler

        private void OnEnable(bool repealed)
        {
            Models.ForEach(counter => 
            {
                if (counter.IsSelected && counter.Repealed != repealed)
                {
                    counter.Repealed = repealed;
                    counter = Service.Update(counter);
                }
            });
        }
        #endregion
    }
}
