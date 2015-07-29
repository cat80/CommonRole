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
    public partial class Role_groupBLL : BllBase
    {
        #region public void UpdateBaseInfo(Role_group entity) //更新一条的基础信息(不更新权限）
        //------------------------------------------------------------------------------------------
        /// <summary>
        //更新一条的基础信息(不更新权限）
        /// </summary>
        /// <param name="entity"></param>
        public bool UpdateBaseInfo(Role_group entity)
        {
            try
            {
                ClearCacheData();
                return _Role_groupDal.UpdateBaseInfo(entity);
                
            }
            catch (Exception ex)
            {
                DealError(ex);
                return false;
            }
        }
        //------------------------------------------------------------------------------------------
        #endregion
    }
}
