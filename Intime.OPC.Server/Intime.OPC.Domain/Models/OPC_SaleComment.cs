using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_SaleComment:IEntity
    {
        public int Id { get; set; }
        public string SaleOrderNo { get; set; }
        public string Content { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
    }
}
