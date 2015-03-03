using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Validation;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// 专柜
    /// </summary>
    [Uri("counters")]
    public class Counter : Dimension
    {
        private bool _repealed;
        private string _code;
        private string _areaCode;
        private string _contactPhoneNumber;
        private Store _store;

        /// <summary>
        /// 专柜码
        /// </summary>
        [Display(Name = "专柜码")]
        [LocalizedRequired()]
        [LocalizedMaxLength(200)]
        public string Code
        {
            get { return _code; }
            set { SetProperty(ref _code, value); }
        }

        /// <summary>
        /// 销售区域
        /// </summary>
        [Display(Name = "销售区域")]
        [LocalizedMaxLength(200)]
        public string AreaCode
        {
            get { return _areaCode; }
            set { SetProperty(ref _areaCode, value); }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        [LocalizedMaxLength(50)]
        public string ContactPhoneNumber
        {
            get { return _contactPhoneNumber; }
            set { SetProperty(ref _contactPhoneNumber, value); }
        }

        /// <summary>
        /// 是否已撤柜
        /// </summary>
        public bool Repealed
        {
            get { return _repealed; }
            set { SetProperty(ref _repealed, value); }
        }

        /// <summary>
        /// 门店ID
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 门店
        /// </summary>
        [Display(Name = "所属门店")]
        [Required(ErrorMessage = "必须选择一家门店")]
        public Store Store
        {
            get { return _store; }
            set { SetProperty(ref _store, value); }
        }

        /// <summary>
        /// 销售的品牌
        /// </summary>
        public IList<Brand> Brands { get; set; }

        /// <summary>
        /// 管理部门
        /// </summary>
        public Organization Organization { get; set; }
    }
}
