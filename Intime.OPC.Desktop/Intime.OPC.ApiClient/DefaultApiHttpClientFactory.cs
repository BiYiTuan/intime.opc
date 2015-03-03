using System;
using System.Net.Http;
using Intime.OPC.ApiClient.MessageHandler;

namespace Intime.OPC.ApiClient
{
    public class DefaultApiHttpClientFactory
    {
        private readonly Uri _baseAddress;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly HttpMessageHandler _handler;

        public DefaultApiHttpClientFactory(Uri baseAddress, string consumerKey, string consumerSecret)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;

            _baseAddress = baseAddress;
            _handler = new SignatureMessageHandler(consumerKey, consumerSecret);
        }

        public ApiHttpClient Create()
        {
            return new ApiHttpClient(_baseAddress, _consumerKey, _consumerSecret, _handler);
        }
    }
}