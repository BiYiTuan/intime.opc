using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class Supplier : Dimension
    {
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string Code { get; set; }
    }
}
