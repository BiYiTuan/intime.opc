using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateIncomeTran2Req
    {
        public int Id { get; set; }
        public int FullPackageId { get; set; }
        public int RequestId { get; set; }
    }
}
