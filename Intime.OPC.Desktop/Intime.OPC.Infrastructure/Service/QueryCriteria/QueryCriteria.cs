using Intime.OPC.Domain.Attributes;
using System;
using System.Reflection;
using System.Text;

namespace Intime.OPC.Infrastructure.Service
{
    using Microsoft.Practices.Prism.Mvvm;

    public class QueryCriteria : BindableBase, IQueryCriteria
    {
        private const int DefaultPageIndex = 1;
        private const int DefaultPageSize = 100;

        public QueryCriteria()
        {
            PageIndex = DefaultPageIndex;
            PageSize = DefaultPageSize;
        }

        [UriParameter("page")]
        public int PageIndex { get; set; }

        [UriParameter("pagesize")]
        public int PageSize { get; set; }

        public string BuildQueryString()
        {
            var queryString = new StringBuilder();
            var properties = this.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var parameterValue = GetParameterValue(propertyInfo);
                var attributes = propertyInfo.GetCustomAttributes(typeof(UriParameterAttribute), true);
                if (attributes.Length > 0)
                {
                    var uriParameter = (UriParameterAttribute)attributes[0];
                   
                    queryString.Append(string.Format("{0}={1}&", uriParameter.Name, parameterValue));
                }
                else
                {
                    queryString.Append(string.Format("{0}={1}&", propertyInfo.Name, parameterValue));
                }
            }
            if (queryString.Length > 0) queryString.Remove(queryString.Length - 1, 1);

            return queryString.ToString();
        }

        private string GetParameterValue(PropertyInfo propertyInfo)
        {
            var propertyValue = propertyInfo.GetValue(this);

            if (propertyValue == null)
            {
                return string.Empty;
            }

            if (propertyValue.GetType().IsEnum)
            {
                propertyValue = (int)propertyValue;
            }

            if (propertyValue.GetType().Equals(typeof(DateTime)))
            {
                propertyValue = ((DateTime)propertyValue).ToShortDateString();
            }

            return propertyValue.ToString();
        }
    }
}
