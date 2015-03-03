using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Intime.OPC.WebApi.Core.MessageHandlers.AccessToken;

namespace Intime.OPC.WebApi.Bindings
{
    public class UserIdHttpParameterBinding : HttpParameterBinding
    {
        public UserIdHttpParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            if (actionContext != null &&
                actionContext.Request.Properties.ContainsKey(AccessTokenConst.UseridPropertiesName))
            {
                if (actionContext.ActionArguments != null)
                    actionContext.ActionArguments[Descriptor.ParameterName] =
                        actionContext.Request.Properties[AccessTokenConst.UseridPropertiesName];
            }
            else
            {
                if (actionContext != null)
                    if (actionContext.ActionArguments != null)
                        actionContext.ActionArguments[Descriptor.ParameterName] = 0;
            }

            return Task.FromResult<object>(null);
        }
    }
}