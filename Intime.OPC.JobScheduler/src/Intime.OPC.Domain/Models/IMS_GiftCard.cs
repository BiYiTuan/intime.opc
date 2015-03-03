using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_GiftCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
