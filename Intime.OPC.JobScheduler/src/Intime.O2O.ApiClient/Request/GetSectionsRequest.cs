using Intime.O2O.ApiClient.Response;
using System;

namespace Intime.O2O.ApiClient.Request
{
    /// <summary>
    /// 查询所有专柜请求
    /// </summary>
    public class GetSectionsRequest : Request<GetSectionsRequestData, GetSectionsResponse>
    {
        public override string GetResourceUri()
        {
            return "counter/queryCounters";
        }
    }
}
