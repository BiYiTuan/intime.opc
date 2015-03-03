using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Intime.OPC.Common.Extensions
{
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<TResult>(this IEnumerable<TResult> value) where TResult : class
        {
            //创建属性的集合
            var pList = new List<PropertyInfo>();
            Type type = typeof (TResult);
            var dt = new DataTable();
            Array.ForEach(type.GetProperties(), p =>
            {
                pList.Add(p);
                dt.Columns.Add(p.Name, p.PropertyType);
            });
            try
            {
                if (value != null)
                {
                    foreach (TResult item in value)
                    {
                        //创建一个DataRow实例
                        DataRow row = dt.NewRow();
                        pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                        dt.Rows.Add(row);
                    }
                }
            }
            catch
            {
            }
            return dt;
        }
    }
}