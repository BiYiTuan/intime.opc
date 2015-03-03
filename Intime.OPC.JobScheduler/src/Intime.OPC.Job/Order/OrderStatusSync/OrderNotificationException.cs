using System;

namespace Intime.OPC.Job.Order.OrderStatusSync
{
    public class OrderNotificationException : Exception
    {
        public OrderNotificationException(string message)
            : base(message)
        {
        }

        public OrderNotificationException(string format, params object[] messages)
            : base(string.Format(format, messages))
        {
        }
    }
}