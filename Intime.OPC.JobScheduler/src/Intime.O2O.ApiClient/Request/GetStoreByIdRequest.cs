using Intime.O2O.ApiClient.Response;

namespace Intime.O2O.ApiClient.Request
{
    /// <summary>
    /// 根据Id获取门店信息
    /// </summary>
    public class GetStoreByIdRequest : Request<GetStoreByIdRequestData, GetStoreByIdResponse>
    {
        public override string GetResourceUri()
        {
            return "stores/queryStoreById";
        }
    }
}
