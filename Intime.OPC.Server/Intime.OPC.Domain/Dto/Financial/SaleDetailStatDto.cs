using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto.Financial
{

    /// <summary>
    /// 网络销售明细统计
    /// </summary>
    public class SaleDetailStatDto : WebSiteBaseDto
    {

        /// <summary>
        ///     销售金额
        /// </summary>
        /// <value>The sale Total price.</value>
        public decimal SaleTotalPrice { get; set; }
        /// <summary>
        ///     销售数量
        /// </summary>
        /// <value>The sale count.</value>
        public int SellCount { get; set; }
        /// <summary>
        /// 订单运费
        /// </summary>
        public decimal? OrderTransFee { get; set; }
    }


    public class SaleDetailStatListDto : List<SaleDetailStatDto>
    {
        /// <summary>
        /// 销售总数量
        /// </summary>
        /// <value>The total sale count.</value>
        public int TotalSaleCount { get; set; }

        /// <summary>
        /// 销售总金额
        /// </summary>
        /// <value>The total sale total price.</value>
        public decimal TotalSaleTotalPrice { get; set; }

        /// <summary>
        /// 运费总计
        /// </summary>
        /// <value>The total sale total price.</value>
        public decimal TotalShippingFee { get; set; }

        public void Stat()
        {
            TotalSaleCount = this.Sum(t => t.SellCount);
            TotalSaleTotalPrice = this.Sum(t => t.SalePrice);
            TotalShippingFee = this.Where(t => t.OrderTransFee.HasValue).Sum(t => t.OrderTransFee.Value);
        }
    }

    public class PagedSaleDetailStatListDto : SaleDetailStatListDto
    {
        public PagedSaleDetailStatListDto(PagerRequest pagerRequest, int totalCount, IEnumerable<SaleDetailStatDto> datas)
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
