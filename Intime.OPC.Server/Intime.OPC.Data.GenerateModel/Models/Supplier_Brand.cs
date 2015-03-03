using System;
using System.Collections.Generic;

namespace Intime.OPC.Data.GenerateModel.Models
{
    public partial class Supplier_Brand
    {
        public int Id { get; set; }
        public int Supplier_Id { get; set; }
        public int Brand_Id { get; set; }
    }
}
