using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Intime.OPC.Modules.GoodsReturn.Common;
using Microsoft.Practices.Prism.Commands;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Mvvm.Utility;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof(ReturnAcceptCashierViewModel))]
    public class ReturnAcceptCashierViewModel : BaseReturnGoodsSearchCommonWithRma
    {
        public ReturnAcceptCashierViewModel()
        {
            CustomReturnGoodsUserControlViewModel.IsReturnAcceptCashierConfirmVisible = true;
            CustomReturnGoodsUserControlViewModel.IsReturnAcceptCashierVisible = true;
        }

        public override void QueryRma()
        {
            CustomReturnGoodsUserControlViewModel.RmaList =
                    AppEx.Container.GetInstance<IGoodsReturnService>()
                        .GetRmaForReturnCash(ReturnGoodsCommonSearchDto)
                        .ToList();
        }
    }
}