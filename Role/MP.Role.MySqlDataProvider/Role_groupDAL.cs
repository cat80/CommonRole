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
    public partial class Role_groupDAL
    {
        #region public void UpdateBaseInfo(Role_group entity) //更新一条的基础信息(不更新权限）
        //------------------------------------------------------------------------------------------
        /// <summary>
        //更新一条的基础信息(不更新权限）
        /// </summary>
        /// <param name="entity"></param>
        public bool UpdateBaseInfo(Role_group entity)
        {
            string sqlCmd = "update mp_role_group set  GroupName = @GroupName, State = @State  where   GroupID =  @GroupID ";

            MySqlParameter[] pars = new MySqlParameter[3];
            pars[0] = new MySqlParameter("@GroupID", entity.GroupID);
            pars[1] = new MySqlParameter("@GroupName", entity.GroupName);
            pars[2] = new MySqlParameter("@State", entity.State);

            return MySqlHelper.ExecuteNonQuery(CommandType.Text, sqlCmd, pars);
        }
        //------------------------------------------------------------------------------------------
        #endregion
    }
}

