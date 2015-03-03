using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Product2IMSTag
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int IMSTagId { get; set; }
    }
}
