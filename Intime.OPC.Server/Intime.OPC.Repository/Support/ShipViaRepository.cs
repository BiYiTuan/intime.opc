// ***********************************************************************
// Assembly         : 01_Intime.OPC.Repository
// Author           : Liuyh
// Created          : 03-28-2014 21:09:22
//
// Last Modified By : Liuyh
// Last Modified On : 03-28-2014 21:09:50
// ***********************************************************************
// <copyright file="ShipViaRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository.Base;

namespace Intime.OPC.Repository.Support
{
    /// <summary>
    /// Class ShipViaRepository.
    /// </summary>
    public class ShipViaRepository : BaseRepository<ShipVia>, IShipViaRepository
    {
        public PageResult<ShipVia> GetAll(int pageIndex, int pageSize = 20)
        {

           return Select(t => t.Status == 1,t=>t.UpdateDate,false,pageIndex,pageSize);
            
        }
    }
}