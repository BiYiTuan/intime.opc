
using System;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public class ExectueResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 执行结果编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 执行结果 信息（如果有错误，会包含错误信息）
        /// </summary>
        public string Message { get; set; }
    }


    public class OkExectueResult : ExectueResult
    {
        public OkExectueResult()
        {
            IsSuccess = true;
            Code = 200;
            Message = "Success";
        }
    }

    public class FailureExectueResult : ExectueResult
    {
        public FailureExectueResult()
        {
            IsSuccess = false;
            Message = "Failure";
        }
    }

    /// <summary>
    /// 执行结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExectueResult<T> : ExectueResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

    public class OkExectueResult<T> : ExectueResult<T>
    {
        public OkExectueResult(T data)
        {
            IsSuccess = true;
            Code = 200;
            Message = "Success";
            Data = data;
        }
    }

    public class FailureExectueResult<T> : ExectueResult<T>
    {
        public FailureExectueResult(string msg)
        {
            IsSuccess = false;
            Code = 400;
            Message = msg;
        }
    }
}
