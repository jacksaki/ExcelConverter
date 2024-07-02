using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelConverter.Extensions
{
    public class SqlResultRowCollection : IEnumerable<SqlResultRow>
    {
        private List<SqlResultRow> _rows = null;
        public SqlResult Parent { get; }

        public SqlResultRowCollection(IDataReader dr, SqlResult parent)
        {
            this.Parent = parent;
            _rows = new List<SqlResultRow>();
            while (dr.Read())
            {
                _rows.Add(new SqlResultRow(dr, this));
            }
        }

        public IEnumerator<SqlResultRow> GetEnumerator()
        {
            return ((IEnumerable<SqlResultRow>)this._rows).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this._rows).GetEnumerator();
        }
    }
}