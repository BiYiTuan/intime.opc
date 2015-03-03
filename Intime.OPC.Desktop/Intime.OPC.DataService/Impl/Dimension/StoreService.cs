using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Intime.OPC.DataService.Interface;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.DataService;

namespace Intime.OPC.DataService.Impl.Info
{
    [Export(typeof (IStoreDataService))]
    public class StoreDataService : IStoreDataService
    {
        public bool SetIsStop(int StoreId, bool isStop)
        {
            throw new NotImplementedException();
        }

        public ResultMsg Add(Store model)
        {
            throw new NotImplementedException();
        }

        public ResultMsg Edit(Store model)
        {
            throw new NotImplementedException();
        }

        public ResultMsg Delete(Store model)
        {
            throw new NotImplementedException();
        }

        public PageResult<Store> Search(IDictionary<string, object> iDicFilter)
        {
            throw new NotImplementedException();
        }
    }
}