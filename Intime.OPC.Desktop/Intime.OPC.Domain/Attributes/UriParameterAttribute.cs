using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Attributes
{
    public class UriParameterAttribute : Attribute
    {
        public UriParameterAttribute(string name, object defaultValue)
        {
            Name = name;
            DefaultValue = defaultValue;
        }

        public UriParameterAttribute(string name)
            :this(name, null)
        {
        }

        public string Name { get; set; }

        public object DefaultValue { get; set; }
    }
}
