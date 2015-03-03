using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_SalesCode
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public string Code { get; set; }
    }
}
