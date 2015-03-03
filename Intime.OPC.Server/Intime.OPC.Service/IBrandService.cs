
using System.Collections.Generic;
using System.ComponentModel;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Service
{
    public interface IBrandService : IService
    {
        IList<Brand> GetAll();
    }
}
