using Intime.OPC.DataService.Interface.RMA;
using Intime.OPC.Domain.Customer;
using Intime.OPC.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.DataService.Impl.RMA
{
    [Export(typeof(IMiniIntimeReturnService))]
    public class MiniIntimeReturnService : ServiceBase<RMADto>, IMiniIntimeReturnService
    {
        public void ReceivePayment(RMADto rmaDto, string paymentNumber)
        {
            string uri = string.Format("{0}/{1}/cash", UriName, rmaDto.RMANo);
            Update(uri, new { CashNo = paymentNumber });
        }

        public void ConsignReturnGoods(RMADto rmaDto)
        {
            string uri = string.Format("{0}/{1}/returnofgoods", UriName, rmaDto.RMANo);
            Update(uri, new { IsConfirm = true });
        }
    }
}
