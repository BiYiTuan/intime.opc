using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Intime.OPC.WebApi.Core.Security;

namespace Intime.OPC.WebApi.Core.MessageHandlers.AccessToken
{
    /// <summary>
    ///     用户身份验证消息处理
    /// </summary>
    public class AccessTokenMessageHandler : DelegatingHandler
    {
        private readonly IList<string> _excludesUrls = new List<string>();
        private IUserProfileProvider _userProfileProvider;

        public AccessTokenMessageHandler(IList<string> excludesUrls)
        {
            _excludesUrls = excludesUrls;
        }

        private bool Enabled
        {
            get
            {
                string enable = ConfigurationManager.AppSettings["AccessToken:Enabled"];

                if (enable == null)
                {
                    return true;
                }

                bool result;

                bool.TryParse(enable, out result);

                return result;
            }
        }

        public void SetUserProfileProvider(IUserProfileProvider userProfileProvider)
        {
            _userProfileProvider = userProfileProvider;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            /**======================================================================
              检查是否支持
             ========================================================================*/
            if (IsPass(request))
            {
                return base.SendAsync(request, cancellationToken);
            }

            /**======================================================================
             AccessToken处理
            ========================================================================*/

            if (!request.Headers.Contains(HeadConfig.Token))
            {
                return CreateErrorResponse(request, string.Format("HttpHeader {0}不存在", HeadConfig.Token));
            }

            string accessToken = request.Headers.GetValues(HeadConfig.Token).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return CreateErrorResponse(request, string.Format("HttpHeader {0} 的值为空", HeadConfig.Token));
            }

            AccessTokenIdentity<int> token = SecurityUtils.GetAccessToken<int>(accessToken);
            if (token == null)
            {
                return CreateErrorResponse(request, "Token验证失败");
            }

            if (token.Expires.CompareTo(DateTime.Now) < 0)
            {
                return CreateErrorResponse(request, "Token已经过期");
            }

            // 兼容老的用户系统设置用户Id
            request.Properties.Add(AccessTokenConst.UseridPropertiesName, token.UserId);

            // ToFix BaseRepostiory Bug
            HttpContext.Current.Items[AccessTokenConst.UseridPropertiesName] = token.UserId;

            // 获取用户信息
            var userProfile = _userProfileProvider.Get(token.UserId);

            //保存UserProfile
            request.Properties.Add(AccessTokenConst.UserProfilePropertiesName, userProfile);

            SetUserPrincipal(userProfile, token.Expires);

            return base.SendAsync(request, cancellationToken);
        }

        private bool IsPass(HttpRequestMessage request)
        {
            return !Enabled || _excludesUrls.Any(c => c == request.RequestUri.AbsolutePath);
        }

        private static Task<HttpResponseMessage> CreateErrorResponse(HttpRequestMessage request, string message)
        {
            HttpResponseMessage errorResponse = request.CreateErrorResponse(HttpStatusCode.Forbidden, message);
            return Task.FromResult(errorResponse);
        }

        private void SetUserPrincipal(UserProfile userProfile, DateTime expires)
        {
            var principal = CreateClaimsPrincipal(userProfile, expires);

            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = Thread.CurrentPrincipal;
        }

        private ClaimsPrincipal CreateClaimsPrincipal(UserProfile userProfile, DateTime expires)
        {
            // 初始化默认
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,userProfile.Name),
                new Claim(ClaimTypes.NameIdentifier,userProfile.Id.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Expired,expires.ToUniversalTime().ToString(CultureInfo.InvariantCulture))
            };

            // 添加角色
            claims.AddRange(userProfile.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            return new ClaimsPrincipal(new ClaimsIdentity[] { new ClaimsIdentity(claims, "OPC_Auth") });
        }
    }
}