using Intime.OPC.Domain.Customer;
using Intime.OPC.Infrastructure.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.DataService.Interface.RMA
{
    /// <summary>
    /// 迷你银退货
    /// </summary>
    public interface IMiniIntimeReturnService : IService<RMADto>
    {
        /// <summary>
        /// 补录收银流水号
        /// </summary>
        /// <param name="rmaDto"></param>
        /// <param name="paymentNumber"></param>
        void ReceivePayment(RMADto rmaDto, string paymentNumber);

        /// <summary>
        /// 退货收货确认
        /// </summary>
        void ConsignReturnGoods(RMADto rmaDto);
    }
}
