using System;
using System.Threading;
using Intime.OPC.Infrastructure.Events;

namespace Intime.OPC.Infrastructure.ErrorHandling
{
    public class AsyncSynchronizationContext : SynchronizationContext
    {
        public override void Send(SendOrPostCallback d, object state)
        {
            try
            {
                d(state);
            }
            catch (Exception ex)
            {
                PublishExceptionEvent(ex);
            }
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            try
            {
                d(state);
            }
            catch (Exception ex)
            {
                PublishExceptionEvent(ex);
            }
        }

        private void PublishExceptionEvent(Exception ex)
        {
            var eventAggregator = AppEx.Container.GetInstance<GlobalEventAggregator>();
            var raiseExceptionEvent = eventAggregator.GetEvent<RaiseExceptionEvent>();
            raiseExceptionEvent.Publish(new ExceptionTriad { Exception = ex });
        }
    }
}
