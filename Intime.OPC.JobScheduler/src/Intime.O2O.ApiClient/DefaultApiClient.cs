using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Cryptography;
using System.Text;

namespace Intime.O2O.ApiClient
{
    /// <summary>
    /// 默认接口实现
    /// </summary>
    public class DefaultApiClient : IApiClient
    {
        private readonly Uri _baseAddress;
        private readonly string _privateKey;
        private readonly string _from;
        private readonly Random _random = new Random();
        private readonly MediaTypeFormatter _mediaTypeFormatter;// = new JsonMediaTypeFormatter();
        private readonly IList<string> _errorMessages = new List<string>();

        public DefaultApiClient()
        {
            var baseAddressStr = ConfigurationManager.AppSettings["intime.o2o.api.url"] ?? string.Empty;
            if (baseAddressStr == null || string.IsNullOrWhiteSpace(baseAddressStr))
            {
                throw new ConfigurationErrorsException("not found [intime.o2o.api.url]");
            }

            _baseAddress = new Uri(baseAddressStr);

            var privateKey = ConfigurationManager.AppSettings["intime.o2o.api.key"] ?? string.Empty;
            if (privateKey == null || string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ConfigurationErrorsException("not found [intime.o2o.api.key]");
            }

            _privateKey = privateKey;

            var from = ConfigurationManager.AppSettings["intime.o2o.api.from"] ?? string.Empty;
            if (from == null || string.IsNullOrWhiteSpace(privateKey))
            {
                throw new ConfigurationErrorsException("not found [intime.o2o.api.from]");
            }

            _from = from;
            _mediaTypeFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = { NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore }
            };
        }

        public DefaultApiClient(string baseAddress, string privateKey, string from)
        {
            _privateKey = privateKey;
            _baseAddress = new Uri(baseAddress);
            _from = from;
        }

        public TResponse Post<TRequest, TResponse>(Request<TRequest, TResponse> request)
        {
            return this.Post(request, false);
        }

        public TResponse Post<TRequest, TResponse>(Request<TRequest, TResponse> request, bool enableTrace = false)
        {
            var resourceUri = request.GetResourceUri();
            using (var client = GetHttpClient())
            {
                request.From = _from;
                request.Nonce = _random.Next(0, 99);
                request.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //计算签名
                request.Sign = Sign(request);

                var result = client.PostAsJsonAsync(resourceUri, request).Result;

                if (enableTrace)
                {
                    _errorMessages.Clear();
                    _errorMessages.Add(result.ReasonPhrase);
                    _errorMessages.Add(GetDataString(request.Data));
                    _errorMessages.Add(result.StatusCode.ToString());
                }

                if (result.IsSuccessStatusCode)
                {
                    if (enableTrace)
                    {
                        _errorMessages.Add(result.Content.ReadAsStringAsync().Result);
                    }
                    return result.Content.ReadAsAsync<TResponse>().Result;
                }
            }
            return default(TResponse);
        }

        public IEnumerable<string> ErrorList()
        {
            return _errorMessages;
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient { BaseAddress = _baseAddress };
        }

        /// <summary>
        /// 计算签名
        /// </summary>
        /// <typeparam name="TRequest">请求对象</typeparam>
        /// <typeparam name="TResponse">响应对象</typeparam>
        /// <param name="request">请求实体</param>
        /// <returns>具体的签名</returns>
        private string Sign<TRequest, TResponse>(Request<TRequest, TResponse> request)
        {
            // 参数列表
            var list = new List<string>(4)
            {
                request.From,
                request.Nonce.ToString(CultureInfo.InvariantCulture),
                request.Timestamp,
                GetDataString(request.Data)
            };

            // 参数排序
            list.Sort(StringComparer.Ordinal);

            return ComputeHash(_privateKey, string.Join("", list));
        }

        private string GetDataString<T>(T data)
        {
            using (var content = new ObjectContent(typeof(T), data, _mediaTypeFormatter))
            {
                return content.ReadAsStringAsync().Result;
            }
        }

        private string ComputeHash(string privateKey, string message)
        {
            var key = Encoding.UTF8.GetBytes(privateKey);

            using (var hmac = new HMACSHA1(key))
            {
                hmac.Initialize();
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
