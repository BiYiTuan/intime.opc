using System.ComponentModel.Composition;
using Intime.OPC.Infrastructure.Events;
using Intime.OPC.Infrastructure.Mvvm.Utility;
using log4net;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Intime.OPC.Infrastructure.ErrorHandling
{
    [Export(typeof(GlobalEventHandler))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GlobalEventHandler
    {
        [Import]
        private GlobalEventAggregator _eventAggregator;

        private ILog _logger = LogManager.GetLogger(typeof(GlobalEventHandler));

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            var raiseExceptionEvent = _eventAggregator.GetEvent<RaiseExceptionEvent>();
            raiseExceptionEvent.Subscribe(OnExceptionRaised, ThreadOption.BackgroundThread);
        }

        private void OnExceptionRaised(ExceptionTriad exceptionTriad)
        {
            if (exceptionTriad == null) return;

            var exception = exceptionTriad.Exception;
            if (exception == null) return;
            try
            {
                _logger.Error("发生未知错误",exception);
                MvvmUtility.ShowMessageAsync(exception.Message,"错误");

                if (exceptionTriad.CompleteCallback != null) exceptionTriad.CompleteCallback();
            }
            catch { }
        }
    }
}
