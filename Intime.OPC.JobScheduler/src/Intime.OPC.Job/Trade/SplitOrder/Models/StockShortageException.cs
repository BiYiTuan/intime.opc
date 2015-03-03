using System;

namespace Intime.OPC.Job.Trade.SplitOrder.Models
{
    /// <summary>
    /// 库存短缺异常
    /// </summary>
    public class StockShortageException : Exception
    {
        public StockShortageException(String message)
            : base(message)
        {
        }
    }
}
