using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class OPC_AuthMenu
    {
        public int Id { get; set; }
        public string MenuName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public System.DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
        public int PraentMenuId { get; set; }
        public bool IsValid { get; set; }
        public int Sort { get; set; }
        public string Url { get; set; }
    }
}
