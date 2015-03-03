using Intime.O2O.ApiClient.Response;

namespace Intime.O2O.ApiClient.Request
{
    public class GetProductPropertiesRequest : Request<GetPagedEntityRequestData, GetProductPropertiesResponse>
    {
        public override string GetResourceUri()
        {
            return "production/queryProperty";
        }
    }
}
