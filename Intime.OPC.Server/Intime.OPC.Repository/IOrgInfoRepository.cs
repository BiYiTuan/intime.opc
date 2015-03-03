using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    public interface IOrgInfoRepository : IRepository<OPC_OrgInfo>
    {
        /// <summary>
        /// 获得某个机构的所有指定类型的下属机构
        /// </summary>
        /// <param name="orgid">The orgid.</param>
        /// <param name="orgtype">The orgtype.</param>
        /// <returns>IList{OPC_OrgInfo}.</returns>
        IList<OPC_OrgInfo> GetByOrgType(string orgid, int orgtype);

        PageResult<OPC_OrgInfo> GetAll(int pageIndex, int pageSize);

        OPC_OrgInfo Add(OPC_OrgInfo orgInfo);

        OPC_OrgInfo GetByOrgID(string orgID);

        PagerInfo<OPC_OrgInfo> GetPagedList(PagerRequest pagerRequest, List<string> startsWith, EnumOrgType? type);
    }
}