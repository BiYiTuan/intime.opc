using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Promotion2Product
    {
        public int Id { get; set; }
        public Nullable<int> ProdId { get; set; }
        public Nullable<int> ProId { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
