using System.ComponentModel.Composition;
using System.Linq;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Modules.GoodsReturn.Common;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof (ReturnGoodsEntryPrintViewModel))]
    public class ReturnGoodsEntryPrintViewModel : BaseReturnGoodsSearchCommonWithRma
    {
        public ReturnGoodsEntryPrintViewModel()
        {
            CustomReturnGoodsUserControlViewModel.IsPrintButtonVisible = true;
            CustomReturnGoodsUserControlViewModel.IsPrintPreviewButtonVisible = true;
            CustomReturnGoodsUserControlViewModel.IsCompletePrintButtionVisible = true;
        }

        public override void QueryRma()
        {
            CustomReturnGoodsUserControlViewModel.RmaList =
                AppEx.Container.GetInstance<IGoodsReturnService>()
                    .GetRmaForReturnPrintDoc(ReturnGoodsCommonSearchDto)
                    .ToList();

        }
    }
}