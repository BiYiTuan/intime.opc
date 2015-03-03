
using System.Runtime.Serialization;

namespace Intime.OPC.Job.Product.ProductSync.Supports.Intime.Repository.DTO
{
    /// <summary>
    /// 门店信息
    /// </summary>
    public class StoreDto
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        [DataMember(Name = "storeno")]
        public string StoreNo { get; set; }

        /// <summary>
        /// 地址信息
        /// </summary>
        [DataMember(Name = "address")]
        public string Address { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 门店电话
        /// </summary>
        [DataMember(Name = "tel")]
        public string Tel { get; set; }
    }
}
