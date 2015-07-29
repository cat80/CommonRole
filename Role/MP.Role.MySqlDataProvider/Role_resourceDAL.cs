#region 文件描述

// 描述：数据类
// 作者：苏志平
// 时间：2013-11-04 11

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
    public partial class Role_resourceDAL
    {

        #region   public bool DeleteAndSub(int resID) /// 删除资源，同时删除子元素
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 删除资源，同时删除子元素
        /// </summary>
        /// <param name="resID"></param>
        /// <returns></returns>
        public bool DeleteAndSub(int resID)
        {
            string sql = string.Format("DELETE FROM `mp_role_resource` WHERE `ResourceID` = {0} OR `ParentID` = {0};", resID);
            return MySqlHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	
			 
    }
}

