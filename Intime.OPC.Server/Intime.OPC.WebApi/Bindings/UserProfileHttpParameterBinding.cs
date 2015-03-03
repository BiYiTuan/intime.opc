using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace Intime.OPC.WebApi.Bindings
{
    public class UserProfileHttpParameterBinding : HttpParameterBinding
    {
        public UserProfileHttpParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (actionContext != null &&
                actionContext.Request.Properties.ContainsKey(AccessTokenConst.UserProfilePropertiesName))
            {
                if (actionContext.ActionArguments != null)
                    actionContext.ActionArguments[Descriptor.ParameterName] =
                        actionContext.Request.Properties[AccessTokenConst.UserProfilePropertiesName];
            }
            else
            {
                if (actionContext != null && actionContext.ActionArguments != null)
                    actionContext.ActionArguments[Descriptor.ParameterName] = null;
            }

            return Task.FromResult<object>(null);
        }
    }
}