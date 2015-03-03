using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Domain.Models
{
    [Uri("orders")]
    public class Order : Model
    {
        /// <summary>
        ///     ¶©µ¥ºÅ
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        ///     ¶©µ¥ÇþµÀºÅ
        /// </summary>
        public string OrderChannelNo { get; set; }

        /// <summary>
        ///     Ö§¸¶·½Ê½
        /// </summary>
        public string PaymentMethodName { get; set; }

        /// <summary>
        ///     ¶©µ¥À´Ô´
        /// </summary>
        public string OrderSouce { get; set; }

        /// <summary>
        ///     ¶©µ¥×´Ì¬
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     ÉÌÆ·ÊýÁ¿
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        ///     ÉÌÆ·½ð¶î
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     ¹Ë¿ÍÔË·Ñ
        /// </summary>
        public decimal CustomerFreight { get; set; }

        /// <summary>
        ///     Ó¦¸¶¿îºÏ¼Æ
        /// </summary>
        public decimal MustPayTotal { get; set; }


        /// <summary>
        ///     ¹ºÂòÊ±¼ä
        /// </summary>
        public DateTime BuyDate { get; set; }

        /// <summary>
        ///     ÊÕ»õÈËÐÕÃû
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        ///     ÊÕ»õÈËµØÖ·
        /// </summary>
        public string CustomerAddress { get; set; }

        /// <summary>
        ///     ÊÕ»õÈËµç»°
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        ///     ¹Ë¿Í±¸×¢
        /// </summary>
        public string CustomerRemark { get; set; }

        /// <summary>
        ///     ÊÇ·ñÒª·¢Æ±
        /// </summary>
        public string IfReceipt { get; set; }

        /// <summary>
        ///     ·¢Æ±Ì¨Í·
        /// </summary>
        public string ReceiptHead { get; set; }

        /// <summary>
        ///     ·¢Æ±ÄÚÈÝ
        /// </summary>
        public string ReceiptContent { get; set; }

        /// <summary>
        ///     ·¢»õ·½Ê½
        /// </summary>
        public string OutGoodsType { get; set; }

        /// <summary>
        ///     ÓÊ±à
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        ///     ·¢»õµ¥ºÅ
        /// </summary>
        public string ShippingNo { get; set; }

        /// <summary>
        ///     ¿ìµÝµ¥ºÅ
        /// </summary>
        public string ExpressNo { get; set; }

        /// <summary>
        ///     ¿ìµÝ¹«Ë¾
        /// </summary>
        public string ExpressCompany { get; set; }

        /// <summary>
        ///     ·¢»õÊ±¼ä
        /// </summary>
        public DateTime OutGoodsDate { get; set; }
    }
}