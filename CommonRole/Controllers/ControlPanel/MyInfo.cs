using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using Common.Util;
using System.Text.RegularExpressions;
using System.ComponentModel;
using MP.Role.Entity;
using MP.Role.Businuss;
using CommonRole.MVCWebSite.Controllers;

namespace CommonRole.Controllers
{

    public partial class ControlPanelController : Controller
    {
        // 当前用户信息

        #region 修改密码
        //------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [Description("修改密码")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldpassword"></param>
        /// <param name="newpasswrod"></param>
        /// <param name="confirmpassword"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("修改密码")]
        public ActionResult ChangePassword(string oldpassword, string newpasswrod, string confirmpassword)
        {
            string retUrl = Url.Action("changepassword");
            if (string.IsNullOrEmpty(oldpassword) || string.IsNullOrEmpty(newpasswrod) || string.IsNullOrEmpty(confirmpassword))
            {
                return CommonResult.ShowMessage("密码修改失败，旧密码或者新密码为空！", retUrl);
            }
            if (newpasswrod != confirmpassword)
            {
                return CommonResult.ShowMessage("密码修改失败，两次密码不一致！", retUrl);
            }
            User_info user = User_infoBLL.Current.GetByID(CurrentUserInfo.CurrentUser.UserID);
            if (user == null)
            {
                return CommonResult.ShowMessage("密码修改失败，用户不存在！", retUrl);
            }
            if (user.UserPassword != Utils.MD5(oldpassword))
            {
                return CommonResult.ShowMessage("密码修改失败，原始密码不正确！", retUrl);
            }
            user.UserPassword = Utils.MD5(newpasswrod);
            if (User_infoBLL.Current.Update(user))
            {
                return CommonResult.ShowMessage("密码修改成功，下次请使用新密码登陆！", retUrl);
            }
            return CommonResult.ShowMessage("密码修改失败，请重试！", retUrl);

        }
        //------------------------------------------------------------------------------------------------------------
        #endregion

        /// <summary>
        /// 当前用户信息
        /// </summary>
        /// <returns></returns>
        [Description("基本资料")]
        public ActionResult MyInfo()
        {

            return View(CurrentUserInfo.CurrentUser);
        }
         
    }
}
