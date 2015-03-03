using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Modules.GoodsReturn.ViewModel;
using Microsoft.Practices.Prism.Mvvm;
using Intime.OPC.Domain.Customer;
using Intime.OPC.DataService.Interface.RMA;

namespace Intime.OPC.Modules.GoodsReturn.Common
{
    public abstract class BaseReturnGoodsSearchCommonWithRma : BindableBase
    {
        public BaseReturnGoodsSearchCommonWithRma()
        {
            CommandSearch = new AsyncDelegateCommand(OnQuery);
            ReturnGoodsCommonSearchDto = new GoodsReturnQueryCriteria();
            CustomReturnGoodsUserControlViewModel = new CustomReturnGoodsUserControlViewModel { MasterViewModel = this }; 
        }

        public CustomReturnGoodsUserControlViewModel CustomReturnGoodsUserControlViewModel { get; set; }

        public GoodsReturnQueryCriteria ReturnGoodsCommonSearchDto { get; set; }

        public ICommand CommandSearch { get; set; }

        public abstract void QueryRma();

        private void OnQuery()
        {
            QueryRma();

            MvvmUtility.WarnIfEmpty(CustomReturnGoodsUserControlViewModel.RmaList, "退货单");
        }
    }
}