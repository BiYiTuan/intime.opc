using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusinessId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int Status { get; set; }
    }
}
