using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Infrastructure.REST;
using Intime.OPC.Infrastructure.Service;

namespace Intime.OPC.Infrastructure.Service
{
    /// <summary>
    /// Service interface for CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T>
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="obj">object to create</param>
        T Create(T obj);

        /// <summary>
        /// Create model by passsing another type of data
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        T Create<TData>(TData data);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="obj">object to update</param>
        T Update(T obj);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="data"></param>
        void Update<TData>(T obj, TData data);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">object id</param>
        void Delete(int id);

        /// <summary>
        /// Delete by unique ID
        /// </summary>
        /// <param name="uniqueID">unique ID</param>
        void Delete(string uniqueID);

        /// <summary>
        /// Query by ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>Object with specifier type</returns>
        T Query(int id);

        /// <summary>
        /// Query by unique ID
        /// </summary>
        /// <param name="uniqueID">unique ID</param>
        /// <returns>Object with specifier type</returns>
        T Query(string uniqueID);

        /// <summary>
        /// Query
        /// </summary>
        /// <returns>Object collection</returns>
        PagedResult<T> Query(IQueryCriteria queryCriteria);

        /// <summary>
        /// Query all
        /// </summary>
        /// <param name="queryCriteria"></param>
        /// <returns></returns>
        IList<T> QueryAll(IQueryCriteria queryCriteria);
    }
}
