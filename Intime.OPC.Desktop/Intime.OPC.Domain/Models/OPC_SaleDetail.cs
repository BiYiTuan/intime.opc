using System;
using System.Globalization;

using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Domain.Models
{
    [Uri("salesorderdetail")]
    public class OPC_SaleDetail : Model
    {
        /// <summary>
        /// ÉÌÆ·±àÂë
        /// </summary>
        public string ProductNo { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// ÏúÊÛµ¥ºÅ
        /// </summary>
        public string SaleOrderNo { get; set; }

        /// <summary>
        /// ¿îºÅ
        /// </summary>
        public string StyleNo { get; set; }

        /// <summary>
        /// ³ß´ç
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// É«Âë
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Æ·ÅÆ
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// ÏúÊÛ¼Û¸ñ
        /// </summary>
        public double SellPrice { get; set; }

        /// <summary>
        /// Ô­¼Û
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// ´ÙÏú¼Û¸ñ
        /// </summary>
        public double SalePrice { get; set; }

        /// <summary>
        /// ÏúÊÛÊýÁ¿
        /// </summary>
        public int SellCount { get; set; }

        /// <summary>
        /// ÍË»õÊýÁ¿
        /// </summary>
        public int ReturnCount { get; set; }

        /// <summary>
        /// µõÅÆ¼Û¸ñ
        /// </summary>
        public double LabelPrice { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        public double Discount {
            get
            {
                return Math.Round((SellPrice / LabelPrice), 2, MidpointRounding.AwayFromZero) * 10;
            }
        }

        /// <summary>
        /// 折扣描述
        /// </summary>
        public string DiscountDescription
        {
            get
            {
                return Discount.Equals(10.0) ? "无" : Discount.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string SectionCode { get; set; }

        /// <summary>
        /// ×Ü¼Û
        /// </summary>
        public double SumPrice { get; set; }

        public OrderItem OrderItems { get; set; }

        /// <summary>
        /// 商品图片URL
        /// </summary>
        public string ProductImageUrl { get; set; }
    }
}