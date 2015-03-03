using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.DataService.Impl.RMA
{
    [Export(typeof (IGoodsReturnService))]
    public class GoodsReturnService : IGoodsReturnService
    {
        #region 退货入收银

        public IList<RMADto> GetRmaForReturnCash(GoodsReturnQueryCriteria returnGoodsCommonSearchDto)
        {
            PageResult<RMADto> lst = RestClient.GetPage<RMADto>("trans/GetRmaCashByExpress", returnGoodsCommonSearchDto.ToString());
            return lst.Result;
        }

        public bool SetReturnGoodsCash(List<string> listRmaNo)
        {
            return RestClient.Post("trans/SetRmaCash", listRmaNo);
        }

        public bool SetReturnGoodsComplete(List<string> listRmaNo)
        {
            return RestClient.Post("trans/SetRmaCashOver", listRmaNo);
        }

        #endregion

        #region 退货入库

        public bool SetReturnGoodsInStorage(List<string> listRmaNo)
        {
            return RestClient.Post("trans/SetRmaShipInStorage", listRmaNo);
        }

        public IList<RMADto> GetRmaForReturnInStorage(GoodsReturnQueryCriteria returnGoodsCommonSearchDto)
        {
            PageResult<RMADto> lst = RestClient.GetPage<RMADto>("trans/GetRmaReturnByExpress",
                returnGoodsCommonSearchDto.ToString());
            return lst.Result;
        }

        #endregion

        #region 打印退货单

        public bool PrintReturnGoods(List<string> listRmaNo)
        {
            throw new NotImplementedException();
        }

        public bool PrintReturnGoodsComplete(List<string> listRmaNo)
        {
            return RestClient.Post("trans/SetRmaPint", listRmaNo);
        }

        public IList<RMADto> GetRmaForReturnPrintDoc(GoodsReturnQueryCriteria returnGoodsCommonSearchDto)
        {
            PageResult<RMADto> lst = RestClient.GetPage<RMADto>("trans/GetRmaPrintByExpress",
                returnGoodsCommonSearchDto.ToString());
            return lst.Result;
        }

        #endregion

        #region 导购退货收货查询

        public IList<RMADto> GetRmaForShopperReturnOrReceivingPrintDoc(
            GoodsReturnQueryCriteria returnGoodsCommonSearchDto)
        {
            PageResult<RMADto> lst = RestClient.GetPage<RMADto>("custom/GetRmaByShoppingGuide",
                returnGoodsCommonSearchDto.ToString());
            return lst.Result;
        }

        #endregion

        #region 已经完成退货单查询

        public IList<RMADto> GetRmaForCompletedReturnGoods(GoodsReturnQueryCriteria returnGoodsCommonSearchDto)
        {
            PageResult<RMADto> lst = RestClient.GetPage<RMADto>("custom/GetRmaByAllOver",
                returnGoodsCommonSearchDto.ToString());
            return lst.Result;
        }

        #endregion
    }
}