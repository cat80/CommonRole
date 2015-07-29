#region 文件描述

// 描述：数据类
// 作者：苏志平
// 时间：2013-07-18 11

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
using MP.Role.Entity;
using MySql.Data.MySqlClient;
using System.Data;

namespace MP.Role.MySqlDataProvider
{
    /// <summary>
    /// 数据类
    /// </summary>
    public partial class User_infoDAL
    {
        #region public Users GetByUserId(string username) /// 根据用户名获取用户信息
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User_info GetByUserLoginName(string username)
        {

            User_info users = null;

            string sql = "SELECT * FROM `mp_user_info` u WHERE u.`UserLoginName` = @username;";
            using (IDataReader reader = MySqlHelper.ExecuteReader(CommandType.Text, sql, new MySqlParameter("@username", username)))
            {
                if (reader.Read())
                {
                    users = DataReaderToEntity(reader);
                }
            }
            return users;
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region   public List<User_info> GetAdminList(int? creatorUser, int? isDeleted, string keyword, bool? isExpireOnly, int? status, int? level, int pagesize, int pageindex, out int recordCount)  /// 获取管理员分页列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取管理员分页列表
        /// </summary>
        /// <param name="creatorUser">创建用户，如果为空则查询所有。</param>
        /// <param name="isDeleted">是否为已经删除</param>
        /// <param name="keyword">关键词 </param>
        /// <param name="status">状态</param>
        /// <param name="isExpireOnly">是否公查询过期</param>
        /// <param name="level">用户等级</param>
        /// <param name="pagesize">每页大小</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="recordCount">总记录条数</param>
        /// <returns></returns>
        public List<User_info> GetAdminList(int? creatorUser, int? isDeleted, string keyword, bool? isExpireOnly, int? status, int? level, int pagesize, int pageindex, out int recordCount)
        {
            List<MySqlParameter> pars = new List<MySqlParameter>();
            StringBuilder sb = new StringBuilder(@" 
SELECT 
  au.*
 
FROM
  `mp_user_info` au 
  
WHERE 1 = 1 
");
         
            if (isDeleted.HasValue) //是否为已经删除
            {
                //AND au.`Deleted` = 1 
                sb.Append("   AND au.`Deleted` = @Deleted  ");
                pars.Add(new MySqlParameter("@Deleted", isDeleted.Value));
            }
            if (isExpireOnly.HasValue) //是否为所有过期
            {
                if (isExpireOnly.Value)
                {
                    sb.Append(" AND au.`ExpireTime` < NOW() ");
                }
                else
                {
                    sb.Append(" AND au.`ExpireTime` > NOW() ");
                }
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                sb.Append(" AND (au.`UserName` LIKE @keyword OR au.`UserLoginName` like @keyword  OR au.`Province` LIKE @keyword OR au.`City` like @keyword) ");
                pars.Add(new MySqlParameter("@keyword", string.Format("%{0}%", keyword)));
            }
            if (status.HasValue)
            {
                sb.Append(" AND au.`Status` = @Status ");
                pars.Add(new MySqlParameter("@Status", status.Value));

            } 
             
            return MySqlHelper.GetPagerList(pagesize, pageindex, out recordCount, sb.ToString(), " au.CreateTime Desc  ", reader =>
            {
                var entity = DataReaderToEntity(reader);
                return entity;
            }, pars.ToArray());
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region public int AdminCreateUserCount(int creatorid)  /// 管理员创建用户个数
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 管理员创建用户个数
        /// </summary>
        /// <param name="creatorid"></param>
        /// <returns></returns>
        public int AdminCreateUserCount(int creatorid)
        {
            string sql = @"SELECT 
  COUNT(1) 
FROM
  `mp_store` ms 
WHERE ms.`CreatorAdmin` = " + creatorid;
            return Convert.ToInt32(MySqlHelper.ExecuteScalar(CommandType.Text, sql));
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion


        #region   public List<int> GetAdminIDsByLoginName(List<string> names)    /// 根据登陆名称获取所有的ID列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据登陆名称获取所有的ID列表
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public List<int> GetAdminIDsByLoginName(List<string> names)
        {
            List<int> list = new List<int>();
            if (names == null || names.Count == 0)
            {
                return list;
            }
            List<string> parNames = new List<string>();
            List<MySqlParameter> pars = new List<MySqlParameter>();
            for (int i = 0; i < names.Count; i++)
            {
                string name = "@name" + i;
                parNames.Add(name);
                pars.Add(new MySqlParameter(name, names[i]));
            }
            string sql = string.Format(@"SELECT au.`UserID` FROM `mp_user_info` au 
WHERE au.`UserLoginName` IN({0})", string.Join(",", parNames));
            return MySqlHelper.ExecuteToList(CommandType.Text, sql, item => { return Convert.ToInt32(item["UserID"]); }, pars.ToArray());
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion


        #region public List<int> GetUserChildIDList(int user) /// 获取某个用户创建所有用户的列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取某个用户创建所有用户的列表
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<int> GetUserChildIDList(int user)
        {
            string sql = @"
SELECT au.`UserID` FROM `mp_user_info` au 
WHERE au.`CreateUser` = " + user;
            return MySqlHelper.ExecuteToList(CommandType.Text, sql, item =>
            {

                return item.GetInt32(0);
            });
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion


    }
}

