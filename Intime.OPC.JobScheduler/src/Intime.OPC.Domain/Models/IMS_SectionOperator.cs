using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_SectionOperator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectionId { get; set; }
        public string OperatorCode { get; set; }
    }
}
