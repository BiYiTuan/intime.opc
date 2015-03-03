using System;
using System.Collections.Generic;

namespace Intime.OPC.Job.Trade.SplitOrder.Models
{
    /// <summary>
    /// 拆单结果模型
    /// </summary>
    public class SpliltResultModel
    {
        private readonly IEnumerable<SaleOrderModel> _saleOrderInfos;
        private readonly Exception _exception;

        public SpliltResultModel(IEnumerable<SaleOrderModel> saleOrderInfos, Exception exception = null)
        {
            _saleOrderInfos = saleOrderInfos;
            _exception = exception;
        }

        /// <summary>
        /// 销售单列表
        /// </summary>
        public IEnumerable<SaleOrderModel> SaleOrderInfos
        {
            get { return _saleOrderInfos; }
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception
        {
            get
            {
                return _exception;
            }
        }

        /// <summary>
        /// 是否拆单存在异常
        /// </summary>
        public bool HasException
        {
            get { return _exception != null; }
        }
    }
}
