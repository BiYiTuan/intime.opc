using System.Collections.Generic;
using Intime.OPC.Domain.Customer;
using OPCAPP.Domain.Dto.Financial;
using Intime.OPC.Domain.Dto.Financial;

namespace Intime.OPC.DataService.Interface.Financial
{
    public interface IPaymentVerificationService
    {
        //退货付款确认  
        bool ReturnGoodsPayVerify(string rmaNo, decimal money);
        IList<RMADto> GetRmaByRmaOder(string rmaNo);
        IList<SaleRmaDto> GetRmaByReturnGoodPay(ReturnGoodsPayDto returnGoodsPay);

        bool FinancialVerifyPass(List<string> rmaNoList);
        bool FinancialVerifyNoPass(List<string> rmaNoList);

        //退回付款确认 查询 退货单列表
        IList<RMADto> GetRmaByFilter(PackageReceiveDto packageReceive);
    }
}