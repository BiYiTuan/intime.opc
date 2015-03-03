using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface.Customer;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.DataService.Impl.Customer
{
    [Export(typeof (ICustomerGoodsReturnService))]
    public class CustomerGoodsReturnService : ICustomerGoodsReturnService
    {
        public IList<OPC_SaleRMA> ReturnGoodsSearch(ReturnGoodsGet returnGoodsGet)
        {
            try
            {
                PageResult<OPC_SaleRMA> lst = RestClient.GetPage<OPC_SaleRMA>("custom/GetOrder",
                    returnGoodsGet.ToString());
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<OPC_SaleRMA>();
            }
        }

        public IList<OrderItemDto> GetOrderDetailByOrderNo(string orderNO)
        {
            try
            {
                PageResult<OrderItemDto> lst = RestClient.GetPage<OrderItemDto>("order/GetOrderItemsByOrderNo",
                    string.Format("orderNo={0}&pageIndex={1}&pageSize={2}", orderNO, 1, 300));
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<OrderItemDto>();
            }
        }

        public bool CustomerReturnGoodsSave(RMAPost rmaPost)
        {
            try
            {
                return RestClient.Post("rma/CreateSaleRMA", rmaPost);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #region 自主退货
        //自主退货明细查询
        public IList<OrderItemDto> GetOrderDetailByOrderNoWithSelf(string orderNo)
        {
            try
            {
                PageResult<OrderItemDto> lst = RestClient.GetPage<OrderItemDto>("custom/GetOrderItemsByOrderNoAutoBack",
                    string.Format("orderNo={0}&pageIndex={1}&pageSize={2}", orderNo, 1, 300));
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<OrderItemDto>();
            }
        }
        //自主退货明细查询
        public IList<OPC_SaleRMA> ReturnGoodsSearchForSelf(ReturnGoodsGet returnGoodsGet)
        {
            try
            {
                PageResult<OPC_SaleRMA> lst = RestClient.GetPage<OPC_SaleRMA>("custom/GetOrderAutoBack",
                    returnGoodsGet.ToString());
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<OPC_SaleRMA>();
            }
        }
        //自助退货  退货审核通过
        public bool CustomerReturnGoodsSelfPass(RMAPost rmaPost)
        {
            try
            {
                return RestClient.Post("rma/CreateSaleRmaAuto", rmaPost);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //自助退货 拒绝退货申请
        public bool CustomerReturnGoodsSelfReject(RMAPost rmaPost)
        {
            try
            {//接口不对
                return RestClient.Post("rma/CreateSaleRMA", rmaPost);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        }

        #endregion
    }
