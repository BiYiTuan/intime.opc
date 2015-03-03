using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Attributes
{
    public class UriAttribute : Attribute
    {
        public UriAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
