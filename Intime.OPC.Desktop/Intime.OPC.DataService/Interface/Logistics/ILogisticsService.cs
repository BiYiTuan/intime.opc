using System.Collections.Generic;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Domain.Dto;

namespace Intime.OPC.DataService.Interface.Trans
{
    public interface ILogisticsService
    {
        bool ExecutePrintSale(IList<string> saleOrderNoList);
        bool SetStatusAffirmPrintSaleFinish(IList<string> saleOrderNoList);
        bool SetStatusStoreInSure(IList<string> saleOrderNoList);
        bool SetStatusSoldOut(IList<string> saleOrderNoList);

        /// <summary>
        /// 快递单完成打应
        /// </summary>
        /// <param name="goodsOutCode">
        /// 即是对应数据库shipCode  对应客户端数据转换类goodsOutCode
        /// 快递单号 或者 发货单号 它们是一个值
        /// </param>
        /// <returns></returns>
        bool SetStatusPrintExpress(string goodsOutCode);
        bool SetSaleOrderShipped(IList<string> goodOutNoList);
        bool SetStatusPrintInvoice(IList<string> saleOrderNoList);
        PageResult<SaleDto> Search(string salesfilter, EnumSearchSaleStatus searchSaleStatus);
        PageResult<Order> SearchOrderBySale(string saleOrder);
        List<OPC_ShippingSale> GetListShipSaleBySale(string saleOrder);
        PageResult<OPC_SaleDetail> SelectSaleDetail(string saleOrderNo);
        bool SaveShip(ShippingSaleCreateDto dto);
        PageResult<OPC_ShippingSale> GetListShip(string filter);
        List<OPC_Sale> SelectSaleByShip(string shipCode);
        List<OPC_Sale> QuerySaleOrderByShippingId(int saleId);
    }
}