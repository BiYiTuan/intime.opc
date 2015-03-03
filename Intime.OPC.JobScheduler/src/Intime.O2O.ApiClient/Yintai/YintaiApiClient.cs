using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Common.Logging;

namespace Intime.O2O.ApiClient.Yintai
{
    public class YintaiApiClient
    {
        private string _url;
        private string _appKey;
        private string _appSecret;
        private string _clientId;
        private string _clientName;
        private string _currentDateTime;
        protected readonly IDictionary<string, string> requestParamDictionary;
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        public YintaiApiClient()
            : this(
                ConfigurationManager.AppSettings["intime.o2o.yintai:baseurl"], 
                ConfigurationManager.AppSettings["intime.o2o.yintai:appkey"], 
                ConfigurationManager.AppSettings["intime.o2o.yintai:appsecret"], 
                ConfigurationManager.AppSettings["intime.o2o.yintai:clientname"], 
                ConfigurationManager.AppSettings["intime.o2o.yintai:clientid"])
        {

        }

        public YintaiApiClient(string baseUrl, string appKey, string appSecret, string clientName, string clientId)
        {
            _currentDateTime = DateTime.Now.ToString("yyyymmddHHmmss");
            requestParamDictionary = new SortedDictionary<string, string>(StringComparer.Ordinal);
            _url = baseUrl;
            _clientId = clientId;
            _clientName = clientName;
            requestParamDictionary.Add("sip_http_method", "post");
            requestParamDictionary.Add("signtype", "1");
            requestParamDictionary.Add("signMethod", "1");
            requestParamDictionary.Add("Content_type", "json");
            requestParamDictionary.Add("ClientName", _clientName);
            requestParamDictionary.Add("ClientId",_clientId);
            requestParamDictionary.Add("Date", _currentDateTime);
            requestParamDictionary.Add("IsEncode","0");
            requestParamDictionary.Add("ver","1.0");
            requestParamDictionary.Add("Timereq",_currentDateTime);
            requestParamDictionary.Add("Language","Chinese");
            this._appKey = appKey;
            this._appSecret = appSecret;
        }

        public YintaiResponse Post(IDictionary<string, string> parameters, string method)
        {
            foreach (var parameter in parameters)
            {
                requestParamDictionary.Add(parameter);
            }
            requestParamDictionary.Add("method", method); 
            requestParamDictionary.Add("sign", this.Sign(parameters));
            
            using (var client = CreateHttpClient())
            {
                var content = new FormUrlEncodedContent(requestParamDictionary);
                var result = client.PostAsync("service", content).Result;
#if DEBUG
                Logger.Error("**************调用银泰网接口返回****************");
                var rst = result.Content.ReadAsStringAsync().Result;
                Logger.Error(rst);
#endif
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (result.IsSuccessStatusCode)
                {
                    return result.Content.ReadAsAsync<YintaiResponse>().Result;
                }
            }
            return null;
        }

        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient {BaseAddress = new Uri(_url)};
            client.DefaultRequestHeaders.Add("accept", "Application/Json");
            return client;
        }

        private string Sign(IDictionary<string, string> parameters)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_appSecret)))
            {
                var valueToSign = ParametersToQueryString(parameters);
                var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(valueToSign));
                return 
                    Convert.ToBase64String(hashValue);
            }
        }

        private string ParametersToQueryString(IDictionary<string, string> parameters)
        {
            var sb = new StringBuilder();
            var sortedParams = new SortedDictionary<string, string>(parameters,StringComparer.Ordinal)
            {
                {"appKey", _appKey},
                {"secrectKey", _appSecret},
                {"TimeReq", _currentDateTime}
            };
            return sortedParams.Keys.Aggregate(sb, (s, e) => s.AppendFormat("{0}={1}&", e.ToUpper(), sortedParams[e]),
                s => s.ToString().TrimEnd('&'));
        }
    }
}
