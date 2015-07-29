#region 文件描述

// 描述：业务逻辑层
// 作者：苏志平
// 时间：2013-11-04 11:15

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
using Common;
using MP.Role.Entity;
using MP.Role.MySqlDataProvider;


namespace MP.Role.Businuss
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public partial class Role_actionBLL : BllBase
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
            try
            {
                return _Role_actionDal.GetPageList(keyword, resourceid, pagesize, pageindex, out recordCount);
            }
            catch (Exception ex)
            {
                recordCount = 0;
                DealError(ex);
                return new List<Role_action>();
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion
    }
}
