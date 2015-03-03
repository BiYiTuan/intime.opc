using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_StorePriority
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int Priority { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUser { get; set; }
    }
}
