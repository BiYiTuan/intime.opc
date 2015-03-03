// ***********************************************************************
// Assembly         : 02_Intime.OPC.Service
// Author           : Liuyh
// Created          : 04-02-2014 19:58:35
//
// Last Modified By : Liuyh
// Last Modified On : 04-02-2014 20:03:16
// ***********************************************************************
// <copyright file="OrgService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Enums;
using Intime.OPC.Domain.Extensions;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;

namespace Intime.OPC.Service.Support
{
    /// <summary>
    /// Class OrgService.
    /// </summary>
    public class OrgService : BaseService<OPC_OrgInfo>, IOrgService
    {



        /// <summary>
        /// Initializes a new instance of the <see cref="OrgService"/> class.
        /// </summary>
        /// <param name="orgInfoRepository">The org information repository.</param>
        public OrgService(IOrgInfoRepository orgInfoRepository):base(orgInfoRepository)
        {
        }

        public PageResult<OPC_OrgInfo> GetAll(int pageIndex, int pageSize = 20)
        {
            return _repository.GetAll(pageIndex, pageSize);
        }

        public OPC_OrgInfo AddOrgInfo(OPC_OrgInfo orgInfo)
        {
            return ((IOrgInfoRepository) _repository).Add(orgInfo);
        }


        public OPC_OrgInfo GetOrgInfoByOrgID(string orgID)
        {
            return ((IOrgInfoRepository) _repository).GetByOrgID(orgID);
        }

        public PagerInfo<OPC_OrgInfo> GetPagedList(PagerRequest pagerRequest, List<string> startsWithOrgId, EnumOrgType? orgType)
        {
            var list = ((IOrgInfoRepository)_repository).GetPagedList(pagerRequest,startsWithOrgId, orgType);

            return list;
        }


        public OPC_OrgInfo Get(int id)
        {
            return ((IOrgInfoRepository) _repository).GetByID(id);
        }
    }
}