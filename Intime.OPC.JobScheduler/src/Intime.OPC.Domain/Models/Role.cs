using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class Role
    {
        public Role()
        {
            this.RoleAccessRights = new List<RoleAccessRight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int CreatedUser { get; set; }
        public int Status { get; set; }
        public int Val { get; set; }
        public virtual ICollection<RoleAccessRight> RoleAccessRights { get; set; }
    }
}
