using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CommonRole.MVCWebSite.Controllers
{
    /// <summary>
    /// BaseContrller基类
    /// </summary>
    public class BaseController : Controller
    {
        [NonAction]
        public ActionResult AjaxRequestParamterError()
        {
            return MessageJosnResult(0, "参数错误！");
        }

        [NonAction]
        public ActionResult MessageJosnResult(int resultCode, string message)
        {
            if (Request.IsAjaxRequest())
            {
                return Json(new
                {
                    result = resultCode,
                    message = message
                });
            }
            string backUrl = BackUrl;
            if (string.IsNullOrEmpty(backUrl))
            {
                if (Request.UrlReferrer != null)
                {
                    backUrl = Request.UrlReferrer.AbsoluteUri;
                }
                else
                {
                    backUrl = "/";
                }
            }
            return CommonResult.ShowMessage(message, backUrl);

        }

        protected virtual string BackUrl { get; set; }

        public ActionResult RedirectUrl(string url)
        {
            this.Response.Clear(); ;//这里是关键，清除在返回前已经设置好的标头信息，这样后面的跳转才不会报错
            this.Response.BufferOutput = true;//设置输出缓冲
            if (!this.Response.IsRequestBeingRedirected)//在跳转之前做判断,防止重复
            {
                return Redirect(url);
            }
            return Redirect(url);
        }
    }


    public class CommonResult
    {
        public static ActionResult ShowMessage(string message, string url)
        {
            return ShowMessage(message, url, 3);
        }
        //返回一个json Reuslt
        public static ActionResult JsonMessage(int code, string msg)
        {
            JsonResult result = new JsonResult();
            result.Data = new { code = code, msg = msg };
            return result;
        }
        /// <summary>
        /// 返回信息展示页面ViewResult
        /// </summary>
        /// <param name="message">展示内容</param>
        /// <param name="url">返回Url</param>
        /// <param name="time">展示时间(秒）</param>
        /// <returns></returns>
        public static ActionResult ShowMessage(string message, string url, int time)
        {
            if (time < 3)
            {
                time = 3;
            }
            ViewResult vr = new ViewResult();
            vr.ViewBag.Message = message;
            vr.ViewBag.Url = url;
            vr.ViewBag.Time = time * 1000;
            vr.ViewName = "../Shared/Message";
            return vr;
        }


        public static ActionResult DinnerShowMessage(string message, string url)
        {
            return DinnerShowMessage(message, url, 3);
        }

        /// <summary>
        /// 返回信息展示页面ViewResult
        /// </summary>
        /// <param name="message">展示内容</param>
        /// <param name="url">返回Url</param>
        /// <param name="time">展示时间(秒）</param>
        /// <returns></returns>
        public static ActionResult DinnerShowMessage(string message, string url, int time)
        {
            if (time < 3)
            {
                time = 3;
            }
            ViewResult vr = new ViewResult();
            vr.ViewBag.Message = message;
            vr.ViewBag.Url = url;
            vr.ViewBag.Time = time * 1000;
            vr.ViewName = "../DinnerTow/DinnerMiddlePage";
            return vr;
        }
    }
}
