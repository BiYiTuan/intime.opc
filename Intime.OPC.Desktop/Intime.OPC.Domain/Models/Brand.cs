using Intime.OPC.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// 品牌
    /// </summary>
    [Uri("brands")]
    public class Brand : Dimension
    {
        private bool _enabled;
        private string _englishName;
        private string _description;

        public Brand()
        {
            Supplier = new Supplier { Id = 4, Name = "SupplierName" };
            _enabled = true;
        }

        /// <summary>
        /// 英文名称
        /// </summary>
        [Display(Name = "英文名称")]
        [LocalizedRequired]
        [LocalizedMaxLength(512)]
        public string EnglishName
        {
            get { return _englishName; }
            set { SetProperty(ref _englishName, value); }
        }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        /// <summary>
        /// 是否已启用
        /// </summary>
        public bool Enabled 
        {
            get { return _enabled; }
            set { SetProperty(ref _enabled, value); }
        }

        /// <summary>
        /// 供应商
        /// </summary>
        public Supplier Supplier { get; set; }

        /// <summary>
        /// 销售这个品牌的专柜
        /// </summary>
        public IList<Counter> Counters { get; set; }
    }
}
