using System.Collections.Generic;
using Intime.OPC.Domain;
using Intime.OPC.Domain.Models;
using Intime.OPC.Repository;

namespace Intime.OPC.Service.Support
{
    public class BrandService : BaseService<Brand>, IBrandService
    {
        public BrandService(IBrandRepository brandRepository) : base(brandRepository)
        {
        }

        public IList<Brand> GetAll()
        {
            var lst= ((IBrandRepository)_repository).GetAll();
            lst.Insert(0,new Brand(){  Id = -1,Name = "全部"});
            return lst;
        }
    }
}