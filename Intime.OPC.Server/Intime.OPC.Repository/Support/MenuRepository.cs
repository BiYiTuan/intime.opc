// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-23-2014 11:38:21
//
// Last Modified By : Liuyh
// Last Modified On : 03-27-2014 23:17:01
// ***********************************************************************
// <copyright file="MenuRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    /// <summary>
    /// Class MenuRepository.
    /// </summary>
    public class MenuRepository : BaseRepository<OPC_AuthMenu>, IMenuRepository
    {
        #region IMenuRepository Members

        /// <summary>
        /// Gets the menus by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>IList{OPC_AuthMenu}.</returns>
        public IList<OPC_AuthMenu> GetMenusByUserID(int userId)
        {
            using (var db = new YintaiHZhouContext())
            {
                //return
                //    db.OPC_AuthMenu.Join(
                //        db.OPC_AuthRoleMenu.Join(db.OPC_AuthRoleUser.Where(u => u.OPC_AuthUserId == userId).Distinct(),
                //            arm => arm.OPC_AuthRoleId, aru => aru.OPC_AuthRoleId, (arm, aru) => arm), m => m.Id,
                //        arm => arm.OPC_AuthMenuId, (m, arm) => m).OrderBy(t=>t.Sort).ToList();



                var lstRole = db.OPC_AuthRoleUsers.Where(u => u.OPC_AuthUserId == userId).Select<OPC_AuthRoleUser,int>(t=>t.OPC_AuthRoleId).Distinct();
                var lstMenu =
                    db.OPC_AuthRoleMenus.Where(t => lstRole.Contains(t.OPC_AuthRoleId))
                        .Select<OPC_AuthRoleMenu, int>(t => t.OPC_AuthMenuId).Distinct();
               return  db.OPC_AuthMenus.Where(t => lstMenu.Contains(t.Id)).Distinct().ToList();

            }
        }

        /// <summary>
        /// Gets the menus by role identifier.
        /// </summary>
        /// <param name="roleID">The role identifier.</param>
        /// <returns>IList{OPC_AuthMenu}.</returns>
        public IList<OPC_AuthMenu> GetMenusByRoleID(int roleID)
        {
            using (var db = new YintaiHZhouContext())
            {
                return db.OPC_AuthRoleMenus.Where(t => t.OPC_AuthRoleId == roleID).Join(
                    db.OPC_AuthMenus,
                    t => t.OPC_AuthMenuId,
                    o => o.Id, (arm, aru) => aru).Distinct().OrderBy(t=>t.Sort).ToList();
            }
        }

        /// <summary>
        /// Gets the menu list.
        /// </summary>
        /// <returns>IList{OPC_AuthMenu}.</returns>
        public IList<OPC_AuthMenu> GetMenuList()
        {
            return Select(t => t.Id != t.PraentMenuId).OrderBy(t=>t.Sort).ToList();
        }

        #endregion
    }
}