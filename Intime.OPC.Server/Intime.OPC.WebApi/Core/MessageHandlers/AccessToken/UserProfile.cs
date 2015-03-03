using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Intime.OPC.WebApi.Core.MessageHandlers.AccessToken
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    public class UserProfile
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 系统管理员,如果是系统管理员可以管理所有门店和专柜
        /// </summary>
        [DataMember]
        public bool IsSystem { get; set; }

        /// <summary>
        /// 专柜列表
        /// </summary>
        [DataMember]
        [System.Obsolete("暂时不可用")]
        public IEnumerable<int> SectionIds { get; set; }

        /// <summary>
        /// 门店列表
        /// </summary>
        [DataMember]
        public IEnumerable<int> StoreIds { get; set; }

        /// <summary>
        /// 获取角色列表
        /// </summary>
        [DataMember]
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// ORG列表
        /// </summary>
        [DataMember]
        public IEnumerable<string> OrgIds { get; set; }
    }
}
