using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_RMALog:IEntity
    {
        public int Id { get; set; }
        public int OpcRmaId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public string Operation { get; set; }
    }
}
