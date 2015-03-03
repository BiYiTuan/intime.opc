using System;
using Intime.OPC.Domain.Attributes;

namespace OPCAPP.Domain.Dto.Financial
{
    [Uri("statistics/rmadetailsreport")]
    public class WebSiteReturnGoodsStatisticsDto : WebSiteBaseDto
    {
        public WebSiteReturnGoodsStatisticsDto()
        {
            RmaDate = DateTime.Now;
            ApplyRmaDate = DateTime.Now;
        }

        /// <summary>
        ///     退货申请时间
        /// </summary>
        public DateTime ApplyRmaDate { get; set; } //1

        /// <summary>
        ///     退货时间
        /// </summary>
        public DateTime RmaDate { get; set; } //1

        /// <summary>
        ///     退货数量
        /// </summary>
        public int ReturnGoodsCount { get; set; } //1

        /// <summary>
        /// 退货状态
        /// </summary>
        public string RmaStatusName { get; set; }

        /// <summary>
        ///     订单运费
        /// </summary>
        public decimal? OrderTransFee { get; set; }


        /// <summary>
        ///     退货价格
        /// </summary>
        public decimal RmaPrice { get; set; }

        /// <summary>
        ///     退货单号
        /// </summary>
        /// <value>The rma no.</value>
        public string RMANo { get; set; } //1

        public decimal RmaAmount { get; set; }
    }
}