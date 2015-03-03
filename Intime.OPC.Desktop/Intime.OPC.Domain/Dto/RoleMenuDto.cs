using System.Collections.Generic;

namespace Intime.OPC.Domain.Dto
{
    public class RoleMenuDto
    {
        public int RoleID { get; set; }

        public int UserID { get; set; }

        public IList<int> MenuList { get; set; }
    }
}