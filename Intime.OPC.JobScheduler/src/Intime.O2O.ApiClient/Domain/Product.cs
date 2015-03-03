
using System;
using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Domain
{
    /// <summary>
    /// 商品信息
    /// </summary>
    [DataContract]
    public class Product
    {
        /// <summary>
        /// 在信息部系统主键
        /// </summary>
        [DataMember(Name = "ID")]
        public string Id { get; set; }


        /// <summary>
        /// 商品图片写入时间
        /// </summary>
        [DataMember(Name = "WRITE_TIME")]
        public string WriteTime { get; set; }

        /// <summary>
        /// 商品编码
        /// </summary>
        [DataMember(Name = "PRODUCTCODE")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [DataMember(Name = "PRODUCTNAME")]
        public string ProductName { get; set; }

        /// <summary>
        /// 专柜Id
        /// </summary>
        [DataMember(Name = "COUNTERID")]
        public string SectionId { get; set; }

        /// <summary>
        /// 品牌Id
        /// </summary>
        [DataMember(Name = "BRANDID")]
        public int? BrandId { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        [DataMember(Name = "STORENO")]
        public string StoreNo { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        [DataMember(Name = "SUPPLIERID")]
        public int? SupplierId { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        [DataMember(Name = "BRANDNAME")]
        public string BrandName { get; set; }

        /// <summary>
        /// 颜色id
        /// </summary>
        [DataMember(Name = "COLORID")]
        public string ColorId { get; set; }

        /// <summary>
        /// 商品颜色
        /// </summary>
        [DataMember(Name = "COLOR")]
        public string Color { get; set; }

        /// <summary>
        /// 尺寸id
        /// </summary>
        [DataMember(Name = "SIZEID")]
        public string SizeId { get; set; }

        /// <summary>
        /// 商品尺码名称
        /// </summary>
        [DataMember(Name = "SIZENAME")]
        public string Size { get; set; }

        /// <summary>
        /// 尺寸id
        /// </summary>
        [DataMember(Name = "BRAND_SIZECODE")]
        public string BrandSizeCode { get; set; }

        /// <summary>
        /// 商品尺码名称
        /// </summary>
        [DataMember(Name = "BRAND_SIZENAME")]
        public string BrandSizeName { get; set; }

        /// <summary>
        /// 库存量
        /// </summary>
        [DataMember(Name = "STOCK")]
        public decimal Stock { get; set; }

        /// <summary>
        /// 标签价格
        /// </summary>
        [DataMember(Name = "LABELPRICE")]
        public decimal LabelPrice { get; set; }

        /// <summary>
        /// 当前售价
        /// </summary>
        [DataMember(Name = "CURRENTPRICE")]
        public decimal CurrentPrice { get; set; }

        /// <summary>
        /// MIS销售码
        /// </summary>
        [DataMember(Name = "POSCODE")]
        public int? PosCode { get; set; }

        /// <summary>
        /// 内部唯一码
        /// </summary>
        [DataMember(Name = "PRODUCTID")]
        public Int64? ProductId { get; set; }

        /// <summary>
        /// 一级工业分类Id
        /// </summary>
        [DataMember(Name = "CATEGORYID1")]
        public string CategoryId1 { get; set; }

        /// <summary>
        /// 二级工业分类Id
        /// </summary>
        [DataMember(Name = "CATEGORYID2")]
        public string CategoryId2 { get; set; }

        /// <summary>
        /// 三级工业分类Id
        /// </summary>
        [DataMember(Name = "CATEGORYID3")]
        public string CategoryId3 { get; set; }

        /// <summary>
        /// 四级工业分类Id
        /// </summary>
        [DataMember(Name = "CATEGORYID4")]
        public string CategoryId4 { get; set; }

        /// <summary>
        /// 一级工业分类名称
        /// </summary>
        [DataMember(Name = "CATEGORY1")]
        public string Category1 { get; set; }

        /// <summary>
        /// 二级工业分类名称
        /// </summary>
        [DataMember(Name = "CATEGORY2")]
        public string Category2 { get; set; }

        /// <summary>
        /// 三级工业分类名称
        /// </summary>
        [DataMember(Name = "CATEGORY3")]
        public string Category3 { get; set; }

        /// <summary>
        /// 四级工业分类名称
        /// </summary>
        [DataMember(Name = "CATEGORY4")]
        public string Category4 { get; set; }
    }
}
