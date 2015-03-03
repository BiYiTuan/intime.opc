using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public int ExCatId { get; set; }
        public string Name { get; set; }
        public string ExCatCode { get; set; }
        public int Status { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int TagId { get; set; }
    }
}
