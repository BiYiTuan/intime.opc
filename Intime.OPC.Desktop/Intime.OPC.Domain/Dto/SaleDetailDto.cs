namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    ///     销售单详情
    /// </summary>
    public class SaleDetailDto
    {
        public int Id { get; set; }

        /// <summary>
        ///     商品编码
        /// </summary>
        /// <value>The product no.</value>
        public string ProductNo { get; set; }

        /// <summary>
        ///     销售单号
        /// </summary>
        /// <value>The sale order no.</value>
        public string SaleOrderNo { get; set; }

        /// <summary>
        ///     款号
        /// </summary>
        /// <value>The style no.</value>
        public string StyleNo { get; set; }

        /// <summary>
        ///     尺寸
        /// </summary>
        /// <value>The standard.</value>
        public string Size { get; set; }

        /// <summary>
        ///     色码
        /// </summary>
        /// <value>The color.</value>
        public string Color { get; set; }

        /// <summary>
        ///     品牌
        /// </summary>
        /// <value>The Brand.</value>
        public string Brand { get; set; }

        /// <summary>
        ///     销售价格
        /// </summary>
        /// <value>The price.</value>
        public double SellPrice { get; set; }

        /// <summary>
        ///     原价
        /// </summary>
        /// <value>The price.</value>
        public double Price { get; set; }

        /// <summary>
        ///     促销价格
        /// </summary>
        /// <value>The sale price.</value>
        public double SalePrice { get; set; }

        /// <summary>
        ///     销售数量
        /// </summary>
        /// <value>The sale count.</value>
        public int SellCount { get; set; }

        /// <summary>
        ///     退货数量
        /// </summary>
        /// <value>The return count.</value>
        public int ReturnCount { get; set; }

        /// <summary>
        ///     零售价格
        /// </summary>
        /// <value>The label price.</value>
        public double LabelPrice { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }
    }
}