// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-28-2014 21:08:09
//
// Last Modified By : Liuyh
// Last Modified On : 03-28-2014 21:08:21
// ***********************************************************************
// <copyright file="IShipViaRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository
{
    /// <summary>
    /// Interface IShipViaRepository
    /// </summary>
    public interface IShipViaRepository : IRepository<ShipVia>
    {
        PageResult<ShipVia> GetAll(int pageIndex, int pageSize = 20);

    }
}