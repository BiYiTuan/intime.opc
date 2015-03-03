using System;

namespace OPCAPP.Domain.Dto.Financial
{
    public class FinancialInfoGet
    {
        public FinancialInfoGet()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     支付类型
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        ///     门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        ///     财务类型
        /// </summary>
        public string FinancialType { get; set; }

        public override string ToString()
        {
            return string.Format("StartDate={0}&EndDate={1}&PayType={2}&StoreName={3}&FinancialType={4}", StartDate,
                EndDate, PayType, StoreName, FinancialType);
        }
    }
}