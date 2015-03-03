using Intime.OPC.Domain;
using Intime.OPC.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Logistics.Models
{
    public class ExpressReceiptCreationDTO : ValidatableBindableBase
    {
        private string _shippingNo;

        public int DeliveryOrderId { get; set; }

        [Display(Name = "快递费")]
        [LocalizedRequired]
        public double ShippingFee { get; set; }

        [Display(Name = "快递单号")]
        [LocalizedRequired]
        public string ShippingNo
        {
            get { return _shippingNo; }
            set { SetProperty(ref _shippingNo, value); }
        }

        [Display(Name = "快递公司")]
        [LocalizedRequired]
        public int? ShipViaID { get; set; }

        public string ShippingRemark { get; set; }
    }
}
