using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class CardBlack
    {
        public int Id { get; set; }
        public string CardNo { get; set; }
        public int Status { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
