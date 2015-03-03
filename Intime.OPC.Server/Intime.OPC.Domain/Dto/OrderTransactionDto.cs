using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 订单支付信息
    /// </summary>
    public class OrderTransactionDto
    {
        public string TransNo { get; set; }

        public string PaymentCode { get; set; }

        public string PaymentName { get; set; }

        public decimal Amount { get; set; }
    }
}
