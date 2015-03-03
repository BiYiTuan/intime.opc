using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Dto.Request;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;
using LinqKit;
using PredicateBuilder = LinqKit.PredicateBuilder;

namespace Intime.OPC.Repository.Support
{
    public class OrgInfoRepository : BaseRepository<OPC_OrgInfo>, IOrgInfoRepository
    {
        #region methods

        private static Expression<Func<OPC_OrgInfo, bool>> OPC_OrgInfoFiller(List<string> startsWith, EnumOrgType? type)
        {
            var query = PredicateBuilder.True<OPC_OrgInfo>();

            if (type != null)
            {
                query = PredicateBuilder.And(query, v => v.OrgType == type.AsId());
            }

            if (startsWith != null && startsWith.Count > 0)
            {
                foreach (var str in startsWith)
                {
                    var str1 = str;
                    query = PredicateBuilder.And(query, v => v.OrgID.StartsWith(str1));
                }
            }

            return query;
        }

        #endregion

        public IList<OPC_OrgInfo> GetByOrgType(string orgid, int orgtype)
        {
            return Select(t => t.OrgID.StartsWith(orgid) && t.OrgType == orgtype && t.StoreOrSectionID.HasValue);
        }

        public bool Create(OPC_OrgInfo entity)
        {
            entity.IsDel = false;
            return base.Create(entity);
        }

        public PageResult<OPC_OrgInfo> GetAll(int pageIndex, int pageSize)
        {
            return Select2<OPC_OrgInfo, string>(t => t.IsDel == false, o => o.OrgID, true, pageIndex, pageSize);
        }


        public OPC_OrgInfo Add(OPC_OrgInfo orgInfo)
        {
            using (var db = new YintaiHZhouContext())
            {
                if (orgInfo != null)
                {

                    var lst = db.OPC_OrgInfos.Where(t => t.ParentID == orgInfo.ParentID).OrderByDescending(t => t.OrgID);
                    var e = lst.FirstOrDefault();
                    if (e == null)
                    {
                        orgInfo.OrgID = orgInfo.ParentID + "001";
                    }
                    else
                    {
                        int d = int.Parse(e.OrgID);
                        orgInfo.OrgID = (d + 1).ToString();
                    }
                    orgInfo.IsDel = false;
                    var a = db.OPC_OrgInfos.Add(orgInfo);
                    db.SaveChanges();
                    return a;
                }
                return null;
            }
        }


        public OPC_OrgInfo GetByOrgID(string orgID)
        {
            return Select(t => t.OrgID == orgID).FirstOrDefault();
        }



        public PagerInfo<OPC_OrgInfo> GetPagedList(PagerRequest pagerRequest, List<string> startsWith, EnumOrgType? type)
        {
            var filter = OPC_OrgInfoFiller(startsWith, type);
            using (var db = new YintaiHZhouContext())
            {
                var orginfos = db.OPC_OrgInfos;

                var q = from orginfo in orginfos.AsExpandable().Where(filter)
                        select orginfo;

                var totalCount = q.Count();
                var rst =
                    q.OrderBy(v => v.OrgID)
                        .Skip(pagerRequest.SkipCount)
                        .Take(pagerRequest.PageSize);

                return new PagerInfo<OPC_OrgInfo>(pagerRequest, totalCount, rst.ToList());
            }
        }
    }
}