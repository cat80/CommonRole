
#region 文件描述

// 描述：实体类
// 作者：cat80
// 时间：2013-07-18 11:37

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
using MP.Role.Entity;

namespace MP.Role.Entity
{
    /// <summary>
    /// 实体类
    /// </summary>
    [Serializable]
    public partial class User_info
    {


        /// <summary>
        /// 用户分组列表
        /// </summary>
        public List<int> UsersGroupsList
        {
            get
            {
                string[] str = Groups.Split(',');
                List<int> list = new List<int>();
                foreach (var item in str)
                {
                    int i;
                    if (int.TryParse(item, out i))
                    {
                        list.Add(i);
                    }
                }
                return list;
            }
        }

        public string CreateUserName
        { get; set; }

        #region 用户资料及权限
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 用户拥有的资源
        /// </summary>
        private List<Role_resource> _userResource = null;
        /// <summary>
        /// 用户拥有的权限
        /// </summary>
        private List<Role_action> _userAction = null;

        private List<Role_group> _userGroup = null;

        /// <summary>
        /// 用户分组
        /// </summary>
        public List<Role_group> UserGroup
        {
            get; set;

        }

        /// <summary>
        /// 获取资源权限
        /// </summary>
        public List<Role_resource> UserResource
        {
            get; set;

        }


        /// <summary>
        /// 用户功能权限
        /// </summary>
        public List<Role_action> UserAction
        {
            get; set;

        }

        //------------------------------------------------------------------------------------------------------------
        #endregion
        private string _userMenuHtml = null;
        /// <summary>
        /// 用户菜单HTML
        /// </summary>
        public string UserMenuHtml
        {
            get
            { 
                if (_userMenuHtml == null)
                {
                    lock (this)
                    {
                        if (_userMenuHtml == null)
                        {
                            var allParent = UserResource.Where(item => item.ParentID == 0 && item.State == 1 && item.ShowInMenu == 1).ToList();
                            StringBuilder sb = new StringBuilder();
                            foreach (var item in allParent)
                            {
                                sb.AppendFormat("<div class=\"nav-header\">{0}</div>", item.ResourceName);
                                var child = UserResource.Where(sub => sub.ParentID == item.ResourceID && item.State == 1 && item.ShowInMenu == 1).ToList();
                                foreach (var sub in child)
                                {
                                    sb.AppendFormat("<li class=\"subCatalogList\"><a href=\"{0}\">{1}</a> </li>", sub.Url, sub.ResourceName);
                                }

                                //<li class="subCatalogList"><a href="/role/resources">资源管理</a> </li>
                            }
                            _userMenuHtml = sb.ToString();
                        }
                    }
                }
                return _userMenuHtml;
            }
        }

    }
}