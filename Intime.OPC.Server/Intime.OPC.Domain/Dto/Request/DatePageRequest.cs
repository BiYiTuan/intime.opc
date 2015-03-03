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

        [System.Obsolete("����ʹ��StartDate")]
        public DateTime? StartTime
        {
            get { return StartDate; }
            set { StartDate = value; }
        }

        [System.Obsolete("����ʹ��EndDate")]
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

        [System.Obsolete("����ʹ��StartDate")]
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
    /// ���ڲ�ѯά��
    /// </summary>
    public enum DateQueryDimensions
    {
        Default = 0,

        /// <summary>
        /// ��������
        /// </summary>
        CreatedDate = 1,

        /// <summary>
        /// �޸�����
        /// </summary>
        UpdateDate  = 2,

        /// <summary>
        /// ��������
        /// </summary>
        BuyDate= 3,

        /// <summary>
        /// �������� ���˻����ڣ�
        /// </summary>
        ReturnDate = 4
    }
}