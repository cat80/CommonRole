using MP.Role.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MP.Role.Businuss
{
    /// <summary>
    /// 当前登陆用户信息
    /// </summary>
    public class CurrentUserInfo
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public static User_info CurrentUser
        {
            get
            {
                return HttpContext.Current.Session[Keys.Session_Keys.LOGON_MEMBER_INFO] as User_info;
            }
        }

        /// <summary>
        /// 是否有
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool HasActionAuth(string controlName, string actionName)
        {
            return User_infoBLL.Current.HasActionAuth(CurrentUser, controlName, actionName);
        }



        /// <summary>
        /// 判断用户是否登陆
        /// </summary>
        public static bool IsLogin
        {
            get
            {
                return CurrentUser != null;
            }
        }
    }
}
