// ***********************************************************************
// Assembly         : 00_Intime.OPC.Domain
// Author           : Liuyh
// Created          : 03-21-2014 00:04:34
//
// Last Modified By : Liuyh
// Last Modified On : 03-21-2014 00:04:54
// ***********************************************************************
// <copyright file="IEntity.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Intime.OPC.Domain.Base
{
    /// <summary>
    ///     Interface IEntity
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; set; }
    }
}