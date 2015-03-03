using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_AuthRoleMenu
    {
        public int Id { get; set; }
        public int OPC_AuthMenuId { get; set; }
        public int OPC_AuthRoleId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
    }
}
