using Intime.O2O.ApiClient.Response;

namespace Intime.O2O.ApiClient.Request
{
    public class GetBrandByIdRequest : Request<GetBrandByIdRequestData, GetBrandByIdResponse>
    {
        public override string GetResourceUri()
        {
            return "brand/queryBrandById";
        }
    }
}
