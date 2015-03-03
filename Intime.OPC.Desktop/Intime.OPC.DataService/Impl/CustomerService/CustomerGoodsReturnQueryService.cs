using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Intime.OPC.DataService.Common;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.DataService.Customer
{
    [Export(typeof (ICustomerGoodsReturnQueryService))]
    public class CustomerGoodsReturnQueryService : ICustomerGoodsReturnQueryService
    {
        //laoda 物流退回 订单查询
        public IList<OrderDto> ReturnGoodsTransSearch(ReturnGoodsInfoGet goodInfoGet)
        {
            try
            {
                PageResult<OrderDto> lst = RestClient.GetPage<OrderDto>("order/GetByOrderNoShippingBack",
                    goodInfoGet.ToString());
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<OrderDto>();
            }
        }

        //lao da 退货赔偿退回
        public IList<OrderDto> ReturnGoodsFinancialSearch(ReturnGoodsInfoGet goodInfoGet)
        {
            try
            {
                PageResult<OrderDto> lst = RestClient.GetPage<OrderDto>("rma/GetByOrderNoReturnGoodsCompensation",
                    goodInfoGet.ToString());
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<OrderDto>();
            }
        }

        public IList<OrderDto> ReturnGoodsRmaSearch(ReturnGoodsInfoGet goodInfoGet)
        {
            try
            {
                PageResult<OrderDto> lst = RestClient.GetPage<OrderDto>("order/GetByReturnGoodsInfo",
                    goodInfoGet.ToString());
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<OrderDto>();
            }
        }

        public IList<RMADto> GetRmaTransByOrderNo(string orderNo)
        {
            try
            {
                PageResult<RMADto> lst = RestClient.GetPage<RMADto>("rma/GetByOrderNoShippingBack",
                    string.Format("orderNo={0}&pageIndex={1}&pageSize={2}", orderNo, 1, 300));
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<RMADto>();
            }
        }

        public IList<RMADto> GetRmaFinancialByOrderNo(string orderNo)
        {
            try
            {
                PageResult<RMADto> lst = RestClient.GetPage<RMADto>("rma/GetByOrderNoReturnGoodsCompensation",
                    string.Format("orderNo={0}&pageIndex={1}&pageSize={2}", orderNo, 1, 300));
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<RMADto>();
            }
        }

        public IList<RMADto> GetRmaByOrderNo(string orderNo)
        {
            try
            {
                PageResult<RMADto> lst = RestClient.GetPage<RMADto>("rma/GetByOrderNo",
                    string.Format("orderNo={0}&pageIndex={1}&pageSize={2}", orderNo, 1, 300));
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<RMADto>();
            }
        }


        public IList<RmaDetail> GetRmaDetailByRmaNo(string rmaNo)
        {
            try
            {
                PageResult<RmaDetail> lst = RestClient.GetPage<RmaDetail>("rma/GetRmaDetailByRmaNo",
                    string.Format("rmaNo={0}&pageIndex={1}&pageSize={2}", rmaNo, 1, 300));
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<RmaDetail>();
            }
        }

        public bool AgreeReturnGoods(List<string> rmaNos)
        {
            try
            {
                return RestClient.Post("rma/SetSaleRmaServiceAgreeGoodsBack", rmaNos);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}