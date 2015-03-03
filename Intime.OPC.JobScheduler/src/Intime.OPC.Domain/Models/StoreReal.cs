using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class StoreReal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StoreNo { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUser { get; set; }
    }
}
