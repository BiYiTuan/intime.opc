
namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 销售单项信息
    /// </summary>
    public class SalesProductDetailDto
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 品牌编号
        /// </summary>
        public string BrandId { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 商品款号
        /// </summary>
        public string StyleNo { get; set; }

        /// <summary>
        /// 商品颜色编号
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// 商品颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 商品尺码编号
        /// </summary>
        public string SizeId { get; set; }

        /// <summary>
        /// 商品尺码
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 吊牌价格
        /// </summary>
        public decimal LabelPrice { get; set; }

        /// <summary>
        /// 商品销售价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
