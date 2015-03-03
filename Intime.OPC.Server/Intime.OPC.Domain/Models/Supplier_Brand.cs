using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Domain.Models
{
    public partial class Supplier_Brand
    {
        public int Id { get; set; }

        public int Supplier_Id { get; set; }

        public int Brand_Id { get; set; }
    }

}
