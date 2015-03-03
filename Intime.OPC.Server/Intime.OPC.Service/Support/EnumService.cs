using System.Globalization;
using Intime.OPC.Domain.Dto;
using Intime.OPC.Domain.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Intime.OPC.Service.Support
{
    public class EnumService : IEnumService
    {
        private static readonly ConcurrentDictionary<string, List<Item>> Dict = new ConcurrentDictionary<string, List<Item>>();

        private static readonly object AsyncObj = new object();

        static EnumService()
        {

        }

        public IList<Item> All(string fileName)
        {
            var path = String.Format("{0}\\Config\\{1}.xml", AppDomain.CurrentDomain.BaseDirectory, fileName);// + "Config\\" + fileName + ".xml";

            List<Item> result;
            if (!Dict.TryGetValue(fileName, out result))
            {
                lock (AsyncObj)
                {
                    if (!Dict.TryGetValue(fileName, out result))
                    {
                        using (var fs = File.OpenRead(path))
                        {
                            var xs = new XmlSerializer(typeof(List<Item>));
                            result = (List<Item>)xs.Deserialize(fs);
                        }

                        Dict.TryAdd(fileName, result);
                    }
                }
            }

            if (result == null)
            {
                throw new Exception(String.Format("指定的{0}配置文件读取内容失败!", fileName));
            }

            return result;
        }

        public IList<Item> All(Type enumType)
        {
            IList<Item> lst = new List<Item>();
            var arr = Enum.GetValues(enumType);
            foreach (Enum s in arr)
            {
                var ii = new Item { Key = s.AsId().ToString(CultureInfo.InvariantCulture), Value = s.GetDescription() };
                lst.Add(ii);
            }
            return lst;
        }
    }
}
