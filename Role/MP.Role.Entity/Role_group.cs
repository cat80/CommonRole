 
#region 文件描述

// 描述：实体类
// 作者：cat80
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
using System.Data;

namespace MP.Role.Entity
{
    /// <summary>
    /// 实体类
    /// </summary>
   [Serializable]
    public partial class Role_group
    {
        /// <summary>
        /// 强类型的actionList
        /// </summary>
        public List<int> Auth_ActionList
        {
            get
            {
                List<int> list = new List<int>();
                if (!string.IsNullOrEmpty(Auth_Action))
                {
                    string[] arr = Auth_Action.Split(',');
                    foreach (var item in arr)
                    {
                        int i = 0;
                        if (int.TryParse(item, out i))
                        {
                            list.Add(i);
                        }
                    }
                }
                return list;
            }
        }


        /// <summary>
        /// 强类型的actionList
        /// </summary>
        public List<int> Auth_ResourceList
        {
            get
            {
                List<int> list = new List<int>();
                if (!string.IsNullOrEmpty(Auth_Resource))
                {
                    string[] arr = Auth_Resource.Split(',');
                    foreach (var item in arr)
                    {
                        int i = 0;
                        if (int.TryParse(item, out i))
                        {
                            list.Add(i);
                        }
                    }
                }
                return list;
            }
        }
    }
}