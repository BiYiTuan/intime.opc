using System.Web.Http;
using System.Web.Http.Controllers;

namespace Intime.OPC.WebApi.Bindings
{
    public class UserIdAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter.ParameterType == typeof(int) || parameter.ParameterType == typeof(int?))
            {
                return new UserIdHttpParameterBinding(parameter);
            }

            return null;
        }
    }
}