namespace Intime.OPC.Domain.Models
{
    public class OPC_RMADetail
    {
        public int Id { get; set; }

        /// <summary>
        ///     款号
        /// </summary>
        public string StyleNumber { get; set; }

        /// <summary>
        ///     规格
        /// </summary>
        public string Standard { get; set; }

        /// <summary>
        ///     色码
        /// </summary>
        public string ColorNumber { get; set; }

        /// <summary>
        ///     退货价格
        /// </summary>
        public decimal RMAPrice { get; set; }

        /// <summary>
        ///     退货数量
        /// </summary>
        public int RMACount { get; set; }

        /// <summary>
        ///     品牌
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        ///     商品编码
        /// </summary>
        public string GoodsCode { get; set; }

        /// <summary>
        ///     专柜码
        /// </summary>
        public string SectionId { get; set; }
    }
}