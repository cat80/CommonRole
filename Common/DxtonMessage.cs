#region 文件描述

// 作者：author
// 时间：2014/2/20 20:46:50
// 描述：

#endregion

#region 类修改记录 : 每次修改一组描述

// 修 改 人：
// 修改时间： 
// 修改描述：

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Net;
using System.Xml.Linq;
using System.IO;
using Common.Config;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    /// <summary>
    /// 短信通发短信
    /// </summary>
    public class DxtonMessage
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="moblie">发送者用户名</param>
        /// <param name="content">发送内容</param>
        public static SendResultStatus SendMessage(string mobile, string content)
        {

            string postStrTpl = "account={0}&password={1}&mobile={2}&content={3}";
            string postStr = string.Format(postStrTpl, DxtonAccount, DxtonPassword, mobile, content);
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] postData = encoding.GetBytes(postStr);
            try
            {

                WebClient client = new WebClient();
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
                client.Encoding = Encoding.UTF8;
                byte[] returnBytes = client.UploadData(_posturl, postData);
                string text = Encoding.UTF8.GetString(returnBytes);

                StringReader reader = new StringReader(text);
                XDocument doc = XDocument.Load(reader);

                int code = 0;
                if (int.TryParse(doc.Element("response").Element("result").Value, out code))
                {
                    return (SendResultStatus)code;
                }
                return SendResultStatus.结果未知;
            }
            catch (Exception ex)
            {
                Log.Default.Error(string.Format("短信能送失败，发送参数:{0},异常：{1}", postStrTpl, ex));
                return SendResultStatus.网络错误;
            }
        }

        public static string _posturl = "http://www.dxton.com/webservice/sms.asmx/Submit";
        private static string DxtonAccount
        {
            get
            {
                return ConfigurationManager.AppSettings["DxtonAccount"];
            }
        }
        private static string DxtonPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["DxtonPassword"];
            }
        }


        #region 新短信接收与发送

        #region 发送短信
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="moblie">发送者用户名</param>
        /// <param name="smsid">目标号码+当前时间戳整数</param>
        /// <param name="content">发送内容</param>
        public static YunXinSendResultStatus SendMessage(string mobile, string smsid, string content)
        {
            SmsConfig config = SiteConfig.Current.GetSmsByType("yunxin");
            if (config == null)
            {
                Log.Default.Debug("短信配置错误！");
                return YunXinSendResultStatus.短信配置错误;

            }
            if (config.Enabled == false)
            {
                Log.Default.Debug("短信配置未启用");
                return YunXinSendResultStatus.短信配置未启用;

            }

            string requestUrl = string.Format("http://api.sms.cn/mt/?uid={0}&pwd={1}&mobile={2}&mobileids={3}&content={4}&encode=utf8", config.Account, config.Password, mobile, smsid, HttpUtility.UrlEncode(content, Encoding.UTF8));
            Log.Default.Debug("【短信发送】接口：" + requestUrl);

            try
            {

                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                var result = client.DownloadString(requestUrl);

                //提取状态码
                Regex regex = new Regex(@"stat\=(\d+)");
                var match = regex.Match(result);
                if (match.Success)
                {
                    Log.Default.Debug("返回状态码：" + match.Groups[1].Value);
                    return (YunXinSendResultStatus)Convert.ToInt32(match.Groups[1].Value);
                }
                return YunXinSendResultStatus.结果未知;

            }
            catch (Exception ex)
            {
                Log.Default.Error(string.Format("短信发送失败，发送参数:{0},异常：{1}", requestUrl, ex));
                return YunXinSendResultStatus.网络错误;
            }
        }

        #endregion

        #endregion
    }

    #region 发送状态Enum
    /// <summary>
    /// 发送结果状态
    /// </summary>
    public enum YunXinSendResultStatus
    {
        发送成功 = 100,
        验证失败 = 101,
        短信不足 = 102,
        操作失败 = 103,
        非法字符 = 104,
        内容过多 = 105,
        号码过多 = 106,
        频率过快 = 107,
        号码内容空 = 108,
        禁止频繁单条发送 = 110,
        号码错误 = 112,
        定时时间格式不对 = 113,
        账号被锁 = 114,
        禁止接口发送 = 116,
        绑定IP不正确 = 117,
        系统升级 = 120,

        短信配置错误 = 125,
        短信配置未启用 = 125,

        结果未知 = 130,

        网络错误 = 140
    }
    #endregion

    /// <summary>
    /// 发送结果状态
    /// </summary>
    public enum SendResultStatus
    {
        发送成功 = 100,
        验证失败 = 101,
        手机号码格式不正确 = 102,
        会员级别不够 = 103,
        内容未审核 = 104,
        内容过多 = 105,
        账户余额不足 = 106,
        系统升级 = 120,
        结果未知 = 410,
        网络错误 = 500

    }
}
