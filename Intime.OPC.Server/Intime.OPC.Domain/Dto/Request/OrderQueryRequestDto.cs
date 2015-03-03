
using System;
using System.Collections.Generic;
using System.Text;
using Intime.OPC.Domain.Enums.SortOrder;

namespace Intime.OPC.Domain.Dto.Request
{
    public class OrderQueryRequest : PageRequest
    {
        public string OrderNo { get; set; }

        public string OrderSource { get; set; }

        public DateTime? StartCreateDate { get; set; }

        public DateTime? EndCreateDate { get; set; }

        public int? BrandId { get; set; }

        public int? StoreId { get; set; }

        public int? Status { get; set; }

        public string PaymentType { get; set; }

        public string OutGoodsType { get; set; }

        public string ShippingContactPhone { get; set; }

        public string ExpressDeliveryCode { get; set; }

        public int? ExpressDeliveryCompany { get; set; }

        /// <summary>
        /// 物流号
        /// </summary>
        public string ShippingNo { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<int> DataRoleStores { get; set; }

        public int? SortOrder { get; set; }

        public OrderSortOrder OrderSortOrder
        {
            get { return SortOrder.HasValue ? ((OrderSortOrder)SortOrder) : OrderSortOrder.Default; }
        }

        /// <summary>
        /// 整理参数
        /// </summary>
        public override void ArrangeParams()
        {
            this.Status = CheckIsNullOrAndSet(this.Status);
            this.StoreId = CheckIsNullOrAndSet(this.StoreId);
            this.BrandId = CheckIsNullOrAndSet(this.BrandId);
            this.ExpressDeliveryCompany = CheckIsNullOrAndSet(this.ExpressDeliveryCompany);


            if (EndCreateDate != null)
            {
                this.EndCreateDate = this.EndCreateDate.Value.AddDays(1);
            }

            base.ArrangeParams();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(ShippingContactPhone))
            {
                sb.AppendFormat("{0}:{1}_", "ShippingContactPhone", ShippingContactPhone);
            }

            return sb.ToString();
        }
    }

    public class StoreRequest : PageRequest
    {
        public StoreRequest()
        {
        }

        public StoreRequest(int maxPageSize)
            : base(maxPageSize)
        {
        }

        public string NamePrefix { get; set; }

        public int? Status { get; set; }

        public int? SortOrder { get; set; }

        public string Name { get; set; }

        public List<int> DataStoreIds { get; set; }

        public StoreSortOrder StoreSortOrder
        {
            get { return SortOrder.HasValue ? ((StoreSortOrder)SortOrder) : StoreSortOrder.Default; }
        }

        /// <summary>
        /// 整理参数
        /// </summary>
        public override void ArrangeParams()
        {
            this.Status = CheckIsNullOrAndSet(this.Status);

            base.ArrangeParams();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IArrangeParamsRequest
    {
        /// <summary>
        /// 参数整理
        /// </summary>
        void ArrangeParams();
    }

    public class PageRequest : IArrangeParamsRequest
    {
        private int? _pageIndex = 1;

        private int? _pageSize = 40;

        private readonly int _maxPageSize;

        public PageRequest(int maxPageSize = 400)
        {
            _maxPageSize = maxPageSize;
        }

        /// <summary>
        /// page == pageindex
        /// </summary>
        public int? PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                if (value == null || value < 0)
                {
                    value = 1;
                }

                _pageIndex = value;
            }
        }

        /// <summary>
        /// page == pageindex
        /// </summary>
        public int? Page
        {
            get
            {
                return PageIndex;
            }
            set { PageIndex = value; }
        }

        public int? PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > _maxPageSize)
                {
                    value = _maxPageSize;
                }

                if (value < 0)
                {
                    value = 0;
                }

                _pageSize = value;
            }
        }

        public Domain.PagerRequest PagerRequest
        {
            get { return new Domain.PagerRequest(this.PageIndex ?? 0, this.PageSize ?? 0, _maxPageSize); }
        }

        private static readonly int? DefNullableInt = null;

        /// <summary>
        /// 检查是否为 NULL OR -1 返回 NULL
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected int? CheckIsNullOrAndSet(int? val)
        {
            return CheckIsNullOrAndSet(val, DefNullableInt);
        }

        /// <summary>
        /// 检查是否为 NULL OR -1 返回 defalueVal
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defalueVal">默认值</param>
        /// <returns></returns>
        protected int? CheckIsNullOrAndSet(int? val, int? defalueVal)
        {
            if (val == null || val == -1)
            {
                return defalueVal;
            }

            return val;
        }





        /// <summary>
        /// 整理参数
        /// </summary>
        public virtual void ArrangeParams()
        {
        }
    }
}
