using MP.Role.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MP.Role.Businuss.Filters
{
    /// <summary>
    /// 角色过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 登陆检查
            //------------------------------------------------------------------------------------------------------------
            if (CurrentUserInfo.CurrentUser == null) //管理员没登陆直接跳转到管理理登陆
            {
                // 是否自动登陆
                if (WebAppsettings.AutoLogon)
                {
                    User_info users = User_infoBLL.Current.GetByUserLoginName(WebAppsettings.AutoLogonUser);
                    User_infoBLL.Current.SetUserAuthInfo(users);
                    if (users == null)
                    {
                        throw new Exception("自动登陆失败，用户名不存在.UserName:" + WebAppsettings.AutoLogonUser);
                    }
                    HttpContext.Current.Session.Add(Keys.Session_Keys.LOGON_MEMBER_INFO, users);
                }
                else
                {
                    filterContext.Result = ActionHelper.GetNoAuthAccess("您未登陆或者超时退出，请重新登陆!", "/account/login", false); //跳转到登陆页面
                }

            }
            //------------------------------------------------------------------------------------------------------------
            #endregion

            #region 权限检查
            //------------------------------------------------------------------------------------------------------------
            if (WebAppsettings.IgnoreAuthCheck == false && filterContext.Result == null) //如果需要权限检查，并且没复制Result
            {
                string controllerName = filterContext.RouteData.Values["controller"].ToString();//获取控制器名称 
                string actionName = filterContext.RouteData.Values["action"].ToString();//获取ACTION 名称 
                if (!User_infoBLL.Current.HasActionAuth(CurrentUserInfo.CurrentUser, controllerName, actionName))
                {
                    filterContext.Result = ActionHelper.GetNoAuthAccess("您当前无权限做此操作", "/controlpanel/");

                }
            }
            //------------------------------------------------------------------------------------------------------------
            #endregion
            base.OnActionExecuting(filterContext);
        }
    }
    /// <summary>
    /// 返回action方法
    /// </summary>
    internal class ActionHelper
    {
        public static ActionResult GetNoAuthAccess(string message, string defaultUrl = "/funs/index", bool showReferrer = true)
        {
            if (showReferrer &&
                HttpContext.Current.Request.UrlReferrer != null)
            {
                defaultUrl = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
            }
            //Ajax请求返回
            //X-Requested-With	XMLHttpRequest
            if (HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                JsonResult json = new JsonResult();
                json.Data = new { code = -1, msg = message, url = defaultUrl };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return json;
                //json.JsonRequestBehavior = JsonRequestBehavior.
            }
            else
            {

                ViewResult vr = new ViewResult();
                vr.ViewBag.Message = message;
                vr.ViewBag.Url = defaultUrl;
                vr.ViewBag.Time = 5 * 1000;
                vr.ViewName = "../Shared/Message";
                return vr;
            }

        }
    }
}
