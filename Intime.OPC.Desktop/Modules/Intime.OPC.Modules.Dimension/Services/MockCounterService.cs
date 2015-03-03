using Intime.OPC.Modules.Dimension.Common;
using Intime.OPC.Modules.Dimension.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Modules.Dimension.Services.Imp
{
    public class MockCounterService : IService<Counter>
    {
        private IList<Counter> counters = new List<Counter>();

        public MockCounterService()
        {
            BuildMockData();
        }

        private void BuildMockData()
        {
            for (int i = 1; i < 100; i++)
            {
                var counter = new Counter()
                {
                    ID = i,
                    Name = string.Format("Name {0}", i),
                    Code = string.Format("Code {0}", i),
                    AreaCode = string.Format("AreaCode {0}", i),
                    ContactPhoneNumber = string.Format("ContactPhoneNumber {0}", i),
                    Repealed = (i % 2) == 0,
                    Organization = new Organization() { ID = i, Name = string.Format("Organization Name {0}", i) },
                    Brands = new List<Brand>()
                };

                for (int j = 1; j < 100; j++)
                {
                    counter.Brands.Add(new Brand
                    {
                        ID = j,
                        Name = string.Format("Name {0}", j),
                        EnglishName = string.Format("English Name {0}", j),
                        Enabled = (j % 2) == 0,
                        Supplier = new Supplier() { ID = j, Name = string.Format("Supplier Name {0}", j) },
                        Description = string.Format("Description {0}", j)
                    });
                }

                counters.Add(counter);
            }
        }

        public Models.Counter Create(Models.Counter obj)
        {
            obj.ID = 1000;

            return obj;
        }

        public Models.Counter Update(Models.Counter obj)
        {
            return obj;
        }

        public void Delete(int id)
        {
            var counterToRemove = counters.Where(counter => counter.ID == id).FirstOrDefault();
            counters.Remove(counterToRemove);
        }

        public Models.Counter Query(int id)
        {
            return counters.Where(counter => counter.ID == id).FirstOrDefault();
        }

        public IList<Models.Counter> Query(string name)
        {
            return counters.Where(counter => counter.Name.Contains(name)).ToList();
        }

        public IList<Models.Counter> QueryAll()
        {
            return counters;
        }
    }
}
