using System.Linq;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    public class RoleUserRepository : BaseRepository<OPC_AuthRoleUser>, IRoleUserRepository
    {
        public bool UserHasRole(int uId, int roleId)
        {
            var lst = Select(t => t.OPC_AuthRoleId == roleId && t.OPC_AuthUserId == uId);
            return lst.Count != 0;
        }

        public void DeleteByUserID(int id)
        {
            using (var db = new YintaiHZhouContext())
            {
                var lst = db.OPC_AuthRoleUsers.Where(t => t.OPC_AuthUserId == id).ToList();
                db.OPC_AuthRoleUsers.RemoveRange(lst);
                db.SaveChanges();
            }
        }
    }
}