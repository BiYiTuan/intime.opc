using System;

namespace Intime.OPC.Domain.Dto.Request
{
    public class DatePageRequest : PageRequest
    {
        private DateTime? _startDateTime;
        private DateTime? _endDateTime;

        public DatePageRequest()
        {
        }

        public DatePageRequest(int maxPageSize)
            : base(maxPageSize)
        {
        }

        [System.Obsolete("建议使用StartDate")]
        public DateTime? StartTime
        {
            get { return StartDate; }
            set { StartDate = value; }
        }

        [System.Obsolete("建议使用EndDate")]
        public DateTime? EndTime
        {
            get { return EndDate; }
            set { EndDate = value; }
        }

        public DateTime? StartDate
        {
            get { return _startDateTime; }
            set { this._startDateTime = value; }
        }

        public DateTime? EndDate
        {
            get { return _endDateTime; }
            set { this._endDateTime = value; }
        }

        [System.Obsolete("建议使用StartDate")]
        public DateTime? BeginDate
        {
            get { return _startDateTime; }
            set { this._startDateTime = value; }
        }

        private void FormatDate()
        {
            if (_startDateTime != null)
            {
                _startDateTime = _startDateTime.Value.Date;
            }

            if (_endDateTime != null)
            {
                _endDateTime = _endDateTime.Value.Date.AddDays(1);
            }
        }

        public override void ArrangeParams()
        {
            FormatDate();

            base.ArrangeParams();
        }
    }

    /// <summary>
    /// 日期查询维度
    /// </summary>
    public enum DateQueryDimensions
    {
        Default = 0,

        /// <summary>
        /// 创建日期
        /// </summary>
        CreatedDate = 1,

        /// <summary>
        /// 修改日期
        /// </summary>
        UpdateDate  = 2,

        /// <summary>
        /// 购买日期
        /// </summary>
        BuyDate= 3,

        /// <summary>
        /// 返回日期 （退货日期）
        /// </summary>
        ReturnDate = 4
    }
}