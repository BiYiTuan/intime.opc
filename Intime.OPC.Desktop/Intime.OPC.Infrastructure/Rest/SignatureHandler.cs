using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.REST
{
    /// <summary>
    ///     签名处理消息
    /// </summary>
    public class SignatureHandler : DelegatingHandler
    {
        private const string Signature = "X-Sign";
        private const string Consumerkey = "X-ConsumerKey";
        private const string TokenKey = "X-Token";

        private readonly string _consumerKey;
        private readonly string _consumerSecret;

        public SignatureHandler(string consumerKey, string consumerSecret)
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

            string sign = ComputeHash(buffer.ToString().ToUpper());

            // 添加Sign的X-Sign头
            if (request.Headers.Contains(Signature))
            {
                throw new ArgumentException(string.Format("Http head: {0}不为空", Signature));
            }

            request.Headers.Add(Signature, sign);

            return base.SendAsync(request, cancellationToken);
        }

        private string ComputeHash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}