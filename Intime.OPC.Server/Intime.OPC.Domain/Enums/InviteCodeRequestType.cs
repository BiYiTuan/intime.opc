using System;
using System.ComponentModel;

namespace Intime.OPC.Domain.Enums
{
    public enum InviteCodeRequestType
    {
        [Description("基本")]
        Basic = 1,
        [Description("导购")]
        Daogou = 2
    }


    [Flags]
    public enum UserOperatorRight
    {
        [Description("银泰卡")]
        GiftCard = 1,
        [Description("系统商品")]
        SystemProduct = 2,
        [Description("自拍商品")]
        SelfProduct = 4
    }
}