using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Intime.OPC.Domain;

namespace System.Linq
{
    public static class QueryableEx
    {
        public static PageResult<T> ToPageResult<T>(this IQueryable<T> source, int pageIndex, int pageSize = 20)
        {
            pageIndex = pageIndex - 1;
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            var lst = source.Skip(pageIndex * pageSize).Take(pageSize).ToList();
            int count = source.Count();

            return new PageResult<T>(lst, count);
        }

        /// <summary>
        /// TO PAGE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pagerRequest"></param>
        /// <returns></returns>
        public static PagerInfo<T> ToPagerInfo<T>(this IQueryable<T> source, PagerRequest pagerRequest)
        {
            var count = source.Count();

            var data = source.Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).ToList();


            return new PagerInfo<T>(pagerRequest, count)
            {
                Datas = data,
            };
        }


        ///// <summary>
        ///// TO PAGE
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="source"></param>
        ///// <param name="pagerRequest"></param>
        ///// <param name="func">convert</param>
        ///// <returns></returns>
        //public static PagerInfo<T> ToPagerInfo<T>(this IQueryable<T> source, PagerRequest pagerRequest, Func<dynamic, T> func)
        //{
        //    var count = source.Count();

        //    var data = source.Skip(pagerRequest.SkipCount).Take(pagerRequest.PageSize).Select(s => func(s)).ToList();


        //    return new PagerInfo<T>(pagerRequest, count)
        //    {
        //        Datas = data,
        //    };
        //}
    }
}
