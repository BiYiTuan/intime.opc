using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Domain.Dto.Financial
{
    public class ReturnGoodsStatDto : WebSiteBaseDto
    {
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

        /// <summary>
        ///     退货单状态
        /// </summary>
        public string RmaStatusName { get; set; }

        public decimal RmaAmount { get; set; }
    }

    public class ReturnGoodsStatListDto : List<ReturnGoodsStatDto>
    {
        /// <summary>
        /// 退货总数量
        /// </summary>
        /// <value>The total sale count.</value>
        public int TotalReturnGoodsCount { get; set; }

        /// <summary>
        /// 退货总金额
        /// </summary>
        /// <value>The total sale total price.</value>
        public decimal TotalRmaPrice { get; set; }

        /// <summary>
        /// 运费总计
        /// </summary>
        /// <value>The total sale total price.</value>
        public decimal TotalShippingFee { get; set; }

         //[System.Obsolete("暂时过期，这段写的很巧")]
        public void Stat()
        {
            TotalReturnGoodsCount = this.Sum(t => t.ReturnGoodsCount);
            TotalRmaPrice = this.Sum(t => t.RmaPrice*t.ReturnGoodsCount);
            TotalShippingFee = this.Where(t => t.OrderTransFee.HasValue).Sum(t => t.OrderTransFee.Value);
        }
    }

    public class PagedReturnGoodsStatListDto : ReturnGoodsStatListDto
    {
        public PagedReturnGoodsStatListDto(PagerRequest pagerRequest, int totalCount, IEnumerable<ReturnGoodsStatDto> datas)
        {
            this.PageIndex = pagerRequest.PageIndex;
            this.PageSize = pagerRequest.PageSize;
            this.TotalCount = totalCount;
            this.AddRange(datas);
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}