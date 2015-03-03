
using System;
using System.Runtime.Serialization;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO
{
    /// <summary>
    /// 商品图片
    /// </summary>
    [DataContract]
    public class ProductImageDto
    {
        /// <summary>
        /// 图片序号
        /// </summary>
        [DataMember(Name = "SEQNO")]
        public int SeqNo { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [DataMember(Name = "PRODUCTID")]
        public string ProductId { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [DataMember(Name = "URL")]
        public string Url { get; set; }

        /// <summary>
        /// 颜色Id
        /// </summary>
        [DataMember(Name = "COLORID")]
        public string ColorId { get; set; }
        /// <summary>
        /// 在信息部系统主键
        /// </summary>
        [DataMember(Name = "ID")]
        public string Id { get; set; }


        /// <summary>
        /// 商品图片写入时间
        /// </summary>

        [DataMember(Name = "WRITE_TIME")]
        public System.DateTime WriteTime {
            get; set;
        }

        public string GroupByKey
        {
            get
            {
                return string.Format("{0}-{1}-{2}", this.ProductId, this.ColorId, this.SeqNo);
            }
        }
    }
}
