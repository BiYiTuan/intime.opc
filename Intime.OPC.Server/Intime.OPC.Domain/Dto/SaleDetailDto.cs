using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 销售单详情
    /// </summary>
    [DataContract]
    public class SaleDetailDto
    {
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        /// <value>The product no.</value>
        [DataMember]
        public string ProductNo { get; set; }

        /// <summary>
        /// 销售单号
        /// </summary>
        /// <value>The sale order no.</value>
        [DataMember]
        public string SaleOrderNo { get; set; }
        /// <summary>
        /// 款号
        /// </summary>
        /// <value>The style no.</value>
        [DataMember]
        public string StyleNo { get; set; }

        /// <summary>
        /// 尺寸
        /// </summary>
        /// <value>The standard.</value>
        [DataMember]
        public string Size { get; set; }

        /// <summary>
        /// 色码
        /// </summary>
        /// <value>The color.</value>
        [DataMember]
        public string Color { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        /// <value>The Brand.</value>
        [DataMember]
        public string Brand { get; set; }

        /// <summary>
        ///  销售价格
        /// </summary>
        /// <value>The price.</value>
        [DataMember(EmitDefaultValue = false)]
        public decimal SellPrice { get; set; }

        /// <summary>
        /// 原价
        /// </summary>
        /// <value>The price.</value>
        [DataMember]
        public decimal Price { get; set; }

        /// <summary>
        /// 促销价格
        /// </summary>
        /// <value>The sale price.</value>
        [DataMember(EmitDefaultValue = false)]
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        /// <value>The sale count.</value>
        [DataMember]
        public int SellCount { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        /// <value>The return count.</value>
        [DataMember(EmitDefaultValue = false)]
        public int ReturnCount { get; set; }

        /// <summary>
        /// 吊牌价格
        /// </summary>
        /// <value>The label price.</value>
        [DataMember]
        public decimal LabelPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The remark.</value>
        [DataMember(EmitDefaultValue = false)]
        public string Remark { get; set; }

        /// <summary>
        /// 专柜Id
        /// </summary>
        [DataMember]
        public string SectionCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }

        /// <summary>
        /// 商品图片Url
        /// </summary>
        [DataMember]
        public IList<string> ProductImageUrls { get; set; }
    }
}
