using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Attributes;
using Intime.OPC.Domain.Validation;

namespace Intime.OPC.Domain.Models
{
    /// <summary>
    /// Dimension such as brand, counter, organization etc.
    /// </summary>
    public abstract class Dimension : Model
    {
        private string _name;

        /// <summary>
        /// 名字
        /// </summary>
        [Display(Name = "名字")]
        [LocalizedRequired]
        [LocalizedMaxLength(64)]
        public string Name 
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
    }
}
