using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class ShipVia
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public Nullable<bool> IsOnline { get; set; }
        public string TemplateName { get; set; }
    }
}
