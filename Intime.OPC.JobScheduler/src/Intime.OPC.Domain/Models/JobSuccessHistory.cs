using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class JobSuccessHistory
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public int JobType { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
