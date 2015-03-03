using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Dto.Custom
{
    /// <summary>
    /// 导购退货收货查询
    /// </summary>
    public class ShoppingGuideRequest : BaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string OrderNo { get; set; }
    }
}