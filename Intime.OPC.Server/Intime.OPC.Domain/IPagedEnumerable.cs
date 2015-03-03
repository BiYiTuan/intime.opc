using System.Collections.Generic;

namespace Intime.OPC.Domain
{
    /// <summary>
    /// 可分页迭代器
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public interface IPagedEnumerable<out T> : IEnumerable<T>, IPagedable
    {
    }
}