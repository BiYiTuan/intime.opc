using System.Collections.Generic;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Modules.Logistics.Print
{
    public class PrintExpressModel
    {
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        public string CustomerAddress { get; set; }

        /// <summary>
        /// 收货人电话
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        /// 快递费
        /// </summary>
        public string ExpressFee { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 门店地址
        /// </summary>
        public string StoreAddress { get; set; }

        /// <summary>
        /// 门店联系人
        /// </summary>
        public string StorePerson { get; set; }

        /// <summary>
        /// 门店电话
        /// </summary>
        public string StoreTel { get; set; }
    }
}