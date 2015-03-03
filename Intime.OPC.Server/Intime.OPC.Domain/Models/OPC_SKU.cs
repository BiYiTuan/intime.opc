using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_SKU:IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorValueId { get; set; }
        public int SizeValueId { get; set; }
    }
}
