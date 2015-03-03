using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Intime.OPC.Domain.Base;
using Intime.OPC.Domain.Models;

namespace Intime.OPC.Repository.Base
{
    public abstract class OPCBaseRepository<TKey, TEntity> : BaseRepository<TEntity>, IOPCRepository<TKey, TEntity> where TEntity : class ,IEntity
    {
        public DbContext DbContext()
        {
            return GetYintaiHZhouContext();
        }

        public YintaiHZhouContext GetYintaiHZhouContext()
        {
            return new YintaiHZhouContext();
        }

        protected virtual DbQuery<TEntity> DbQuery(DbContext context)
        {
            return context.Set<TEntity>();
            //return context.Set<Section>().Include("Brand");
        }

        /// <summary>
        /// 帮助方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public T Func<T>(Func<DbContext, T> func)
        {
            using (var c = DbContext())
            {
                return func(c);
            }
        }

        public void Action(Action<DbContext> action)
        {
            using (var c = DbContext())
            {
                action(c);
            }
        }

        public virtual void Delete(TKey id)
        {
            Action(v => EFHelper.Delete<TKey, TEntity>(v, id));
        }

        void IOPCRepository<TKey, TEntity>.Update(TEntity entity)
        {
            Update(entity);
        }

        /// <summary>
        /// 受保护的方法
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            Action(v => EFHelper.Update(v, entity));
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return Func(v => EFHelper.Insert(v, entity));
        }

        public void InsertOrUpdate(List<TEntity> entity)
        {
            Action(v => EFHelper.InsertOrUpdate(v, entity.ToArray()));
        }

        public virtual List<TEntity> FindAll()
        {
            return Func(v => EFHelper.FindAll<TEntity>(v).ToList());
        }

        public virtual TEntity GetItem(TKey key)
        {
            return Func(v => EFHelper.FindOne<TEntity>(DbQuery(v), key));
        }

        public abstract IEnumerable<TEntity> AutoComplete(string query);
    }
}