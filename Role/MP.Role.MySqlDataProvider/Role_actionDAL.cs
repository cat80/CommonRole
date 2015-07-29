#region 文件描述

// 描述：数据类
// 作者：cat80
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
    public partial class Role_actionDAL
    {
         #region  public List<Role_action> GetPageList(string keyword, int? resourceid, int pagesize, int pageindex, out int recordCount)  /// 获取分布列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取分布列表
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="resourceid">资源ID</param>
        /// <param name="pagesize">每页大小</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="recordCount">记录条数</param>
        /// <returns></returns>
        public List<Role_action> GetPageList(string keyword, int? resourceid, int pagesize, int pageindex, out int recordCount)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT 
  ra.*,
  IFNULL(rr.`ResourceName` ,'') as ResourceName
FROM
  mp_role_action ra 
  LEFT JOIN `mp_role_resource` rr 
    ON ra.`ResourceID` = rr.`ResourceID` WHERE 1 = 1 ");
            List<MySqlParameter> pars = new List<MySqlParameter>();
            if (!string.IsNullOrEmpty(keyword))
            {
                sb.Append(@" AND (
    ra.`ActionName` LIKE @keyword 
    OR ra.`ControllerName` LIKE @keyword 
    OR ra.`DisplayName` LIKE @keyword 
    OR rr.`ResourceName` LIKE @keyword
  )  ");
                pars.Add(new MySqlParameter("@keyword", string.Format("%{0}%", keyword)));
            }
            if (resourceid.HasValue)
            {
                sb.Append("   AND ra.`ResourceID` = @ResourceID  ");
                pars.Add(new MySqlParameter("@ResourceID", resourceid.Value));
            }
            return MySqlHelper.GetPagerList(pagesize, pageindex, out recordCount, sb.ToString(), " rr.`ResourceID` DESC, ra.`Sort` DESC ", item =>
            {
                var action = DataReaderToEntity(item);
                action.ResourceName = item["ResourceName"].ToString();
                return action;
            },
                pars.ToArray());
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion 	  
    }
}

