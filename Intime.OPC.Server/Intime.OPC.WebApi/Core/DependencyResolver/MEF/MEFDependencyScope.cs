using System;
using System.Collections.Generic;
using System.Composition;
using System.Web.Http.Dependencies;

namespace Intime.OPC.WebApi.Core.DependencyResolver.MEF
{
    /// <summary>
    ///     Implements IDependencyScope over a MEF lightweight container.
    /// </summary>
    public class MefDependencyScope : IDependencyScope
    {
        private readonly Export<CompositionContext> _compositionScope;

        /// <summary>
        ///     Construct a dependency scope for the composition context accessed
        ///     using the <see cref="CompositionContext" />.
        /// </summary>
        /// <param name="compositionScope">A handle to the export provider to use.</param>
        public MefDependencyScope(Export<CompositionContext> compositionScope)
        {
            if (compositionScope == null) throw new ArgumentNullException("compositionScope");
            _compositionScope = compositionScope;
        }

        /// <summary>
        ///     The composition scope.
        /// </summary>
        protected CompositionContext CompositionScope
        {
            get { return _compositionScope.Value; }
        }

        #region IDependencyScope Members

        /// <summary>
        ///     Release the scope.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        ///     Get the service represented by <paramref name="serviceType" /> from the
        ///     scope.
        /// </summary>
        /// <param name="serviceType">The service to retrieve.</param>
        /// <returns>The service instance; or null.</returns>
        public object GetService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");

            object result;
            CompositionScope.TryGetExport(serviceType, null, out result);
            return result;
        }

        /// <summary>
        ///     Get all services represented by <paramref name="serviceType" /> from
        ///     the scope.
        /// </summary>
        /// <param name="serviceType">The service type to retrieve.</param>
        /// <returns>All instances of the service type.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException("serviceType");

            return CompositionScope.GetExports(serviceType, null);
        }

        #endregion

        /// <summary>
        ///     Release the scope.
        /// </summary>
        /// <param name="disposing">
        ///     If the scope is being disposed, true; otherwise
        ///     the scope is undergoing finalization.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
                _compositionScope.Dispose();
        }
    }
}