using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExcelConverter.Extensions
{
    public class SqlResultColumn
    {
        public SqlResultColumnCollection Parent { get; }
        public string ColumnName { get; }
        public int ColumnIndex { get; }
        public Type DataType { get; }
        internal SqlResultColumn(IDataReader dr, int index, SqlResultColumnCollection parent)
        {
            this.ColumnName = dr.GetName(index);
            this.ColumnIndex = index;
            this.DataType = dr.GetFieldType(index);
            this.Parent = parent;
        }
    }
}
