using System.Collections.Generic;

namespace Intime.OPC.Domain.Dto
{
    public class RoleAuthDto
    {
        public int RoleID { get; set; }

        public int UserID { get; set; }

        /*菜单IDS 或者 用户IDS*/
        public IList<int> IDList { get; set; }
    }
}