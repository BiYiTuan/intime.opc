using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Dto.Custom
{
    public class PackageReceiveRequest : BaseRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string OrderNo { get; set; }

        public string SaleOrderNo { get; set; }

    }
}
