using System.ComponentModel.Composition;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Modules.Dimension.Services
{
    using System.Collections.Generic;

    [Export(typeof(IService<Department>))]
    public class DepartmentService : ServiceBase<Department>
    {
        public override IList<Department> QueryAll(IQueryCriteria queryCriteria)
        {
            var departments = base.QueryAll(queryCriteria) ?? new List<Department>();
            departments.Insert(0, new Department {Id =-1, Name = "全部"});

            return departments;
        }
    }
}
