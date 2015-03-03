using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Intime.OPC.Infrastructure.Rest;

namespace Intime.OPC.Infrastructure.REST
{
    public class RestClient : IRestClient
    {
        private const string Consumerkey = "X-ConsumerKey";
        private const string TokenKey = "X-Token";

        private readonly Uri _baseAddress;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;
        private readonly string _token;
        private readonly Random _random = new Random();
        private readonly MediaTypeFormatter _mediaTypeFormatter = new JsonMediaTypeFormatter();

        public RestClient(string baseAddress, string consumerKey, string consumerSecret, string token)
        {
            _baseAddress = new Uri(baseAddress);
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _token = token;
        }

        public TData Get<TData>(string uri)
        {
            using (var client = CreateHttpClient())
            {
                return Request<TData>(uri, client.GetAsync);
            }
        }

        public TEntity Post<TEntity>(string uri, TEntity entity)
        {
            using (var client = CreateHttpClient())
            {
                return Send<TEntity>(uri, entity, client.PostAsJsonAsync);
            }
        }

        public TEntity Put<TEntity>(string uri, TEntity entity)
        {
            using (var client = CreateHttpClient())
            {
                return Send<TEntity>(uri, entity, client.PutAsJsonAsync);
            }
        }

        public TEntity Post<TEntity, TData>(string uri, TData data)
        {
            using (var client = CreateHttpClient())
            {
                return Send<TEntity, TData>(uri, data, client.PostAsJsonAsync);
            }
        }

        public TEntity Put<TEntity, TData>(string uri, TData data)
        {
            using (var client = CreateHttpClient())
            {
                return Send<TEntity, TData>(uri, data, client.PutAsJsonAsync);
            }
        }

        public void PutWithoutReturn(string uri)
        {
            using (var client = CreateHttpClient())
            {
                Send<string>(uri, string.Empty, client.PutAsJsonAsync, false);
            }
        }

        public void PutWithoutReturn<TEntity>(string uri, TEntity entity)
        {
            using (var client = CreateHttpClient())
            {
                Send<TEntity>(uri, entity, client.PutAsJsonAsync, false);
            }
        }

        public void Delete(string uri)
        {
            using (var client = CreateHttpClient())
            {
                Request<string>(uri, client.DeleteAsync);
            }
        }

        private TData Request<TData>(string uri, Func<string, Task<HttpResponseMessage>> verb)
        {
            var randomString = BuildRandomString<TData>();
            var url = string.Format("{0}{1}{2}", _baseAddress, uri, randomString);
            var response = verb(url).Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsAsync<TData>().Result;
            }

            string errorMessage = ParseErrorMessage(response);
            throw new RestException(response.StatusCode, errorMessage);
        }

        private TEntity Send<TEntity>(string uri, TEntity entity, Func<string, TEntity, Task<HttpResponseMessage>> verb, bool withReturn = true)
        {
            return Send<TEntity, TEntity>(uri, entity, verb, withReturn);
        }

        private TEntity Send<TEntity, TData>(string uri, TData data, Func<string, TData, Task<HttpResponseMessage>> verb, bool withReturn = true)
        {
            var response = verb(string.Format("{0}{1}", _baseAddress, uri), data).Result;
            if (response.IsSuccessStatusCode)
            {

                return withReturn ? response.Content.ReadAsAsync<TEntity>().Result : default(TEntity);
            }

            string errorMessage = ParseErrorMessage(response);
            throw new RestException(response.StatusCode, errorMessage);
        }

        private static string ParseErrorMessage(HttpResponseMessage response)
        {
            string errorMessage = string.Empty;
            try
            {
                errorMessage = response.Content.ReadAsAsync<RestErrorMessage>().Result.Message;
            }
            catch
            {
                errorMessage = response.Content.ReadAsStringAsync().Result;
            }

            string errorDetailMessage = string.IsNullOrEmpty(errorMessage) ? string.Empty: string.Format("\n\n错误信息:{0}。", errorMessage);

            return string.Format("调用后台API时发生错误。{0}", errorDetailMessage);
        }

        private HttpClient CreateHttpClient()
        {
            var handler = new SignatureHandler(_consumerKey, _consumerSecret);
            var client = new HttpClient(handler) { BaseAddress = _baseAddress };

            SetHeaderValue(client, Consumerkey, _consumerKey);
            SetHeaderValue(client, TokenKey, _token);

            return client;
        }

        private void SetHeaderValue(HttpClient client, string name, string value)
        {
            lock (client.DefaultRequestHeaders)
            {
                client.DefaultRequestHeaders.Remove(name);
                client.DefaultRequestHeaders.Add(name, value);
            }
        }

        private static string BuildRandomString<TData>()
        {
            var randomString = string.Empty;
            var dataType = typeof(TData);
            if (dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(PagedResult<>))
            {
                randomString = string.Format("&timestamp={0}", DateTime.Now.ToString("MMddyyyyHHmmssfff"));
            }
            else
            {
                randomString = string.Format("/?timestamp={0}", DateTime.Now.ToString("MMddyyyyHHmmssfff"));
            }
            return randomString;
        }
    }
}
