using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class IMS_GiftCardUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string GiftCardAccount { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUser { get; set; }
    }
}
