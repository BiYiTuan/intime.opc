using Intime.O2O.ApiClient.Response;

namespace Intime.O2O.ApiClient.Request
{
    public class GetProductPropertiesRawRequest : Request<GetPagedEntityRequestData, GetProductPropertiesRawResponse>
    {
        public override string GetResourceUri()
        {
            return "production/queryPropertyList";
        }
    }
}
