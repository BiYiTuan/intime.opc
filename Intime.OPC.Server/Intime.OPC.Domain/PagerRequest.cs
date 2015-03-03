using System;
using System.Runtime.Serialization;

namespace Intime.OPC.Domain
{
    /// <summary>
    /// 分页请求
    /// </summary>
    [DataContract]
    public class PagerRequest
    {
        private readonly int _maxPageSize;

        public PagerRequest()
            : this(200)
        {
        }

        public PagerRequest(int maxPageSize)
        {
            this._maxPageSize = maxPageSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerRequest"/> class.
        /// </summary>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        public PagerRequest(int pageNumber, int pageSize)
            : this(pageNumber, pageSize, 200)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PagerRequest"/> class.
        /// </summary>
        /// <param name="pageNumber">
        /// The page number.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="maxPageSize"> </param>
        public PagerRequest(int pageNumber, int pageSize, int maxPageSize)
            : this(maxPageSize)
        {
            this.PageSize = pageSize;
            this.PageIndex = pageNumber;
        }

        /// <summary>
        /// local pageNumber
        /// </summary>
        private int _pageIndex;

        /// <summary>
        /// local pageSize
        /// </summary>
        private int _pageSize;

        /// <summary>
        /// 页大小
        /// </summary>
        [DataMember]
        public int PageSize
        {
            get
            {
                return this._pageSize;
            }

            private set
            {
                this._pageSize = (value < 1 ? 1 : (value > _maxPageSize ? _maxPageSize : value));
            }
        }

        [IgnoreDataMember]
        public int SkipCount
        {
            get { return (PageIndex - 1) * PageSize; }
        }

        /// <summary>
        /// 页码
        /// </summary>
        [DataMember]
        public int PageIndex
        {
            get
            {
                return this._pageIndex;
            }

            private set
            {
                this._pageIndex = value < 1 ? 1 : value;
            }
        }

        public static bool TryParse(string query, out PagerRequest result)
        {
            result = null;

            if (String.IsNullOrEmpty(query))
            {
                return false;
            }

            var parts = query.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }

            int page, pageSize;

            if ((Int32.TryParse(parts[0], out page)) && (Int32.TryParse(parts[1], out pageSize)))
            {
                result = new PagerRequest(page, pageSize);

                return true;
            }

            return false;
        }
    }
}
