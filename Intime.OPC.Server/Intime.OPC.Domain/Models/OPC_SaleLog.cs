using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_SaleLog:IEntity
    {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }
        public int Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
    }
}
