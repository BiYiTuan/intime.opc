using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using System.Collections.Generic;

namespace Intime.OPC.DataService.Interface.Customer
{
    public interface ICustomerInquiryService
    {
        PageResult<Order> GetOrder(string filter);
        PageResult<OPC_Sale> GetSaleByOrderNo(string orderNo);


        PageResult<OPC_ShippingSale> GetShipping(string filter);
        PageResult<Order> GetOrderByShippingId(string shippingId);
        bool SetCustomerMoneyGoods(List<string> rmaNoList);
        bool SetCannotReplenish(List<string> toList);
        PageResult<Order> GetOrderStockout(string orderfilter);
        PageResult<Order> GetOrderNoReplenish(string orderfilter);
    }
}