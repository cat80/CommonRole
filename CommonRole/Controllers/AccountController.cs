using MP.Role.Businuss;
using MP.Role.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonRole.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            LoginResultStatus result = User_infoBLL.Current.Login(username, password);
            if (result == LoginResultStatus.登陆成功)
            {
                return RedirectToAction( "adminlist", "controlpanel");
            }
            ViewBag.LoginError = result;
            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return Redirect(Url.Action("login"));
        }
    }
}