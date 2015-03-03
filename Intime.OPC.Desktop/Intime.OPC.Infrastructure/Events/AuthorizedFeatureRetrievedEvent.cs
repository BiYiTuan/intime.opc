using System.Collections.Generic;
using Intime.OPC.Domain;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Intime.OPC.Infrastructure.Events
{
    public class AuthorizedFeatureRetrievedEvent : PubSubEvent<IEnumerable<MenuGroup>>
    {
    }
}
