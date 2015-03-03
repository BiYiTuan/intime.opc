using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Intime.OPC.WebApi.Core.MessageHandlers.AccessToken
{
    /// <summary>
    /// 绑定用户接口中的UserId
    /// </summary>
    public class UserIdFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Properties.ContainsKey(AccessTokenConst.UseridPropertiesName))
            {
                if (actionContext.ActionArguments.ContainsKey("userId"))
                {
                    actionContext.ActionArguments["userId"] = actionContext.Request.Properties[AccessTokenConst.UseridPropertiesName];
                }
                else
                {
                    actionContext.ActionArguments.Add("userId", actionContext.Request.Properties[AccessTokenConst.UseridPropertiesName]);
                }


                // 修改串用户的BUG,临时解决方案
                //var userIdKey = "__ACCESS_TOKEN__USERID__";
                if (HttpContext.Current.Items.Contains("__ACCESS_TOKEN__USERID__"))
                {
                    HttpContext.Current.Items["__ACCESS_TOKEN__USERID__"] = actionContext.Request.Properties[AccessTokenConst.UseridPropertiesName];
                }
                else
                {
                    HttpContext.Current.Items.Add("__ACCESS_TOKEN__USERID__", actionContext.Request.Properties[AccessTokenConst.UseridPropertiesName]);
                }
            }
        }
    }
}
