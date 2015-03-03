
using System.Runtime.Serialization;

namespace Intime.O2O.ApiClient.Domain
{
    /// <summary>
    /// 门店信息
    /// </summary>
    [DataContract]
    public class Store
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
