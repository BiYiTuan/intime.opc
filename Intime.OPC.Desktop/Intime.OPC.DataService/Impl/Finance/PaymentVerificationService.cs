using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Intime.OPC.DataService.Common;
using Intime.OPC.DataService.Interface.Financial;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.DataService.Impl.Trans
{
    [Export(typeof (IPaymentVerificationService))]
    public class PaymentVerificationService : IPaymentVerificationService
    {
        public bool ReturnGoodsPayVerify(string rmaNo, decimal money)
        {
            try
            {
                return RestClient.Post("rma/CompensateVerify", new {RmaNo = rmaNo, Money = money});
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public IList<RMADto> GetRmaByRmaOder(string rmaNo)
        {
            try
            {
                PageResult<RMADto> lst = RestClient.Get<RMADto>("rma/GetByRmaNo", string.Format("RmaNo={0}", rmaNo), 1,
                    300);
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<RMADto>();
            }
        }

        public IList<SaleRmaDto> GetRmaByReturnGoodPay(ReturnGoodsPayDto returnGoodsPay)
        {
            try
            {
                PageResult<SaleRmaDto> lst = RestClient.GetPage<SaleRmaDto>("rma/GetRmaByReturnGoodPay",
                    returnGoodsPay.ToString());
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<SaleRmaDto>();
            }
        }


        public bool FinancialVerifyPass(List<string> rmaNoList)
        {
            try
            {
                return RestClient.Post("rma/FinaceVerify", new {RmaNos = rmaNoList, Pass = true});
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool FinancialVerifyNoPass(List<string> rmaNoList)
        {
            try
            {
                return RestClient.Post("rma/FinaceVerify", new {RmaNos = rmaNoList, Pass = false});
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IList<RMADto> GetRmaByFilter(PackageReceiveDto packageReceive)
        {
            try
            {
                PageResult<RMADto> lst = RestClient.GetPage<RMADto>("rma/GetByFinaceDto", packageReceive.ToString());
                return lst.Result;
            }
            catch (Exception ex)
            {
                return new List<RMADto>();
            }
        }
    }
}