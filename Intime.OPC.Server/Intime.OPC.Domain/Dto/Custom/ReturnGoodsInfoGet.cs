using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;
using Intime.OPC.Domain.Dto.Financial;

namespace Intime.OPC.Domain.Dto.Custom
{
    /// <summary>
    /// 客户服务-客服退货查询-退货信息
    /// </summary>
    public class ReturnGoodsInfoRequest : BaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string RmaNo { get; set; }

        public string OrderNo { get; set; }

        public string SaleOrderNo { get; set; }

        public int? RmaStatus { get; set; }


        public string PayType { get; set; }

        public int? StoreID { get; set; }

        public void FormatDate()
        {
            StartDate = StartDate.Date;
            EndDate = EndDate.Date.AddDays(1);
        }
    }
}