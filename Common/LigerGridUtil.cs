#region 文件描述

// 描述：该类主要是LigerUI的序列化类
// 作者：苏志平
// 时间：2012-06-13 11:40

#endregion

#region 类修改记录 : 每次修改一组描述

// 修改描述：
// 修 改 人：
// 修改时间：

#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using System.Web;
using System.Data.Common; 
using MySql.Data.MySqlClient;

namespace Common
{
    /// <summary>
    ///LigerGridUtil 的摘要说明
    /// </summary>
    public class LigerGridUtil
    {
        public LigerGridUtil()
        {
            OracleParamsList = new Dictionary<string, object>();
        }

        private string executSQL;
        private List<MySqlParameter> mySqlParameterList = new List<MySqlParameter>();

        public List<MySqlParameter> MySqlParameterList
        {
            get
            {
                if (mySqlParameterList == null)
                {
                    mySqlParameterList = new List<MySqlParameter>();
                }
                return mySqlParameterList;
            }
            set { mySqlParameterList = value; }
        }

        public LigerGridUtil(string sql)
            : this()
        {
            this.executSQL = sql;
        }

        public Dictionary<string, object> OracleParamsList
        {
            get;
            set;

        }

        public static string GetJson(string sql)
        {
            return GetJson(sql, null);
        }
        public static string GetJson(string sql, List<MySqlParameter> list)
        {
            LigerGridUtil lgu = new LigerGridUtil(sql);
            if (list != null)
            {
                lgu.MySqlParameterList = list;
            }
            return lgu.GetJson();
        }
        public string GetJson()
        {
            string orderBy = "";
            if (!string.IsNullOrEmpty(SortFiledName))
            {
                orderBy = string.Format("order by {0} {1}", SortFiledName, SortOrder);
            }


            string sql = string.Format(@"
select a.*,null as  row__status
from 
({0}) a
{1}
LIMIT {2} , {3}", this.ExecutSQL, orderBy, (PageIndex - 1) * PageSize, PageSize);

            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(new JavaScriptConverter[] { new DataRowConverter(), new DataTableConverter() });
            DataSet ds = MySqlHelper.ExecuteDataset(CommandType.Text, sql, MySqlParameterList.ToArray());
            string jssStr = jss.Serialize(ds.Tables[0]);
            jssStr = jssStr.Substring(0, jssStr.Length - 1);
            return string.Format("{0},\"Total\":{1}}}", jssStr, RecordCount);
        }


        public int PageIndex
        {
            get
            {
                int? page = RquestFormIntValue("page");
                return page.HasValue ? page.Value : 1;
            }
        }
        private int? RquestFormIntValue(string keyName)
        {
            if (HttpContext.Current == null || string.IsNullOrEmpty(HttpContext.Current.Request.Form[keyName]))
            {
                return null;
            }
            int i = 0;
            if (int.TryParse(HttpContext.Current.Request.Form[keyName], out i))
            {
                return i;
            }
            return null;
        }
        public int PageSize
        {
            get
            {
                int? pagesize = RquestFormIntValue("pagesize");
                return pagesize.HasValue ? pagesize.Value : 20;
            }
        }

        public string SortFiledName
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context == null)
                {
                    return null;
                }
                return context.Request.Form["sortname"];
            }
        }

        public string SortOrder
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context == null)
                {
                    return null;
                }
                return context.Request.Form["sortorder"].ToLower() == "asc" ? "asc" : "desc";
            }
        }

        /// <summary>
        /// 根据json字符串获取到DataTable
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static DataTable GetTable(string json)
        {

            return null;
        }

        public int RecordCount
        {
            get
            {
                string sql = string.Format(@"
select count(*) as row_count from ({0}) t1", ExecutSQL);
                return Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, sql, MySqlParameterList.ToArray()));
            }
        }


        /// <summary>
        /// 要执行的SQL
        /// </summary>
        public string ExecutSQL
        {
            get
            {
                return executSQL;
            }
            set
            {
                this.executSQL = value;
            }
        }
    }
}
