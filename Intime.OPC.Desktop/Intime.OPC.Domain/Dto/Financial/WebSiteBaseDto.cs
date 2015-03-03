using System;
using Intime.OPC.Domain.Models;

namespace OPCAPP.Domain.Dto.Financial
{
    public class WebSiteBaseDto : Model
    {
        public WebSiteBaseDto()
        {
            BuyDate = DateTime.Now;
        }

        #region 共同的字段

        /// <summary>
        ///     门店名称
        /// </summary>
        public string StoreName { get; set; } //1 2

        /// <summary>
        ///     订单号
        /// </summary>
        public string OrderNo { get; set; } //1 2

        /// <summary>
        ///     订单渠道号
        /// </summary>
        public string OrderChannelNo { get; set; } //1 2


        /// <summary>
        ///     支付方式
        /// </summary>
        public string PaymentMethodName { get; set; } //1 2

        /// <summary>
        ///     订单来源
        /// </summary>
        public string OrderSouce { get; set; } //1 2


        /// <summary>
        ///     购买时间
        /// </summary>
        public DateTime BuyDate { get; set; } //1 2


        /// <summary>
        ///     品牌
        /// </summary>
        /// <value>The Brand.</value>
        public string Brand { get; set; } //1 2

        /// <summary>
        ///     款号
        /// </summary>
        /// <value>The style no.</value>
        public string StyleNo { get; set; } //1 2

        /// <summary>
        ///     规格
        /// </summary>
        /// <value>The standard.</value>
        public string Size { get; set; } //1 2

        /// <summary>
        ///     色码
        /// </summary>
        /// <value>The color.</value>
        public string Color { get; set; } //1 2


        /// <summary>
        ///     零售价格
        /// </summary>
        /// <value>The label price.</value>
        public double LabelPrice { get; set; } //1 2

        /// <summary>
        ///     销售价格
        /// </summary>
        /// <value>The sale price.</value>
        public double SalePrice { get; set; } //1 2


        /// <summary>
        ///     专柜码
        /// </summary>
        public string SectionCode { get; set; } //1 2

        /// <summary>
        /// 销售编码
        /// </summary>
        public string SalesCode { get; set; }
        #endregion
    }
}