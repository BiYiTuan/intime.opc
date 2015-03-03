using System;
using System.Net.Http;
using Intime.OPC.ApiClient.Annotations;
using Intime.OPC.ApiClient.Config;

namespace Intime.OPC.ApiClient
{
    public class ApiHttpClient : HttpClient
    {
        [UsedImplicitly] private readonly string _consumerSecret;

        public ApiHttpClient(Uri baseAddress, string consumerKey, string consumerSecret, HttpMessageHandler handler)
            : base(handler)
        {
            _consumerSecret = consumerSecret;

            // 设置ConsumerKey
            SetHeaderValue(HeadConfig.Consumerkey, consumerKey);

            BaseAddress = baseAddress;
        }

        public void SetToken(string token)
        {
            SetHeaderValue(HeadConfig.Token, token);
        }

        private void SetHeaderValue(string name, string value)
        {
            lock (DefaultRequestHeaders)
            {
                DefaultRequestHeaders.Remove(name);
                DefaultRequestHeaders.Add(name, value);
            }
        }
    }
}