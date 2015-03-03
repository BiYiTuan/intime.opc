// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-19-2014 20:11:42
//
// Last Modified By : Liuyh
// Last Modified On : 03-21-2014 01:06:24
// ***********************************************************************
// <copyright file="RoleRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Exception;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    /// <summary>
    ///     Class RoleRepository.
    /// </summary>
    public class RoleRepository : BaseRepository<OPC_AuthRole>, IRoleRepository
    {
        #region IRoleRepository Members

        /// <summary>
        ///     Sets the enable.
        /// </summary>
        /// <param name="roleID">The role identifier.</param>
        /// <param name="enable">if set to <c>true</c> [enable].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SetEnable(int roleID, bool enable)
        {
            using (var db = new YintaiHZhouContext())
            {
                OPC_AuthRole ent = db.OPC_AuthRoles.FirstOrDefault(t => t.Id == roleID);
                if (ent != null)
                {
                    if (ent.IsSystem)
                    {
                        throw new Exception("超级管理员不能删除");
                    }
                    ent.IsValid = enable;
                    db.SaveChanges();

                    return true;
                }
                return false;
            }
        }

        public bool Delete(int id)
        {
            OPC_AuthRole d = base.GetByID(id);
            if (d == null)
            {
                if (d.IsSystem)
                {
                    throw new Exception("超级管理员不能删除");
                }
                return base.Delete(id);
            }
            throw new RoleNotExistsExcepion(id);
        }

        public PageResult<OPC_AuthRole> All(int pageIndex, int pageSize = 20)
        {
            return Select(t => t.IsValid && !t.IsSystem, t => t.UpdateDate, false, pageIndex, pageSize);
        }

        public PageResult<OPC_AuthRole> GetAll(int pageIndex, int pageSize)
        {
            return Select2<OPC_AuthRole, int>(t => t.IsSystem == false, t => t.Id, true, pageIndex, pageSize);
        }

        public PageResult<OPC_AuthRole> GetByUserID(int userID, int pageIndex, int pageSize = 20)
        {
            using (var db = new YintaiHZhouContext())
            {
                return db.OPC_AuthRoleUsers.Where(t => t.OPC_AuthUserId == userID).Join(db.OPC_AuthRoles,
                    t => t.OPC_AuthUserId, o => o.Id, (t, o) => o).ToPageResult(pageIndex, pageSize);
            }
        }


        /// <summary>
        /// 根据用户获取角色列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>角色列表</returns>
        public IEnumerable<OPC_AuthRole> GetRolesByUserId(int userId)
        {
            using (var db = new YintaiHZhouContext())
            {
                return db.OPC_AuthRoleUsers
                    .Where(a => a.OPC_AuthUserId == userId)
                    .Join(db.OPC_AuthRoles,
                    t => t.OPC_AuthRoleId,
                    o => o.Id,
                    (t, o) => o).ToList();
            }
        }

        #endregion
    }
}