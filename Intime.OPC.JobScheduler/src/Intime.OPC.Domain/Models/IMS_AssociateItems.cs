using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateItems
    {
        public int Id { get; set; }
        public int AssociateId { get; set; }
        public int ItemType { get; set; }
        public int ItemId { get; set; }
        public int Status { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public int UpdateUser { get; set; }
        public System.DateTime UpdateDate { get; set; }
    }
}
