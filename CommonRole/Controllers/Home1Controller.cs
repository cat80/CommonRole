using MP.Role.Businuss;
using MP.Role.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonRole.Controllers
{
    public class Home1Conroller : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return Content("content1");
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            LoginResultStatus result = User_infoBLL.Current.Login(username, password);
            ViewBag.LoginError = result;
            return View();
        }
    }
}
