
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Job.Trade.SplitOrder.Models
{
    /// <summary>
    /// 专柜销售清单项数据模型
    /// </summary>
    public class SectionSaleDetailInfo
    {
        private readonly int _sectionId;
        private readonly OPC_SaleDetail _saleDetail;

        public SectionSaleDetailInfo(int sectionId, OPC_SaleDetail saleDetail)
        {
            _sectionId = sectionId;
            _saleDetail = saleDetail;
        }

        /// <summary>
        /// 专柜Id
        /// </summary>
        public int SectionId
        {
            get { return _sectionId; }
        }

        /// <summary>
        /// 销售清单信息
        /// </summary>
        public OPC_SaleDetail SaleDetail
        {
            get { return _saleDetail; }
        }
    }
}
