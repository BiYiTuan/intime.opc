using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intime.OPC.Infrastructure.Excel
{
    /// <summary>
    /// Column definition for exporting to excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ColumnDefinition<T>
    {
        private Tuple<string, Func<T, dynamic>> tuple;

        public ColumnDefinition(string columnName,Func<T,dynamic> propertySelector)
        {
            tuple = new Tuple<string, Func<T, dynamic>>(columnName, propertySelector);
        }

        public string ColumnName
        {
            get { return tuple.Item1; }
        }

        public Func<T, dynamic> PropertySelector
        {
            get { return tuple.Item2; }
        }
    }
}
