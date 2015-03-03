using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_AssociateBrand
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
        public int Status { get; set; }
        public int AssociateId { get; set; }
        public int BrandId { get; set; }
    }
}
