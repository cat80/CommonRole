using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.Web;

namespace Common
{
    /// <summary>
    /// 飞信发送地址
    /// </summary>
    public class Fetion
    {
        /// <summary>
        /// 发送信息信息
        /// </summary>
        /// <param name="from">发送者</param>
        /// <param name="pwd">密码</param>
        /// <param name="to">接收用户</param>
        /// <param name="msg">发送内容</param>
        /// <returns></returns>
        public static bool SendMessage(string from, string pwd, string to, string msg)
        {
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;

                client.DownloadString(string.Format("{0}?from={1}&to={2}&msg={3}&pwd={4}",
                    ConfigurationManager.AppSettings["fetionmsgurl"],
                    from,
                    to,
                   HttpUtility.UrlEncode(msg, Encoding.UTF8),
                    HttpUtility.UrlEncode(pwd, Encoding.UTF8)
                    ));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
