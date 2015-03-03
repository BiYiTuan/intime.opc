using System.Collections.Generic;
using Intime.O2O.ApiClient.Domain;

namespace Intime.O2O.ApiClient.Response
{
    /// <summary>
    /// 获取商品列表响应
    /// </summary>
    public class GetProductsResponse : Response<IEnumerable<Product>>
    {
    }

    public class GetProductPropertiesResponse : Response<IEnumerable<PropertyValue>>
    {
    }

    public class GetProductPropertiesRawResponse : Response<IEnumerable<PropertyValueRaw>>
    {
    }
}
