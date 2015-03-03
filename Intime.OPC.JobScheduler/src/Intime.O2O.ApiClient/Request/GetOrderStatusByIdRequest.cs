using Intime.O2O.ApiClient.Response;

namespace Intime.O2O.ApiClient.Request
{

    public class GetOrderStatusByIdRequest : Request<dynamic, GetOrderStatusByIdResponse>
    {
        public override string GetResourceUri()
        {
            return "order/queryorderdetail";
        }
    }
}
