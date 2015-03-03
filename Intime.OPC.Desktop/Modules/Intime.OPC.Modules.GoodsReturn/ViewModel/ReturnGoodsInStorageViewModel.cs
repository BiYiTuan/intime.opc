using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Modules.GoodsReturn.Common;
using Microsoft.Practices.Prism.Commands;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof (ReturnGoodsInStorageViewModel))]
    public class ReturnGoodsInStorageViewModel : BaseReturnGoodsSearchCommonWithRma
    {
        public ReturnGoodsInStorageViewModel()
        {
            CustomReturnGoodsUserControlViewModel.IsReturnGoodsInStoreButtionVisible = true;
        }

        public override void QueryRma()
        {
            CustomReturnGoodsUserControlViewModel.RmaList =
                AppEx.Container.GetInstance<IGoodsReturnService>()
                    .GetRmaForReturnInStorage(ReturnGoodsCommonSearchDto)
                    .ToList();
        }
    }
}