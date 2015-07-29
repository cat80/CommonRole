using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace Common
{
    /// <summary>
    /// 页面的ajax请求通用类
    /// </summary>
    public class FormAjaxDetails
    {
        /// <summary>
        /// 获取明细的json
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="pars">参数</param>
        /// <returns></returns>
        public static string GetDetailsJson(string sql, Dictionary<string, object> pars)
        {
            List<MySqlParameter> sqlPars = new List<MySqlParameter>();
            if (pars != null)
            {
                foreach (var item in pars)
                {
                    sqlPars.Add(new MySqlParameter(item.Key, item.Value));
                }
            }
            DataSet ds = MySqlHelper.ExecuteDataset(CommandType.Text, sql, sqlPars.ToArray());
            List<object> list = new List<object>();
            if (ds.Tables.Count > 0)
            {
                DataTable tab = ds.Tables[0];
                if (tab.Rows.Count > 0)
                {
                    foreach (DataColumn item in tab.Columns)
                    {
                        list.Add(new
                        {
                            text = item.ColumnName,
                            value = tab.Rows[0][item]
                        });
                    }
                }
                //text value

            }
            return JSON.GetJson(list);
        }
    }
}
