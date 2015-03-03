using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Intime.OPC.Domain.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// USE JavaScriptSerializer
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            var serializer = new JavaScriptSerializer();
            
            return serializer.Serialize(obj);
        }

        /// <summary>
        ///  USE JavaScriptSerializer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Deserialize<T>(json);
        }
    }
}
