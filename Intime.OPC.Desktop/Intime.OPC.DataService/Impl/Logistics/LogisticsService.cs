using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface.Trans;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Domain.Dto;

namespace Intime.OPC.DataService.Impl.Trans
{
    [Export(typeof (ILogisticsService))]
    public class LogisticsService : ILogisticsService
    {
        public PageResult<SaleDto> Search(string salesfilter, EnumSearchSaleStatus enumSearchSaleStatus)
        {
            string url = "";
            switch (enumSearchSaleStatus)
            {
                case EnumSearchSaleStatus.CompletePrintSearchStatus:
                    url = "sale/GetSaleNoPickUp";
                    break;
                case EnumSearchSaleStatus.StoreInDataBaseSearchStatus:
                    url = "sale/GetSalePrintSale";
                    break;
                case EnumSearchSaleStatus.StoreOutDataBaseSearchStatus:
                    url = "sale/GetSaleShipInStorage";
                    break;
                case EnumSearchSaleStatus.PrintInvoiceSearchStatus:
                    url = "sale/GetSalePrintInvoice";
                    break;
                case EnumSearchSaleStatus.PrintExpressSearchStatus:
                    url = "sale/GetSalePrintExpress";
                    break;
                case EnumSearchSaleStatus.ShipedSearchStatus:
                    url = "sale/GetShipped";
                    break;
            }
            PageResult<SaleDto> lst = RestClient.GetPage<SaleDto>(url, salesfilter);
            return lst;
        }

        public PageResult<OPC_ShippingSale> GetListShip(string filter)
        {
            PageResult<OPC_ShippingSale> shipSales = RestClient.GetPage<OPC_ShippingSale>("trans/GetShippingSale",
                filter + "&pageIndex=1&pageSize=50");
            return shipSales;
        }

        /*根据销售单拿到订单*/

        public PageResult<Order> SearchOrderBySale(string orderNo)
        {
            var order = RestClient.GetSingle<Order>("order/GetOrderByOderNo", string.Format("OrderNo={0}", orderNo));
            return new PageResult<Order>(new List<Order> { order }, 100);
        }

        /*完成打印销售单 状态*/ //SetSaleOrderPrintSale

        public bool SetStatusAffirmPrintSaleFinish(IList<string> saleOrderNoList)
        {
            try
            {
                return RestClient.Put("sale/SetSaleOrderFinishPrintSale", saleOrderNoList);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*设置销售单入库 状态*/

        public bool SetStatusStoreInSure(IList<string> saleOrderNoList)
        {
            try
            {
                return RestClient.Put("sale/SetSaleOrderShipInStorage", saleOrderNoList);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*设置销售单出库 状态*/

        /*设置销售单缺货 状态*/

        public bool SetStatusSoldOut(IList<string> saleOrderNoList)
        {
            try
            {
                return RestClient.Put("sale/SetSaleOrderStockOut", saleOrderNoList);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<OPC_ShippingSale> GetListShipSaleBySale(string saleOrderNo)
        {
            try
            {
                var shipSale = RestClient.GetSingle<OPC_ShippingSale>("trans/GetShippingSaleBySaleOrderNo",
                    string.Format("saleOrderNo={0}", saleOrderNo));
                return shipSale == null ? new List<OPC_ShippingSale>() : new List<OPC_ShippingSale> {shipSale};
            }
            catch (Exception ex)
            {
                return new List<OPC_ShippingSale>();
            }
        }

        public PageResult<OPC_SaleDetail> SelectSaleDetail(string saleOrderNo)
        {
            IList<OPC_SaleDetail> lst = RestClient.Get<OPC_SaleDetail>("sale/GetSaleOrderDetails",
                string.Format("saleOrderNo={0}", saleOrderNo));
            return new PageResult<OPC_SaleDetail>(lst, 100);
        }


        public bool SetStatusPrintInvoice(IList<string> saleOrderNoList)
        {
            try
            {
                return RestClient.Put("sale/SetSaleOrderPrintInvoice", saleOrderNoList);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool SetStatusPrintExpress(string shipCode)
        {
            try
            {
                return RestClient.Put("sale/SetSaleOrderPrintExpress", shipCode);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /*打印销售单*/

        public bool ExecutePrintSale(IList<string> saleOrderNoList)
        {
            try
            {
                return RestClient.Put("sale/SetSaleOrderPrintSale", saleOrderNoList);
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool SaveShip(ShippingSaleCreateDto dto)
        {
            try
            {
                return RestClient.Post("trans/CreateShippingSale", dto);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Obsolete("废弃此方法，调用 QuerySaleOrderByShippingId Wallace 2014-05-02")]
        public List<OPC_Sale> SelectSaleByShip(string shipCode)
        {
            try
            {
                List<OPC_Sale> lst = RestClient.Get<OPC_Sale>("trans/GetSaleByShippingSaleNo",
                    string.Format("shippingSaleNo={0}", shipCode)).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                return new List<OPC_Sale>();
            }
        }
        public List<OPC_Sale> QuerySaleOrderByShippingId(int packageId)
        {
            return RestClient.Get<OPC_Sale>("trans/QueryRelatedSaleOrders", string.Format("packageId={0}", packageId)).ToList();
        }

        public bool SetSaleOrderShipped(IList<string> shipSalerNoList)
        {
            try
            {
                return RestClient.Put("sale/SetSaleOrderShipped", shipSalerNoList);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool PrintSale(IList<string> saleOrderNoList)
        {
            try
            {
                return RestClient.Put("sale/PrintSale", saleOrderNoList);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SetSaleOrderStockOut(IList<string> saleOrderNoList)
        {
            try
            {
                return RestClient.Put("sale/SetSaleOrderStockOut", saleOrderNoList);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}