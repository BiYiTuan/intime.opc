
namespace Intime.O2O.ApiClient.Request
{
    public class OrderNotifyRequest:Request<dynamic, Response<dynamic>>
    {
        public override string GetResourceUri()
        {
            return "order/addorder";
        }
    }
}
