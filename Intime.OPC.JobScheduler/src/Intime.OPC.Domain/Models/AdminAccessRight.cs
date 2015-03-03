using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class AdminAccessRight
    {
        public AdminAccessRight()
        {
            this.RoleAccessRights = new List<RoleAccessRight>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ControllName { get; set; }
        public string ActionName { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> InDate { get; set; }
        public Nullable<int> InUser { get; set; }
        public virtual ICollection<RoleAccessRight> RoleAccessRights { get; set; }
    }
}
