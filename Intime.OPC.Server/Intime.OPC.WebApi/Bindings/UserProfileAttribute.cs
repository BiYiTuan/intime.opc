using System.Web.Http;
using System.Web.Http.Controllers;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace Intime.OPC.WebApi.Bindings
{
    public class UserProfileAttribute : ParameterBindingAttribute
    {
        public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
        {
            if (parameter.ParameterType == typeof(UserProfile))
            {
                return new UserProfileHttpParameterBinding(parameter);
            }

            return null;
        }
    }
}