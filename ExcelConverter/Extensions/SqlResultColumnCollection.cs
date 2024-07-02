using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExcelConverter.Extensions
{
    public class SqlResultColumnCollection : IEnumerable<SqlResultColumn>
    {
        public int Count { get; }
        public SqlResult Parent { get; }

        private List<SqlResultColumn> _columns = null;
        public SqlResultColumnCollection(IDataReader dr, SqlResult parent)
        {
            this.Parent = parent;
            this.Count = dr.FieldCount;
            _columns = Enumerable.Range(0, dr.FieldCount).Select(x => new SqlResultColumn(dr, x, this)).ToList();
        }
        public IEnumerator<SqlResultColumn> GetEnumerator()
        {
            return ((IEnumerable<SqlResultColumn>)this._columns).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._columns).GetEnumerator();
        }
    }
}