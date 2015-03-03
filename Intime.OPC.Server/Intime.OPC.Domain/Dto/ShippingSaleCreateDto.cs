using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto
{
    public  class ShippingSaleCreateDto
    {
        public ShippingSaleCreateDto()
        {
            SaleOrderIDs=new List<string>();
        }

        public decimal ShippingFee { get; set; }

        public string ShippingCode { get; set; }

        public int ShipViaID { get; set; }

        public string ShipViaName { get; set; }
        public string OrderNo { get; set; }

        public IList<string> SaleOrderIDs { get; set; }
    }
}
