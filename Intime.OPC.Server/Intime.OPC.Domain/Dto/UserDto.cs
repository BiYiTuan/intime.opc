// ***********************************************************************
// Assembly         : 00_Intime.OPC.Domain
// Author           : Liuyh
// Created          : 04-01-2014 22:22:41
//
// Last Modified By : Liuyh
// Last Modified On : 04-01-2014 22:24:15
// ***********************************************************************
// <copyright file="UserDto.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace Intime.OPC.Domain.Dto
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserDto"/> class.
        /// </summary>
        public UserDto()
        {
            SectionIds = new List<int>();
            StoreIds = new List<int>();
            OrgIds = new List<string>();
        }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否为管理员
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 专柜列表
        /// </summary>
        public IList<int> SectionIds { get; set; }

        /// <summary>
        /// 门店列表
        /// </summary>
        /// <value>The store i ds.</value>
        public IList<int> StoreIds { get; set; }

        /// <summary>
        /// 组织IDS，管理员没有组织
        /// </summary>
        public IList<string> OrgIds { get; set; }
    }

    public class ChangePasswordDto
    {
        public int UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}