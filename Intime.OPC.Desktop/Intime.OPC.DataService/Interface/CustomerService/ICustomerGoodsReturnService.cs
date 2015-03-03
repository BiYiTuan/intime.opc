using System.Collections.Generic;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.DataService.Interface.Customer
{
    public interface ICustomerGoodsReturnService
    {
        IList<OPC_SaleRMA> ReturnGoodsSearch(ReturnGoodsGet returnGoodsGet);
        IList<OrderItemDto> GetOrderDetailByOrderNo(string orderNO);
        bool CustomerReturnGoodsSave(RMAPost shipComment);
        IList<OrderItemDto> GetOrderDetailByOrderNoWithSelf(string orderNo);

        IList<OPC_SaleRMA> ReturnGoodsSearchForSelf(ReturnGoodsGet returnGoodsGet);
        bool CustomerReturnGoodsSelfPass(RMAPost rmaPost);
        bool CustomerReturnGoodsSelfReject(RMAPost rmaPost);
    }
}