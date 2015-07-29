using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace MP.Role.MySqlDataProvider
{
    public partial class MySqlHelper
    {
        #region public 公有级数据成员
        // ----------------------------------------------------------------------------------------------------------
        /// <summary>
        /// dtl_server数据库连接串
        /// </summary>
        private static string _MYSQL_SERVER = String.Empty;

        /// <summary>
        /// dtl_server数据库连接串
        /// </summary>
        public static string MYSQL_SERVER
        {
            get
            {
                if (String.IsNullOrEmpty(_MYSQL_SERVER))
                {
                    lock (_MYSQL_SERVER)
                    {
                        _MYSQL_SERVER = ConfigurationManager.ConnectionStrings["mainDB"].ConnectionString;
                    }
                }
                return _MYSQL_SERVER;
            }
        }

 

        // ----------------------------------------------------------------------------------------------------------
        #endregion


        #region  public static MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) //ExecuteReader
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// ExecuteReader
        /// </summary>
        /// <param name="commandType">MySql命令类型</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="commandParameters">命令的参数</param>
        /// <returns>MySqlDataReader结果</returns>
        public static MySqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteReader(MYSQL_SERVER, cmdType, cmdText, commandParameters);
        }
        //-----------------------------------------------------------------------------------------
        #endregion 
        #region  public static MySqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) //重载：ExecuteReader
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 重载：ExecuteReader
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = new MySqlConnection(connString);
            {
                MySqlCommand cmd = new MySqlCommand();

                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                MySqlDataReader rdr = null;
                try
                {
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (MySqlException e)
                {
                    conn.Close();
                    conn.Dispose();

                    throw e;
                }
                cmd.Parameters.Clear();

                return rdr;
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion


        #region public static object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) // ExecuteScalar
        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="commandType">MySql命令类型</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="commandParameters">命令的参数</param>
        /// <returns>一个须转换成其它类型的值</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteScalar(MYSQL_SERVER, cmdType, cmdText, commandParameters);
        }
        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        #region public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) // 重载：ExecuteScalar
        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 重载:ExecuteScalar
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            object val;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlCommand cmd = new MySqlCommand();

                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);

                try
                {
                    val = cmd.ExecuteScalar();
                }
                catch (MySqlException e)
                {
                    conn.Close();
                    conn.Dispose();

                    throw e;
                }

                cmd.Parameters.Clear();
            }
            return val;


        }
        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion


        #region  public static bool ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) //ExecuteNonQuery
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="commandType">MySql命令类型</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="commandParameters">命令的参数</param>
        /// <returns>一个须转换成其它类型的值</returns>
        public static bool ExecuteNonQuery(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(MYSQL_SERVER, cmdType, cmdText, commandParameters);
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region  public static bool ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) //重载：ExecuteNonQuery
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 重载：ExecuteNonQuery
        /// </summary>
        /// <param name="commandType">MySql命令类型</param>
        /// <param name="commandText">命令语句</param>
        /// <param name="commandParameters">命令的参数</param>
        /// <returns>一个须转换成其它类型的值</returns>
        public static bool ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            int effectRows = 0;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlCommand cmd = new MySqlCommand();

                PrepareCommand(cmd, conn, cmdType, cmdText, commandParameters);
                try
                {
                    effectRows = cmd.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    conn.Close();
                    conn.Dispose();

                    throw e;
                }
                cmd.Parameters.Clear();
            }

            return effectRows > 0;
        }
        //-----------------------------------------------------------------------------------------
        #endregion


        #region private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms) // 配置一个用来执行的Command对像
        // ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 配置一个用来执行的Command对像
        /// </summary>
        /// <param name="cmd">Command对像,在本方法中被改变</param>
        /// <param name="conn">数据库连接对像</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="cmdParms">命令的参数</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        #region  private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms) //为事务作准备
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 为事务作准备
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="cmdParms"></param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        //-----------------------------------------------------------------------------------------
        #endregion


        #region DataSet ExecuteDataset(MySqlConnection, CommandType, string, params MySqlParameter[]) // 返回DataSet
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            return ExecuteDataset(MYSQL_SERVER, commandType, commandText, commandParameters);
        }
        // -----------------------------------------------------------------------------------------
        #endregion

        #region DataSet ExecuteDataset(MySqlConnection, CommandType, string, params MySqlParameter[]) // 返回DataSet
        // -----------------------------------------------------------------------------------------
        /// <summary>
        /// 重载：返回DataSet
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataset(string connString, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandTimeout = 120;

                bool mustCloseConnection = true;
                PrepareCommand(cmd, conn, commandType, commandText, commandParameters);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds);
                    }
                    catch (MySqlException e)
                    {
                        conn.Close();
                        conn.Dispose();

                        throw e;
                    }

                    cmd.Parameters.Clear();

                    if (mustCloseConnection)
                        conn.Close();

                    return ds;
                }
            }
        }
        // -----------------------------------------------------------------------------------------
        #endregion


        #region  public static MySqlDataAdapter ExecuteAdapter(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) // 返回读取适配器
        //------------------------------------------------------------------------------------------
        /// <summary>
        /// 返回读取适配器
        /// </summary>
        /// <param name="connectString">数据库连接字符串</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public static MySqlDataAdapter ExecuteAdapter(string connectString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlConnection conn = new MySqlConnection(connectString);
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (commandParameters != null)
            {
                foreach (MySqlParameter parm in commandParameters)
                {
                    cmd.Parameters.Add(parm);
                }
            }

            return new MySqlDataAdapter(cmd);
        }

        /// <summary>
        /// 返回读取适配器
        /// </summary>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public static MySqlDataAdapter ExecuteAdapter(CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            return ExecuteAdapter(MYSQL_SERVER, cmdType, cmdText, commandParameters);
        }
        //------------------------------------------------------------------------------------------
        #endregion

        #region  public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters) //执行带事务的操作
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 执行带事务的操作
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region  public static IList<T> ExecuteToList<T>(string connString, CommandType cmdType, string cmdText, MappingHandle<T> convert, params MySqlParameter[] commandParameters) //直接返回列表
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 直接返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="convert"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static List<T> ExecuteToList<T>(string connString, CommandType cmdType, string cmdText, Func<IDataReader, T> convert, params MySqlParameter[] commandParameters)
        {
            List<T> list = new List<T>();

            using (IDataReader reader = ExecuteReader(connString, cmdType, cmdText, commandParameters))
            {
                while (reader.Read())
                {
                    list.Add(convert(reader));
                }
            }

            return list;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region  public static IList<T> ExecuteToList<T>(CommandType cmdType, string cmdText, Func<IDataReader, T> convert, params MySqlParameter[] commandParameters) //直接返回列表
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 直接返回列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="convert"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static List<T> ExecuteToList<T>(CommandType cmdType, string cmdText, Func<IDataReader, T> convert, params MySqlParameter[] commandParameters)
        {
            return ExecuteToList<T>(MYSQL_SERVER, cmdType, cmdText, convert, commandParameters);
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region  public static T ExecuteToEntity<T>(string connString, CommandType cmdType, string cmdText, DataMappingEvent<T> convert, params MySqlParameter[] commandParameters) //直接返回实体
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 直接返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="convert"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static T ExecuteToEntity<T>(string connString, CommandType cmdType, string cmdText, Func<IDataReader, T> convert, params MySqlParameter[] commandParameters)
        {
            T t = default(T);

            using (IDataReader reader = ExecuteReader(connString, cmdType, cmdText, commandParameters))
            {

                if (reader.Read())
                {
                    t = convert(reader);
                }

            }

            return t;
        }
        //-----------------------------------------------------------------------------------------
        #endregion

        #region  public static T ExecuteToEntity<T>(CommandType cmdType, string cmdText, DataMappingEvent<T> convert, params MySqlParameter[] commandParameters) //直接返回实体
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 直接返回实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="convert"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static T ExecuteToEntity<T>(CommandType cmdType, string cmdText, Func<IDataReader, T> convert, params MySqlParameter[] commandParameters)
        {
            T t = default(T);

            using (IDataReader reader = ExecuteReader(MYSQL_SERVER, cmdType, cmdText, commandParameters))
            {
               if (reader.Read())
                    {
                        t = convert(reader);
                    }
                 
            }

            return t;
        }
        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
