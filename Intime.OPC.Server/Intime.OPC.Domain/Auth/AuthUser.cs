using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Intime.OPC.Domain.Auth
{
    public class AuthUser:IAuthUser
    {
        readonly List<int> _authStoreIds = new List<int>(); 
        public AuthUser(int uid)
        {
            using (var db = new YintaiHZhouContext())
            {
                var user = db.OPC_AuthUsers.FirstOrDefault(x => x.Id == uid);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("未授权用户");
                }

                IsAdmin = user.IsSystem;
                if (IsAdmin)
                {
                    _authStoreIds.AddRange(db.Stores.Where(x=>x.Status == 1).Select(t=>t.Id));
                    return;
                }

                var authStore = db.OPC_OrgInfos.FirstOrDefault(o => o.OrgID == user.DataAuthId && o.OrgType == 5);
                if (authStore == null || !authStore.StoreOrSectionID.HasValue)
                {
                    throw new UserException(string.Format("用户关联的组织机构不存在:user id :{0}, userName {1}", uid, user.Name));
                }

                
                _authStoreIds.Add(authStore.Id);
            }
        }

        public IEnumerable<int> AuthenticatedStores()
        {
            return _authStoreIds;
        }

        public bool IsAdmin { get; private set; }
    }
}
