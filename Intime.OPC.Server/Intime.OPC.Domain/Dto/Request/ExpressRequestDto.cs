using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain.Dto.Financial;

namespace Intime.OPC.Domain.Dto.Request
{
    public class ExpressRequestDto : DatePageRequest
    {
        public string OrderNo { get; set; }
    }
}
