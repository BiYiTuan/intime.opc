using System.Collections.Generic;
using Intime.OPC.Modules.Logistics.Models;

namespace Intime.OPC.Modules.Logistics.Services
{
    /// <summary>
    /// 门店接口定义
    /// </summary>
    public interface IInvoiceService
    {
        List<Invoice> SearchData(Invoice4Get invoice4get);

        /// <summary>
        /// 完成打印销售单，此接口只更新订单状态，可以考虑传递不同的参数，复用
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        ResultMsg SetFinish(int Type); 
    }
}