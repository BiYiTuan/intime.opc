using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Repository.Base
{
    public static  class  StringEx
    {
        public static bool IsNotNull(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNull(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }
    }
}
