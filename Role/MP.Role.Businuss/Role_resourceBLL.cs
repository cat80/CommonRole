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
    public partial class Role_resourceBLL : BllBase
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
            try
            {

                bool result = _Role_resourceDal.DeleteAndSub(resID);
                ClearCacheData();
                return result;
            }
            catch (Exception ex)
            {
                DealError(ex);
                return false;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion
    }
}
