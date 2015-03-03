using System.ComponentModel.Composition;
using System.Collections.Generic;
using Intime.OPC.Infrastructure.Service;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Modules.Finance.Services
{
    [Export(typeof(IService<Bank>))]
    public class BankService : ServiceBase<Bank>
    {
        public override IList<Bank> QueryAll(IQueryCriteria queryCriteria)
        {
            var banks = base.QueryAll(queryCriteria) ?? new List<Bank>();

            banks.Insert(0, new Bank { Code = null, Name = "全部"});

            return banks;
        }
    }
}
