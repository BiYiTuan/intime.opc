
using System.Collections.Generic;

namespace Intime.O2O.ApiClient
{
    /// <summary>
    /// Api接口类
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// 执行请求
        /// </summary>
        /// <typeparam name="TRequest">请求对象类型</typeparam>
        /// <typeparam name="TResponse">响应对象类型</typeparam>
        /// <param name="request">请求对象</param>
        /// <param name="traceEnabled">是否启用trace</param>
        /// <returns>响应结果</returns>
        TResponse Post<TRequest, TResponse>(Request<TRequest, TResponse> request, bool traceEnabled = false);

        //TResponse Post<TRequest, TResponse>(Request<TRequest, TResponse> request);

        IEnumerable<string> ErrorList();
    }
}
