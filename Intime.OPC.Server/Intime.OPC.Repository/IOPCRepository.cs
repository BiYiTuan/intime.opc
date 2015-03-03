using System.Collections.Generic;

namespace Intime.OPC.Repository
{
    public interface IOPCRepository<in TKey, TEntity> where TEntity : class
    {
        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id">根据ID来删除</param>
        void Delete(TKey id);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">要插入的实体</param>
        /// <returns name="T">返回更新后的实体</returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">要插入的实体</param>
        /// <returns name="T">返回更新后的实体</returns>
        void InsertOrUpdate(List<TEntity> entity);

        /// <summary>
        /// 全部
        /// </summary>
        /// <returns></returns>
        List<TEntity> FindAll();

        /// <summary>
        /// 查找key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity GetItem(TKey key);

        /// <summary>
        /// Autocomplete interface
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<TEntity> AutoComplete(string query);
    }
}