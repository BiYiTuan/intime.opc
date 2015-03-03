using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Logistics.Models
{
    public class Invoice4Get
    {
        public Invoice4Get()
        {
            StartSellDate = DateTime.Now;
            EndSellDate = DateTime.Now;
        }

        /// <summary>
        /// 开始时间条件
        /// </summary>
        public DateTime StartSellDate { get; set; }

        /// <summary>
        /// 结束时间条件
        /// </summary>
        public DateTime EndSellDate { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SaleOrderNo { get; set; }
    }
}
