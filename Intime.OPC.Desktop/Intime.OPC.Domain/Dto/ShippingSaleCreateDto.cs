using Intime.OPC.Domain.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Intime.OPC.Domain.Dto
{
    public class ShippingSaleCreateDto : ValidatableBindableBase
    {
        public ShippingSaleCreateDto()
        {
            SaleOrderIDs = new List<string>();
        }

        public string OrderNo { get; set; }

        public string ShippingStatusName { get; set; }

        public double ShippingFee { get; set; }

        public string ShippingCode { get; set; }

        public int? ShipViaID { get; set; }

        public string ShipViaName { get; set; }

        public string RmaNo { get; set; }

        public IList<string> SaleOrderIDs { get; set; }
    }
}