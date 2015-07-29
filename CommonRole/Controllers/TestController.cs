using MP.Role.Businuss.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonRole.Controllers
{
    [RoleFilter]
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test1()
        { return Content("test1"); }
    }
}