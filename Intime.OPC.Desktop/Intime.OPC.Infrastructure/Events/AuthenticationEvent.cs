using Microsoft.Practices.Prism.PubSubEvents;
using Intime.OPC.Domain;

namespace Intime.OPC.Infrastructure.Events
{
    public class AuthenticationEvent : PubSubEvent<ILoginModel>
    {

    }
}
