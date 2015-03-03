using System;
using System.Collections.Generic;

namespace Intime.OPC.Domain.Models
{
    public partial class RoleAccessRight
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AccessRightId { get; set; }
        public virtual AdminAccessRight AdminAccessRight { get; set; }
        public virtual Role Role { get; set; }
    }
}
