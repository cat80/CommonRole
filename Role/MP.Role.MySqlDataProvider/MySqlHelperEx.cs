using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace MP.Role.MySqlDataProvider
{
    public partial class MySqlHelper
    {
        #region internal static IDataReader GetDataReaderBySql(int pageSize, int pageIndex, out int recordCount, string sql, string sortWay, params MySqlParameter[] pars)  /// 分页查询
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageIndex">当前页索引</param>
        /// <param name="recordCount">记录条数</param>
        /// <param name="sql">查询sql</param>
        /// <param name="sortWay">排序方式</param>
        /// <param name="pars">参数代查询参数</param>
        /// <returns></returns>
        internal static IDataReader GetDataReaderBySql(int pageSize, int pageIndex, out int recordCount, string sql, string sortWay, params MySqlParameter[] pars)
        {
            if (pageSize < 1)
            {
                pageSize = 20;
            }
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (!string.IsNullOrEmpty(sortWay))
            {
                sortWay = "order by " + sortWay + " ";
            }

            string querySql = string.Format(@"

{0} {1}
LIMIT {2} , {3}", sql, sortWay, (pageIndex - 1) * pageSize, pageSize);
            string countSql = string.Format(@"
select count(*) as row_count from ({0}) t1", sql);
            recordCount = Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, countSql, pars));
            return MySqlHelper.ExecuteReader(CommandType.Text, querySql, pars);
        }

        internal static List<T> GetPagerList<T>(int pageSize, int pageIndex, out int recordCount, string sql, string sortWay, Func<IDataReader, T> convert, params MySqlParameter[] pars)
        {
            List<T> list = new List<T>();

            using (IDataReader reader = GetDataReaderBySql(pageSize, pageIndex, out recordCount, sql, sortWay, pars))
            {
                while (reader.Read())
                {
                    list.Add(convert(reader));
                }
            } 
            return list;
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion


    }
}
