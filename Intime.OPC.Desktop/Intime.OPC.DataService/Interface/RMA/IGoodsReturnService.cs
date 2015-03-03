using System.Collections.Generic;
using Intime.OPC.Domain.Customer;

namespace Intime.OPC.DataService.Interface.RMA
{
    public interface IGoodsReturnService
    {
        bool SetReturnGoodsCash(List<string> listRmaNo);
        bool SetReturnGoodsComplete(List<string> listRmaNo);
        //退货入库
        bool SetReturnGoodsInStorage(List<string> listRmaNo);
        IList<RMADto> GetRmaForReturnInStorage(GoodsReturnQueryCriteria returnGoodsCommonSearchDto);
        //打印退货单
        bool PrintReturnGoods(List<string> listRmaNo);
        bool PrintReturnGoodsComplete(List<string> listRmaNo);

        //退货入收银
        IList<RMADto> GetRmaForReturnCash(GoodsReturnQueryCriteria returnGoodsCommonSearchDto);

        IList<RMADto> GetRmaForReturnPrintDoc(GoodsReturnQueryCriteria returnGoodsCommonSearchDto);

        //导购退货收货查询
        IList<RMADto> GetRmaForShopperReturnOrReceivingPrintDoc(GoodsReturnQueryCriteria returnGoodsCommonSearchDto);

        //已完成退货单查询
        IList<RMADto> GetRmaForCompletedReturnGoods(GoodsReturnQueryCriteria returnGoodsCommonSearchDto);
    }
}