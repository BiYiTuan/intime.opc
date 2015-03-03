using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Intime.OPC.Domain
{
    [DataContract(Name = "pager")]
    public class PagerInfo<T> : IPagedable
    {
        #region fields

        #endregion

        #region .ctor

        public PagerInfo()
        {
        }

        public PagerInfo(PagerRequest request)
            : this(request, 0)
        {
        }

        public PagerInfo(PagerRequest request, int totalCount)
        {
            this.Index = request.PageIndex;
            this.Size = request.PageSize;
            this.TotalCount = totalCount;
        }

        public PagerInfo(PagerRequest request, int totalCount, List<T> datas)
        {
            this.Index = request.PageIndex;
            this.Size = request.PageSize;
            this.TotalCount = totalCount;
            this.Datas = datas;
        }

        #endregion

        #region properties

        /// <summary>
        /// 索引
        /// </summary>
        [DataMember(Name = "pageindex", Order = 1)]
        public int Index { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        [DataMember(Name = "pagesize", Order = 2)]
        public int Size { get; set; }

        /// <summary>
        /// 总数据量
        /// </summary>
        [DataMember(Name = "totalcount", Order = 3)]
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [IgnoreDataMember]
        public int TotalPaged
        {
            get
            {
                return (int)Math.Ceiling(this.TotalCount / (double)this.Size);
            }
            set { }
        }

        /// <summary>
        /// 是否存在分页
        /// </summary>
        [IgnoreDataMember]
        public bool IsPaged
        {
            get
            {
                return Size < TotalCount;
            }
            set { }
        }

        /// <summary>
        /// 总数据量
        /// </summary>
        [DataMember(Name = "datas", Order = 4)]
        public List<T> Datas { get; set; }

        #endregion

        #region methods

        #endregion
    }
}