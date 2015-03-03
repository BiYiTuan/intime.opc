using Intime.O2O.ApiClient.Response;

namespace Intime.O2O.ApiClient.Request
{
    /// <summary>
    /// 根据Id获取专柜请求
    /// </summary>
    public class GetSectionByIdRequest : Request<GetSectionByIdRequestData, GetSectionByIdResponse>
    {
        public override string GetResourceUri()
        {
            return "counter/queryCounterById";
        }
    }
}
