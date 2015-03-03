using System.Collections.Generic;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Models;
using Intime.OPC.Domain.ReturnGoods;

namespace Intime.OPC.DataService.Interface.RMA
{
    public interface IPackageService
    {
        //退货包裹管理的 退货包裹确认 查询收货单
        IList<SaleRmaDto> GetSaleRma(PackageReceiveDto packageReceiveDto);
        //退货包裹管理的 退货包裹确认 查询退货单
        IList<RMADto> GetRma(PackageReceiveDto packageReceiveDto);
        //退货包裹管理的 退货包裹确认  查询退货单明细 通过退货单
        IList<RmaDetail> GetRmaDetailByRma(string rmaNo);
        //退货包裹管理的 物流收货确认
        bool ReceivingGoodsSubmit(List<string> rmaNosList);
        //退货包裹审核
        bool TransVerifyPass(List<string> rmaNoList);
        bool TransVerifyNoPass(List<string> rmaNoList);

        //退回付款确认 查询 退货单列表
        IList<RMADto> GetRmaByFilter(PackageReceiveDto packageReceive);
        //退货包裹审核

        //打印快递单
        IList<OPC_ShippingSale> GetShipListWithReturnGoods(RmaExpressDto rmaExpress);
        bool UpdateShipWithReturnExpress(RmaExpressSaveDto rmaExpressSaveDto);
        IList<RMADto> GetRmaForPrintExpress(string rmaNo);

        IList<OrderDto> GetOrderByOrderNo(string orderNo);

        bool ShipPrintComplete(string rmaNO);
        bool ShipPrint(string rmaNo);

        //打印快递单完成
        IList<RMADto> GetRmaForPrintExpressConnect(string rmaNo);
        IList<Order> GetOrderForPrintExpressConnect(string orderNo);
        IList<OPC_ShippingSale> GetShipListWithReturnGoodsConnect(RmaExpressDto rmaExpress);
        bool ShipPrintComplateConnect(string shipNo);
    }
}