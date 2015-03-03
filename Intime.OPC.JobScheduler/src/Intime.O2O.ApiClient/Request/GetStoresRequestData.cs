using System.Runtime.Serialization;
using Intime.O2O.ApiClient.Response;
using System;

namespace Intime.O2O.ApiClient.Request
{
    /// <summary>
    /// 获取所有的门店请求数据
    /// </summary>
    [DataContract]
    public class GetStoresRequestData
    {
        [DataMember(Name = "page")]
        public int PageIndex { get; set; }

        [DataMember(Name = "size")]
        public int PageSize { get; set; }
    }
}
