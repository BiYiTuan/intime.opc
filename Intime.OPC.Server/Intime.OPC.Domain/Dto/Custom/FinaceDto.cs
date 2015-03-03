using System;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Dto.Custom
{
    /// <summary>
    ///     退货赔偿审核 查询
    /// </summary>
    public class FinaceRequest : BaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string OrderNo { get; set; }

        public string SaleOrderNo { get; set; }
    }
}