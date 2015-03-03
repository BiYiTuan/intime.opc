using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.PubSubEvents;
using Intime.OPC.Infrastructure.ErrorHandling;

namespace Intime.OPC.Infrastructure.Events
{
    [Export(typeof(GlobalEventAggregator))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GlobalEventAggregator : EventAggregator, IPartImportsSatisfiedNotification
    {
        [Import]
        private GlobalEventHandler _globalEventHandler;

        public void OnImportsSatisfied()
        {
            _globalEventHandler.Initialize();
        }
    }
}
