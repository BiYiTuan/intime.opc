using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.REST
{
    public class RestException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public RestException(HttpStatusCode statusCode, string message)
            :base(message)
        {
            StatusCode = statusCode;
        }

        protected RestException(SerializationInfo info, StreamingContext context)
            :base(info, context)
        { }

        public RestException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
