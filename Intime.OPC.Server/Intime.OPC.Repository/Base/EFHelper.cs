using Intime.OPC.Domain.Base;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Intime.OPC.Repository.Base
{
    public class EFHelper//<TEntity> where TEntity : class, IEntity
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(EFHelper));

        #region methods

        private static DbSet<TEntity> GetDbSet<TEntity>(DbContext context) where TEntity : class
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            return context.Set<TEntity>();
        }

        #endregion

        #region IEfRepository<TEntity>  成员

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public static int Count<TEntity>(DbContext context) where TEntity : class
        {
            return GetDbSet<TEntity>(context).Count();
        }

        #region get

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Get<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return Get(context, filter, null, null);
            //return GetDbSet<TEntity>(context).AsNoTracking().AsExpandable().Where(filter).AsQueryable();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Get<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null) where TEntity : class
        {
            var dbset = GetDbSet<TEntity>(context);

            return Get(dbset, filter, orderBy, take);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dbset"></param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Get<TEntity>(DbQuery<TEntity> dbset, Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int? take = null) where TEntity : class
        {
            var resetSet = filter != null ? dbset.AsExpandable().Where(filter).AsQueryable() : dbset.AsQueryable();

            resetSet = orderBy != null ? orderBy(resetSet).AsQueryable() : resetSet.AsQueryable();

            resetSet = take == null ? resetSet : resetSet.Take(take.Value);

            return resetSet;
        }

        #endregion

        #region getpaged

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">条件</param>
        /// <param name="total">筛选后返回记录数</param>
        /// <param name="index">指定页面索引</param>
        /// <param name="size">指定页面条数</param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetPaged<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50) where TEntity : class
        {
            return GetPaged(context, filter, out total, index, size, null);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">条件</param>
        /// <param name="total">筛选后返回记录数</param>
        /// <param name="index">指定页面索引</param>
        /// <param name="size">指定页面条数</param>
        /// /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetPaged<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null) where TEntity : class
        {
            var dbset = GetDbSet<TEntity>(context);

            return GetPaged(dbset, filter, out total, index, size, null);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="dbset"></param>
        /// <param name="filter">条件</param>
        /// <param name="total">筛选后返回记录数</param>
        /// <param name="index">指定页面索引</param>
        /// <param name="size">指定页面条数</param>
        /// /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetPaged<TEntity>(DbQuery<TEntity> dbset, Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null) where TEntity : class
        {
            var skipCount = (index - 1) * size;
            var resetSet = filter != null ? dbset.AsNoTracking().AsExpandable().Where(filter).AsQueryable() : dbset.AsNoTracking().AsQueryable();

            resetSet = orderBy != null ? orderBy(resetSet).AsQueryable() : resetSet.AsQueryable();
            total = resetSet.Count();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);

            if (Log.IsDebugEnabled)
            {
                Log.Debug(resetSet.ToString());
            }

            return resetSet;
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="total">筛选后返回记录数</param>
        /// <param name="index">指定页面索引</param>
        /// <param name="size">指定页面条数</param>
        /// /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public static IQueryable<TEntity> GetPaged<TEntity>(IQueryable<TEntity> resetSet, out int total, int index = 0, int size = 50, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null) where TEntity : class
        {
            var skipCount = (index - 1) * size;

            resetSet = orderBy != null ? orderBy(resetSet).AsQueryable() : resetSet.AsQueryable();
            total = resetSet.Count();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);

            if (Log.IsDebugEnabled)
            {
                Log.Debug(resetSet.ToString());
            }

            return resetSet;
        }

        #endregion

        /// <summary>
        /// 根据条件分组
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="context"></param>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="grouping"></param>
        /// <returns></returns>
        public static IQueryable<IGrouping<TKey, TEntity>> GetGrouping<TKey, TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                               Func<IQueryable<TEntity>, IQueryable<IGrouping<TKey, TEntity>>> grouping = null) where TEntity : class
        {
            if (grouping == null)
            {
                throw new ArgumentNullException("grouping");
            }

            IQueryable<TEntity> query = GetDbSet<TEntity>(context);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            var result = grouping(query);

            return result.AsQueryable();
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">条件</param>
        /// <param name="total">筛选后返回记录数</param>
        /// <param name="index">指定页面索引</param>
        /// <param name="size">指定页面条数</param>
        /// <param name="specialSkipCount"></param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Get4SpecialSkip<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, int specialSkipCount = 0) where TEntity : class
        {
            var dbset = GetDbSet<TEntity>(context);
            var skipCount = specialSkipCount;
            var resetSet = filter != null ? dbset.AsExpandable().Where(filter).AsQueryable() : dbset.AsQueryable();

            resetSet = orderBy != null ? orderBy(resetSet).AsQueryable() : resetSet.AsQueryable();
            total = resetSet.Count();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);


            return resetSet;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="includeProperties">包含属性</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Get4IncludeProperties<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = GetDbSet<TEntity>(context);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>dbset</returns>
        public IQueryable<TEntity> GetAll<TEntity>(DbContext context) where TEntity : class
        {
            return GetDbSet<TEntity>(context).AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// 查看是否存在某种条件下记录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public bool Contains<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return GetDbSet<TEntity>(context).AsNoTracking().AsExpandable().Any(filter);
        }

        /// <summary>
        /// 根据ID获取一条记录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TEntity FindOne<TEntity>(DbContext context, object id) where TEntity : class,IEntity
        {
            return FindOne(GetDbSet<TEntity>(context), id);
        }

        /// <summary>
        /// 根据ID获取一条记录
        /// </summary>
        /// <param name="dbSet"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static TEntity FindOne<TEntity>(DbQuery<TEntity> dbset, object id) where TEntity : class,IEntity
        {
            var ID = Int32.Parse(id.ToString());
            return dbset.AsQueryable().FirstOrDefault(v => v.Id == ID);
        }

        public static IQueryable<TEntity> FindAll<TEntity>(DbContext context) where TEntity : class
        {
            var dbset = GetDbSet<TEntity>(context);
            return Get(dbset, null);
            //return GetDbSet<TEntity>(context).AsQueryable();
        }

        public static IQueryable<TEntity> FindAll<TEntity>(DbSet<TEntity> dbQuery) where TEntity : class
        {
            return Get(dbQuery, null);
            //return GetDbSet<TEntity>(context).AsQueryable();
        }



        /// <summary>
        /// Find object by keys
        /// </summary>
        /// <param name="context"></param>
        /// <param name="keys">Specified the search keys</param>
        /// <returns>DbSet</returns>
        public static TEntity Find<TEntity>(DbContext context, params object[] keys) where TEntity : class
        {
            return GetDbSet<TEntity>(context).Find(keys);
        }

        /// <summary>
        /// 根据条件查找
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">条件</param>
        /// <returns>T</returns>
        public static TEntity FindOne<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return FindOne(GetDbSet<TEntity>(context), filter);
        }



        /// <summary>
        /// 根据条件查找
        /// </summary>
        /// <param name="dbset"></param>
        /// <param name="filter">条件</param>
        /// <returns>T</returns>
        public static TEntity FindOne<TEntity>(DbSet<TEntity> dbset, Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return dbset.FirstOrDefault(filter);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity">要插入的实体</param>
        /// <returns>T</returns>
        public static TEntity Insert<TEntity>(DbContext context, TEntity entity) where TEntity : class
        {
            var newentity = GetDbSet<TEntity>(context).Add(entity);
            context.SaveChanges();

            return newentity;
        }

        public static IEnumerable<TEntity> Inserts<TEntity>(DbContext context, params TEntity[] entitys) where TEntity : class
        {

            var set = GetDbSet<TEntity>(context);
            var list = new List<TEntity>();
            foreach (var c in entitys)
            {
                var result = set.Add(c);
                list.Add(result);
            }

            context.SaveChanges();

            return list;
        }

        #region delete

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id">根据ID来删除</param>
        public static void Delete<TKey, TEntity>(DbContext context, TKey id) where TEntity : class,IEntity
        {
            var entityToDelete = GetDbSet<TEntity>(context).Find(id);
            Delete(context, entityToDelete);

            //_unitOfWork.Commit();
            context.SaveChanges();
        }

        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityToDelete">要删除的实体</param>
        public static void Delete<TEntity>(DbContext context, TEntity entityToDelete) where TEntity : class,IEntity
        {
            var entry = context.Entry(entityToDelete);
            if (entry.State == EntityState.Detached)
            {
                entityToDelete = FindOne<TEntity>(context, entityToDelete.Id);
            }
            else
            {
                context.Entry(entityToDelete).State = EntityState.Deleted;
            }

            GetDbSet<TEntity>(context).Remove(entityToDelete);
            context.SaveChanges();
            //_unitOfWork.Commit();
        }

        /// <summary>        
        /// 根据条件删除.        
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter">删除条件</param>
        /// <returns>影响行数</returns>
        public static int Delete<TEntity>(DbContext context, Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var objects = Get(context, filter);

            var dbset = GetDbSet<TEntity>(context);

            foreach (var obj in objects)
                dbset.Remove(obj);
            var t = context.SaveChanges();

            //_unitOfWork.Commit();

            return t;
        }


        public static void Delete<TEntity>(DbContext context, List<TEntity> entities) where TEntity : class
        {
            var dbset = GetDbSet<TEntity>(context);
            dbset.RemoveRange(entities);
            context.SaveChanges();
        }

        #endregion


        public static void InsertOrUpdate<TEntity>(DbContext context, TEntity[] entityToUpdate)
            where TEntity : class
        {
            var dbset = GetDbSet<TEntity>(context);
            dbset.AddOrUpdate(entityToUpdate);
            context.SaveChanges();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="context"></param>
        /// <param anme="entityToUpdate">要更新的实体</param>
        public static void Update<TEntity>(DbContext context, TEntity entityToUpdate) where TEntity : class, IEntity
        {

            //dbEntityEntry.State = EntityState.Modified; --- I cannot do this.

            //Ensure only modified fields are updated.
            try
            {
                var dbset = GetDbSet<TEntity>(context);

                dbset.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                //var dbEntityEntry = context.Entry(entityToUpdate);

                //foreach (var property in dbEntityEntry.OriginalValues.PropertyNames)
                //{
                //    var original = originalEntity.OriginalValues.GetValue<object>(property);
                //    var current = dbEntityEntry.CurrentValues.GetValue<object>(property);
                //    if (original != null && !original.Equals(current))
                //        dbEntityEntry.Property(property).IsModified = true;
                //}
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw dbEx;
            }
            catch (Exception ex)
            {

                throw ex;
            }


            //var old = context.Entry(entityToUpdate);
            //if (old.State == EntityState.Detached)
            //{
            //    var entry = Find<TEntity>(context, entityToUpdate.Id);
            //    entityToUpdate = Mapper.Map(entityToUpdate, entry);
            //}

            //GetDbSet<TEntity>(context).Attach(entityToUpdate);
            //context.Entry(entityToUpdate).State = EntityState.Modified;

            //try
            //{
            //    context.SaveChanges();
            //    //_unitOfWork.Commit();
            //    // 写数据库
            //}

            //catch (DbEntityValidationException dbEx)
            //{
            //    if (dbEx.EntityValidationErrors != null)
            //    {
            //        foreach (var err in dbEx.EntityValidationErrors)
            //        {
            //            foreach (var e in err.ValidationErrors)
            //            {
            //                Log.Error(e.PropertyName + ":" + e.ErrorMessage);
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity">实体</param>
        /// <param name="fields">更新字段数组</param>
        public static void UpdateEntityFields<TEntity>(DbContext context, TEntity entity, IEnumerable<string> fields) where TEntity : class
        {
            UpdateEntityFields(context, entity, fields, true);
        }


        /// <summary>
        /// 更新指定字段
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity">实体</param>
        /// <param name="fields">更新字段数组</param>
        /// <param name="isSaveChanges">是否保存修改</param>
        public static void UpdateEntityFields<TEntity>(DbContext context, TEntity entity, IEnumerable<string> fields, bool isSaveChanges) where TEntity : class
        {
            if (entity != null && fields != null)
            {
                var dbset = GetDbSet<TEntity>(context);

                dbset.Attach(entity);
                var setEntry = ((IObjectContextAdapter)context).ObjectContext.
                    ObjectStateManager.GetObjectStateEntry(entity);

                foreach (var t in fields)
                {
                    setEntry.SetModifiedProperty(t);
                }

                if (isSaveChanges)
                {
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        public TEntity Create<TEntity>(DbContext context) where TEntity : class
        {
            var entity = GetDbSet<TEntity>(context).Create();
            context.SaveChanges();

            return entity;
        }

        public static IQueryable<TEntity> GetWithRawSql<TEntity>(DbContext context, string query, params object[] parameters) where TEntity : class
        {
            return GetDbSet<TEntity>(context).SqlQuery(query, parameters).AsQueryable();
        }

        public static IQueryable<TEntity> GetWithDBSql<TEntity>(DbContext context, string query, params object[] parameters)
        {
            return context.Database.SqlQuery<TEntity>(query, parameters).AsQueryable();
        }

        public static int ExecuteSqlCommand(DbContext context, string sql, params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return context.Database.ExecuteSqlCommand(sql);
            }

            return context.Database.ExecuteSqlCommand(sql, parameters);
        }

        #endregion
    }
}
