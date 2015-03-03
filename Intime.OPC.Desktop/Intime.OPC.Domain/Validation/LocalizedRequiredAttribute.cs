using Intime.OPC.Domain.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Validation
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
        public LocalizedRequiredAttribute()
        {
            ErrorMessageResourceType = typeof(Resources);
            ErrorMessageResourceName = this.GetType().Name.Replace("Attribute",string.Empty);
        }
    }
}
