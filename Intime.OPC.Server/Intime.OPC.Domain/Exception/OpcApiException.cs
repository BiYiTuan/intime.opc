using System;

namespace Intime.OPC.Domain.Exception
{
    /// <summary>
    /// API 异常基类
    /// </summary>
    public abstract class OpcApiException : System.Exception
    {
        protected OpcApiException(string msg)
            : base(msg)
        {
        }
    }


    public class OpcException : OpcApiException
    {
        public OpcException(string msg)
            : base(msg)
        {
        }
    }
}