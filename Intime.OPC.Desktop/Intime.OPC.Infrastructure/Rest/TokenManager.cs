using Intime.OPC.Domain;
using Intime.OPC.Infrastructure.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Rest
{
    [Export(typeof(TokenManager))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TokenManager
    {
        private GlobalEventAggregator _eventAggregator;

        [ImportingConstructor]
        public TokenManager(GlobalEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            SubscribeAuthenticationEvent();
        }

        public string Token { get; private set; }

        private void SubscribeAuthenticationEvent()
        {
            var authenticationEvent = _eventAggregator.GetEvent<AuthenticationEvent>();
            authenticationEvent.Subscribe(OnAuthenticated, ThreadOption.BackgroundThread);
        }

        private void OnAuthenticated(ILoginModel loginModel)
        {
            Token = loginModel == null ? null: loginModel.Token;
        }
    }
}
