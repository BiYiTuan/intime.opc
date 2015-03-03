using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Intime.OPC.WebApi.Core.MessageHandlers.Signature
{
    /// <summary>
    ///     签名处理消息类，
    ///     说明 ： 针对发送数据和URL进行统一的签名处理
    /// </summary>
    public class SignatureMessageHandler : DelegatingHandler
    {
        private IAppSecurityManager _appSecurityManager = new AppSecurityManager();

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            /**======================================================================
             *  签名开启检查
            ========================================================================--*/

            if (!_appSecurityManager.Enabled)
            {
                return base.SendAsync(request, cancellationToken);
            }

            /**======================================================================
             *  签名规则说明
             *  
             *      正对签名使用{url}+{postdata}+{consumerKey}，统一转化为大写进行MD5计算
             *      
             ========================================================================--*/

            if (!request.Headers.Contains(HeadConfig.Sign))
            {
                return CreateErrorResponse(request, "签名为空");
            }

            if (!request.Headers.Contains(HeadConfig.Consumerkey))
            {
                return CreateErrorResponse(request, "ConsumeyKey为空");
            }

            string sign = request.Headers.GetValues(HeadConfig.Sign).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(sign))
            {
                return CreateErrorResponse(request, "签名值为空");
            }

            string consumerKey = request.Headers.GetValues(HeadConfig.Consumerkey).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(consumerKey))
            {
                return CreateErrorResponse(request, "ConsumerKey不能为空");
            }

            string secretKey = _appSecurityManager.GetSecretKey(consumerKey);
            if (string.IsNullOrWhiteSpace(secretKey))
            {
                return CreateErrorResponse(request, string.Format("Consumerkey找不到相关配置"));
            }

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
            buffer.Append(consumerKey);

            // 添加secretKey
            buffer.Append(secretKey);

            /*======================================================================
            *  比较签名
            ========================================================================--*/

            string signStr = GetMd5Hash(buffer.ToString().ToUpper());

            return String.Compare(sign, signStr, StringComparison.OrdinalIgnoreCase) != 0
                ? CreateErrorResponse(request, "签名不正确")
                : base.SendAsync(request, cancellationToken);
        }

        public void SetAppSecurityManager(IAppSecurityManager appSecurityManager)
        {
            _appSecurityManager = appSecurityManager;
        }

        #region Helper

        private static Task<HttpResponseMessage> CreateErrorResponse(HttpRequestMessage request, string message)
        {
            HttpResponseMessage errorResponse = request.CreateErrorResponse(HttpStatusCode.BadRequest, message);
            return Task.FromResult(errorResponse);
        }

        private static string GetMd5Hash(string content)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(content));

            var sBuilder = new StringBuilder();
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        #endregion
    }
}