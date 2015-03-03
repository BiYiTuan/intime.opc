using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class RMAReason:IEntity
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int Status { get; set; }
    }
}
