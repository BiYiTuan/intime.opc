using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using System;

namespace Intime.OPC.Domain.Exception
{
    public class RmaException : OpcApiException
    {
        public RmaException(string msg)
            : base(msg)
        {
        }
    }

    public class NotExistsRmaException : RmaException
    {
        public NotExistsRmaException(string rmano)
            : base(String.Format("Rma({0})未找到", rmano))
        {
        }
    }

    /// <summary>
    /// ordertype 异常
    /// </summary>
    public class NotOrderProductTypeRmaException : RmaException
    {
        public NotOrderProductTypeRmaException(string rmano, OrderProductType orderProductType)
            : base(String.Format("Rma({0})不是{1}的订单", rmano, orderProductType.GetDescription()))
        {
        }
    }

    /// <summary>
    /// 状态异常
    /// </summary>
    public class NotStatusRmaException : RmaException
    {
        public NotStatusRmaException(string rmano, EnumRMAStatus currentStatus, EnumRMAStatus destStatus)
            : base(String.Format("Rma({0})当前状态是{1}({2}),目标状态是{3}({4})", rmano, currentStatus.GetDescription(), currentStatus, destStatus.GetDescription(), destStatus.AsId()))
        {
        }
    }


    /// <summary>
    /// 收银状态异常
    /// </summary>
    public class NotCashStatusRmaException : RmaException
    {
        public NotCashStatusRmaException(string rmano, EnumRMACashStatus currentStatus, EnumRMACashStatus destStatus)
            : base(String.Format("Rma({0})当前状态是{1}({2}),目标状态是{3}({4})", rmano, currentStatus.GetDescription(), currentStatus, destStatus.GetDescription(), destStatus.AsId()))
        {
        }
    }
}