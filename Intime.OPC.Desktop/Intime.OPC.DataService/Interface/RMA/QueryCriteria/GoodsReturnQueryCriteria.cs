using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Infrastructure.Service;
using System;

namespace Intime.OPC.DataService.Interface.RMA
{
    //退货入收银   退货入库 打印退货单 导购退货收货查询 已完成退货查询
    public class GoodsReturnQueryCriteria : QueryCriteria
    {
        public GoodsReturnQueryCriteria()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        [UriParameter("orderno")]
        public string OrderNo { get; set; }

        [UriParameter("startdate")]
        public DateTime StartDate { get; set; }

        [UriParameter("enddate")]
        public DateTime EndDate { get; set; }

        [UriParameter("storeid")]
        public int StoreId { get; set; }

        [UriParameter("status")]
        public EnumRMAStatus Status { get; set; }

        public override string ToString()
        {
            return BuildQueryString();
        }
    }
}