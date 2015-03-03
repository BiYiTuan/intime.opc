using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_SectionBrand
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int BrandId { get; set; }
    }
}
