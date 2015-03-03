using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Dto.Request
{
    public class BankQueryRequest : PageRequest
    {
        public string PreName { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int? Status { get; set; }

        public override void ArrangeParams()
        {
            base.ArrangeParams();

            Status = CheckIsNullOrAndSet(Status);
        }
    }
}
