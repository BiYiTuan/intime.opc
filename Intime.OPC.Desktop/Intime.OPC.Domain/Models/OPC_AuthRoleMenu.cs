using System;

namespace Intime.OPC.Domain.Models
{
    public class OPC_AuthRoleMenu
    {
        public int Id { get; set; }
        public int OPC_AuthMenuId { get; set; }
        public int OPC_AuthRoleId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
    }
}