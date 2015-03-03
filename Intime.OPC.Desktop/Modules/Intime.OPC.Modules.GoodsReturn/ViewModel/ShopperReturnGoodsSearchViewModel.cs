using System.ComponentModel.Composition;
using System.Linq;
using Intime.OPC.Modules.GoodsReturn.Common;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.Infrastructure;


namespace Intime.OPC.Modules.GoodsReturn.ViewModel
{
    [Export(typeof (ShopperReturnGoodsSearchViewModel))]
    public class ShopperReturnGoodsSearchViewModel : BaseReturnGoodsSearchCommonWithRma
    {
        public override void QueryRma()
        {
            CustomReturnGoodsUserControlViewModel.RmaList =
                AppEx.Container.GetInstance<IGoodsReturnService>()
                    .GetRmaForShopperReturnOrReceivingPrintDoc(ReturnGoodsCommonSearchDto)
                    .ToList();
        }
    }
}