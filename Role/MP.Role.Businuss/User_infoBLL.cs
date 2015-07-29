#region 文件描述

// 描述：业务逻辑层
// 作者：cat80
// 时间：2013-07-18 16:45

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
using System.Web;


namespace MP.Role.Businuss
{
    /// <summary>
    /// 业务逻辑层
    /// </summary>
    public partial class User_infoBLL : BllBase
    {
        #region public LoginResultStatus Login(string userid, string password) /// 用户登陆
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public LoginResultStatus Login(string userid, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userid) || string.IsNullOrEmpty(password))
                {
                    return LoginResultStatus.用户名和密码不匹配;
                }
                User_info userinfo = _User_infoDal.GetByUserLoginName(userid);
                if (userinfo == null ||
                    !userinfo.UserPassword.Equals(Utils.MD5(password), StringComparison.CurrentCultureIgnoreCase))
                {
                    return LoginResultStatus.用户名和密码不匹配;
                }
                if (userinfo.Status == 0)
                {
                    return LoginResultStatus.账户被禁用;
                }
                if (userinfo.ExpireTime < DateTime.Now)
                {
                    return LoginResultStatus.账号已经到期;
                }
                //userinfo.LastLoginTime = DateTime.Now;
                //userinfo.LastLoginIP = HttpContext.Current.Request.UserHostAddress;
                //userinfo.LoginCount += 1;
                //Current.Update(userinfo);
                HttpContext.Current.Session[Keys.Session_Keys.LOGON_MEMBER_INFO] = userinfo;
                SetUserAuthInfo(userinfo);

                //写登陆历史
                //User_login_hisBLL.Current.Append(new User_login_his()
                //{
                //    LoginIP = Utils.GetRealIP(),
                //    LoginTime = DateTime.Now,
                //    UserID = userinfo.UserID,
                //    UserName = userinfo.UserName,
                //    UserType = 1

                //});
                return LoginResultStatus.登陆成功;

            }
            catch (Exception ex)
            {
                DealError(ex);
                return LoginResultStatus.系统出错;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region public bool ExistsUserID(string userid) /// 是否已经存在相同的用户登陆名userid
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 是否已经存在相同的用户登陆名userid
        /// </summary>
        /// <param name="userid">登陆名</param>
        /// <returns></returns>
        public bool ExistsUserID(string userid)
        {
            try
            {
                return _User_infoDal.GetByUserLoginName(userid) != null;
            }
            catch (Exception ex)
            {
                DealError(ex);
                return true;
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region public Users GetByUserId(string username) /// 根据用户名获取用户信息
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User_info GetByUserLoginName(string username)
        {
            try
            {
                return _User_infoDal.GetByUserLoginName(username);
            }
            catch (Exception ex)
            {
                DealError(ex);
                return null;
            }
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
            try
            {
                return _User_infoDal.GetAdminList(creatorUser, isDeleted, keyword, isExpireOnly, status, level, pagesize, pageindex, out recordCount);
            }
            catch (Exception ex)
            {
                recordCount = 0;
                DealError(ex);
                return new List<User_info>();
            }
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
            try
            {
                return _User_infoDal.AdminCreateUserCount(creatorid);
            }
            catch (Exception ex)
            {
                DealError(ex);
                return 0;
            }
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
            try
            {
                return _User_infoDal.GetAdminIDsByLoginName(names);
            }
            catch (Exception ex)
            {
                DealError(ex);
                return new List<int>();
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region public List<int> GetUserChildIDList(int user) /// 获取某个用户创建所有用户的列表
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 获取某个用户创建所有用户的列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<int> GetUserChildIDList(int userid)
        {
            try
            {
                return _User_infoDal.GetUserChildIDList(userid);
            }
            catch (Exception ex)
            {
                DealError(ex);
                return new List<int>();
            }
        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        #region 设置用户访问权限

        public void SetUserAuthInfo(User_info userinfo)
        {
            //设置用户分组
            userinfo.UserGroup = Role_groupBLL.Current.CacheAllDataList().Where(item => userinfo.UsersGroupsList.Contains(item.GroupID)).ToList();
            //调用用户资源
            //先获取去重复的资源ID列表
            var resids = new List<int>();
            userinfo.UserGroup.ForEach(item => resids.AddRange(item.Auth_ResourceList));
            resids = resids.Distinct().ToList();
            userinfo.UserResource = Role_resourceBLL.Current.CacheAllDataList().Where(item => resids.Contains(item.ResourceID)).OrderByDescending(item => item.Sort).ToList();
            //设置用户actions 
            var actionIDs = new List<int>();
            userinfo.UserGroup.ForEach(item => actionIDs.AddRange(item.Auth_ActionList));
            actionIDs = actionIDs.Distinct().ToList();
            userinfo.UserAction = Role_actionBLL.Current.CacheAllDataList().Where(item => actionIDs.Contains(item.ActionID)).OrderByDescending(item => item.Sort).ToList();
        }
        #endregion

        /// <summary>
        /// 判断当前用户是否拥有某个功能的权限
        /// </summary>
        /// <param name="cotrollerName">控制器名</param>
        /// <param name="actionName">功能名</param>
        /// <returns>如果有权限就返回True，如果相应的控制器不存在，或者已经被禁用，或者无权限则返回False</returns>
        public bool HasActionAuth(User_info info, string cotrollerName, string actionName)
        {
            return info.UserAction.Where(item => item.ControllerName.Equals(cotrollerName, StringComparison.InvariantCultureIgnoreCase) &&
                item.ActionName.Equals(actionName, StringComparison.InvariantCultureIgnoreCase) &&
                item.Status == 1).FirstOrDefault() != null;
        }
    }
}
