using System.Data.Entity.Validation;
using Intime.OPC.Domain.Exception;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Intime.OPC.WebApi.Core.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ApiExceptionFilterAttribute));

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext == null)
            {
                return;
            }

            _log.Error(actionExecutedContext.ActionContext.ActionArguments);
            _log.Error(actionExecutedContext.ActionContext.RequestContext);

            if (actionExecutedContext.Exception is OpcApiException)
            {
                //都是业务异常，一般都是客户端的错误造成的
                var errorMessagError = actionExecutedContext.Exception.Message;
                actionExecutedContext.Response =
                   actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessagError);
            }
            else
            {
                actionExecutedContext.Response =
    actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "系统内部 错误");
            }

            while (actionExecutedContext.Exception != null)
            {
                _log.Error(actionExecutedContext.Exception);
                //ef 异常处理
                var exception = actionExecutedContext.Exception as DbEntityValidationException;
                if (exception != null)
                {
                    var efException = exception;

                    if (efException.EntityValidationErrors != null)
                    {
                        foreach (var evr in efException.EntityValidationErrors)
                        {
                            if (evr.ValidationErrors != null)
                            {
                                foreach (var ve in evr.ValidationErrors)
                                {
                                    _log.ErrorFormat("ef_eve_error:[p:{1},m:{0}]", ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                        }
                    }
                }

                actionExecutedContext.Exception = actionExecutedContext.Exception.InnerException;
            }
        }
    }
}
