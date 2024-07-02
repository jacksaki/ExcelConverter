using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ExcelConverter.Extensions
{
    public interface IDbQuery : IDisposable
    {
        IDbConnection Connection
        {
            get;
        }
        SqlResultRow GetFirstRow(string sql, IDictionary<string, object> param);
        IDbTransaction BeginTransaction();
        SqlResult GetSqlResult(string sql, IDictionary<string, object> param);
        SqlResult GetSqlResult(string sql, IDictionary<string, object> param, int? fetchRowSize);
        int ExecuteNonQuery(string sql, IDictionary<string, object> param);
        object ExecuteScalar(string sql, IDictionary<string, object> param);
    }
}