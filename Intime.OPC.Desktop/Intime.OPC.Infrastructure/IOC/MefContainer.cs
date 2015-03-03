using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace Intime.OPC.Infrastructure
{
    public class MefContainer : IContainer
    {
        private readonly CompositionContainer _container;

        public MefContainer(AggregateCatalog aggCatalog)
        {
            _container = new CompositionContainer(aggCatalog);
        }

        public MefContainer(CompositionContainer container)
        {
            _container = container;
        }

        public IEnumerable<T> GetInstances<T>()
        {
            return _container.GetExportedValues<T>();
        }

        public T GetInstance<T>()
        {
            return _container.GetExportedValueOrDefault<T>();
        }

        public T GetInstance<T>(string key)
        {
            return _container.GetExportedValue<T>(key);
        }

        public void Dispose()
        {
            if (_container != null)
                _container.Dispose();
        }
    }
}