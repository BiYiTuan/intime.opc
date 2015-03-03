using Intime.OPC.Infrastructure;
using Intime.OPC.Infrastructure.Rest;
using Intime.OPC.Infrastructure.REST;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPCAPP.Common.Extensions;
using Intime.OPC.Domain.Attributes;

namespace Intime.OPC.Infrastructure.Service
{
    public abstract class ServiceBase<TModel> : IService<TModel>, IAddtionalService, IPartImportsSatisfiedNotification
        where TModel : Intime.OPC.Domain.Models.Model
    {
        private IRestClient _restClient;
        private string _uriName;

        [Import]
        public TokenManager TokenManager { get; set; }

        [Import]
        private IRestClientFactory RestClientFactory { get; set; }

        public string UriName
        {
            get { return _uriName; }
        }

        public virtual void Initialize()
        {
            _restClient = RestClientFactory.Create(AppEx.Config.ServiceUrl, AppEx.Config.UserKey, AppEx.Config.Password, TokenManager.Token);

            var attribute = typeof(TModel).GetCustomAttribute<UriAttribute>();
            if (attribute == null) throw new InvalidOperationException("Uriattribute not found on the given dimension.");

            _uriName = attribute.Name;
        }

        public void OnImportsSatisfied()
        {
            Initialize();
        }

        #region Implementation of IService

        public virtual TModel Create(TModel obj)
        {
            return _restClient.Post<TModel>(_uriName, obj);
        }

        public TModel Create<TData>(TData data)
        {
            return _restClient.Post<TModel,TData>(_uriName, data);
        }

        public virtual TModel Update(TModel obj)
        {
            return _restClient.Put<TModel>(string.Format("{0}/{1}", _uriName, obj.Id), obj);
        }

        public virtual void Update<TData>(TModel obj, TData data)
        {
            _restClient.PutWithoutReturn<TData>(string.Format("{0}/{1}", _uriName, obj.Id), data);
        }

        public virtual void Delete(int id)
        {
            Delete(id.ToString());
        }

        public virtual void Delete(string uniqueID)
        {
            _restClient.Delete(string.Format("{0}/{1}", _uriName, uniqueID));
        }

        public virtual TModel Query(int id)
        {
            return Query(id.ToString());
        }

        public virtual TModel Query(string uniqueID)
        {
            return _restClient.Get<TModel>(string.Format("{0}/{1}", _uriName, uniqueID));
        }

        public virtual PagedResult<TModel> Query(IQueryCriteria queryCriteria)
        {
            return _restClient.Get<PagedResult<TModel>>(string.Format("{0}?{1}", _uriName, queryCriteria.BuildQueryString()));
        }

        public virtual IList<TModel> QueryAll(IQueryCriteria queryCriteria)
        {
            if (queryCriteria == null) throw new ArgumentNullException("queryCriteria");

            queryCriteria.PageIndex = 1;
            
            var pagedResult = _restClient.Get<PagedResult<TModel>>(string.Format("{0}?{1}", _uriName, queryCriteria.BuildQueryString()));
            if (pagedResult.TotalCount == pagedResult.Data.Count) return pagedResult.Data;

            var result = new List<TModel>(pagedResult.Data);
            var pages = pagedResult.TotalCount / pagedResult.PageSize + (pagedResult.TotalCount % pagedResult.PageSize == 0 ? 0: 1);

            for (int i = pagedResult.PageIndex; i < pages; i++)
            {
                queryCriteria.PageIndex++;
                pagedResult = _restClient.Get<PagedResult<TModel>>(string.Format("{0}?{1}", _uriName, queryCriteria.BuildQueryString()));
                result.AddRange(pagedResult.Data);
            }

            return result;
        }

        #endregion

        #region Implementation of IAddtionalService

        public void Update<TData>(string uri, TData data)
        {
            _restClient.PutWithoutReturn<TData>(uri, data);
        }

        public void Update(string uri)
        {
            _restClient.PutWithoutReturn(uri);
        }

        #endregion
    }
}
