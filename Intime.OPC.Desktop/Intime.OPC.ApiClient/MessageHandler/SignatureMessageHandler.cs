using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Intime.OPC.ApiClient.Config;
using Intime.OPC.ApiClient.Utils;

namespace Intime.OPC.ApiClient.MessageHandler
{
    /// <summary>
    ///     签名处理消息
    /// </summary>
    public class SignatureMessageHandler : DelegatingHandler
    {
        private readonly string _consumerKey;
        private readonly string _consumerSecret;

        public SignatureMessageHandler(string consumerKey, string consumerSecret)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            /**======================================================================
           *  计算签名
           ========================================================================--*/

            var buffer = new StringBuilder();
            // 添加Method
            buffer.Append(request.Method);

            // 添加请求地址
            buffer.Append(request.RequestUri.PathAndQuery);

            // 添加PostData
            if (request.Content != null && !request.Content.IsMimeMultipartContent())
            {
                string postData = request.Content.ReadAsStringAsync().Result;
                buffer.Append(postData);
            }

            // 添加ConsumerKe
            buffer.Append(_consumerKey);

            // 添加consumerSecret
            buffer.Append(_consumerSecret);

            /*======================================================================
            *  比较签名
            ========================================================================--*/

            string sign = SecurityUtils.GetMd5Hash(buffer.ToString().ToUpper());

            // 添加Sign的X-Sign头
            if (request.Headers.Contains(HeadConfig.Sign))
            {
                throw new ArgumentException(string.Format("Http头:{0}不为空", HeadConfig.Sign));
            }

            request.Headers.Add(HeadConfig.Sign, sign);

            return base.SendAsync(request, cancellationToken);
        }
    }
}