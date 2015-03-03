using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Support
{
    public class AccountRepository : BaseRepository<OPC_AuthUser>, IAccountRepository
    {
        private log4net.ILog _log = log4net.LogManager.GetLogger(typeof(AccountRepository));
        #region methods

        private static Expression<Func<OPC_AuthUser, bool>> OPC_AuthUserFiller(bool? incloudSystem, List<string> authdatastartsWith, string name = null, string logonName = null)
        {
            var query = PredicateBuilder.True<OPC_AuthUser>();

            if (authdatastartsWith != null && authdatastartsWith.Count > 0)
            {
                foreach (var str in authdatastartsWith)
                {
                    var str1 = str;
                    query = PredicateBuilder.And(query, v => v.DataAuthId.StartsWith(str1));
                }
            }

            if (incloudSystem != null)
            {
                query = PredicateBuilder.And(query, v => v.IsSystem == incloudSystem);
            }

            if (!String.IsNullOrEmpty(name))
            {
                query = PredicateBuilder.And(query, v => v.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(logonName))
            {
                query = PredicateBuilder.And(query, v => v.LogonName.Contains(logonName));
            }

            return query;
        }


        private static Expression<Func<OPC_AuthRoleUser, bool>> OPC_AuthRoleUsersFiller(int? roleid)
        {
            var query = PredicateBuilder.True<OPC_AuthRoleUser>();

            if (roleid != null)
            {
                query = PredicateBuilder.And(query, v => v.OPC_AuthRoleId == roleid);
            }

            return query;
        }

        #endregion

        #region IAccountRepository Members

        public bool Delete(int id)
        {
            using (var db = new YintaiHZhouContext())
            {
                var d = db.OPC_AuthUsers.FirstOrDefault(t => t.Id == id);
                if (d != null && d.IsSystem)
                {
                    throw new Exception("系统管理员，不能删除");
                }
                return base.Delete(id);
            }
        }


        public OPC_AuthUser Get(string userName, string password)
        {
            using (var db = new YintaiHZhouContext())
            {
                return db.OPC_AuthUsers.FirstOrDefault(t => t.LogonName == userName && t.Password == password);
            }
        }

        public bool SetEnable(int userId, bool enable)
        {
            using (var db = new YintaiHZhouContext())
            {
                OPC_AuthUser user = db.OPC_AuthUsers.FirstOrDefault(t => t.Id == userId);
                if (user != null)
                {
                    user.IsValid = enable;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public PageResult<OPC_AuthUser> GetByRoleId(int roleId, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                IQueryable<OPC_AuthUser> lst = db.OPC_AuthRoleUsers.Where(t => t.OPC_AuthRoleId == roleId)
                    .Join(db.OPC_AuthUsers.Where(t => t.IsSystem == false), t => t.OPC_AuthUserId, o => o.Id, (t, o) => o);

                lst = lst.OrderBy(t => t.Id);
                return lst.ToPageResult(pageIndex, pageSize);
            }
        }


        public PagerInfo<OPC_AuthUser> GetPagedList(PagerRequest pagerRequest, int? roleId, List<string> authdatastartsWith, bool? incloudeSystem, string name = null, string logonName = null)
        {
            var authuserfilter = OPC_AuthUserFiller(incloudeSystem, authdatastartsWith, name, logonName);
            var authuserrolefilter = OPC_AuthRoleUsersFiller(roleId);
            using (var db = new YintaiHZhouContext())
            {
                var authusers = db.OPC_AuthUsers;
                var authuserroles = db.OPC_AuthRoleUsers;

                IQueryable<OPC_AuthUser> sql;
                if (roleId == null)
                {
                     sql = from authuser in authusers.AsExpandable().Where(authuserfilter)
                              join authuserrole in authuserroles on authuser.Id
                                  equals authuserrole.OPC_AuthUserId into tmp1
                              from authuserrole in tmp1.DefaultIfEmpty()
                              select authuser;
                }
                else
                {
                    sql = from authuser in authusers.AsExpandable().Where(authuserfilter)
                          join authuserrole in authuserroles.AsExpandable().Where(authuserrolefilter) on authuser.Id
                              equals authuserrole.OPC_AuthUserId
                          select authuser;
                }


                var totalCount = sql.Count();

                var datas = sql.OrderBy(v => v.Id)
                        .Skip(pagerRequest.SkipCount)
                        .Take(pagerRequest.PageSize);

                return new PagerInfo<OPC_AuthUser>(pagerRequest, totalCount, datas.ToList());
            }
        }


        public PageResult<OPC_AuthUser> All(int pageIndex, int pageSize = 20)
        {
            return Select(t => !t.IsSystem, t => t.Name, true, pageIndex, pageSize);
        }

        #endregion


        public PageResult<OPC_AuthUser> GetByOrgId(string orgID, int pageIndex, int pageSize)
        {
            return Select2<OPC_AuthUser, string>(t => t.OrgId == orgID, o => o.Name, true, pageIndex,
                pageSize);
        }

        public PageResult<OPC_AuthUser> GetByLoginName(string orgID, string loginName, int pageIndex, int pageSize)
        {
            using (var db = new YintaiHZhouContext())
            {
                var lst = db.OPC_AuthUsers.Where(t => !t.IsSystem);
                if (!string.IsNullOrWhiteSpace(loginName))
                {
                    lst = lst.Where(t => t.LogonName.Contains(loginName));
                }
                if (!string.IsNullOrEmpty(orgID))
                {
                    lst = lst.Where(t => t.OrgId == orgID);
                }
                lst = lst.OrderBy(t => t.LogonName);
                return lst.ToPageResult(pageIndex, pageSize);
            }
        }

        public PageResult<OPC_AuthUser> GetByOrgId(string orgID, string name, int pageIndex, int pageSize)
        {

            using (var db = new YintaiHZhouContext())
            {
                var lst = db.OPC_AuthUsers.Where(t => t.IsSystem == false);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    lst = lst.Where(t => t.Name.Contains(name));
                }
                if (!string.IsNullOrEmpty(orgID))
                {
                    lst = lst.Where(t => t.OrgId == orgID);
                }
                lst = lst.OrderBy(t => t.Name);
                return lst.ToPageResult(pageIndex, pageSize);
            }
        }
    }
}