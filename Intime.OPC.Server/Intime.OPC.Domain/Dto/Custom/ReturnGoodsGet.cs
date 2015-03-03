using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Dto.Custom
{
    public  class ReturnGoodsRequest: BaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string  OrderNo { get; set; }

        public string Telephone { get; set; }

        public string   PayType { get; set; }

        public int? BandId { get; set; }
    }
}
