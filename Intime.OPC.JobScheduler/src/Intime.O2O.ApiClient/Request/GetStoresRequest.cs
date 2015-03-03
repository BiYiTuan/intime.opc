using Intime.O2O.ApiClient.Response;
using System;

namespace Intime.O2O.ApiClient.Request
{
    /// <summary>
    /// 获取所有的门店
    /// </summary>
    public class GetStoresRequest : Request<GetStoresRequestData, GetStoresResponse>
    {
        public override string GetResourceUri()
        {
            return "stores/queryStores";
        }
    }
}
