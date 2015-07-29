using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Common.Config;

namespace Common
{
    /// <summary>
    /// 邮件辅助类
    /// </summary>
    public class Smtp
    {
        /// <summary>
        /// smtp发送邮件
        /// </summary>
        /// <param name="smtpSever">smtp服务</param>
        /// <param name="from">发送邮件ID</param>
        /// <param name="password">邮件密码</param>
        /// <param name="to">发送用户</param>
        /// <param name="title">标题</param>
        /// <param name="body">邮件内容</param>
        public static void SendMail(string smtpSever, string from, string password, string to, string title, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient(smtpSever);
                //client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(from, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailAddress addressFrom = new MailAddress(from);
                MailAddress addressTo = new MailAddress(to);

                System.Net.Mail.MailMessage message = new MailMessage(addressFrom, addressTo);

                //message.Sender = new MailAddress(from);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Subject = title;
                message.Body = body;
                client.Send(message);
            }
            catch (Exception ex)
            {
                Log.Default.Error(string.Format("邮件发送失败，from:{0},to:{1},smtpserve:{2},原因：{3}", from, to, smtpSever, ex));
            }
        }

        /// <summary>
        /// 只需要输入接收邮箱，就可进行发送
        /// </summary>
        /// <param name="fromDisplayName">如:旅游通知邮件</param>
        /// <param name="to">接收邮箱</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容</param>
        public static void SendMail(string fromDisplayName, string to, string title, string body)
        {
            try
            {
                SmsConfig config = SiteConfig.Current.GetSmsByType("email");
                SmtpClient client = new SmtpClient(config.Smpt);
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(config.Account, config.Password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailAddress addressFrom = new MailAddress(config.Account, fromDisplayName);
                MailAddress addressTo = new MailAddress(to);

                System.Net.Mail.MailMessage message = new MailMessage(addressFrom, addressTo);

                //message.Sender = new MailAddress(from);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Subject = title;
                message.Body = body;
                client.Send(message);
                Log.Default.Debug("邮件发送成功");
            }
            catch (Exception ex)
            {
                Log.Default.Error("邮件发送失败原因:" + ex.ToString());
            }
        }

        /// <summary>
        /// smtp发送邮件
        /// </summary>
        /// <param name="smtpSever">smtp服务</param>
        /// <param name="from">发送邮件ID</param>
        /// <param name="password">邮件密码</param>
        /// <param name="to">发送用户</param>
        /// <param name="title">标题</param>
        /// <param name="body">邮件内容</param>
        public static void SendMail(string smtpSever, string from, string password, string fromDisplayName, string to, string title, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient(smtpSever);

                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(from, password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailAddress addressFrom = new MailAddress(from, fromDisplayName);
                MailAddress addressTo = new MailAddress(to);

                System.Net.Mail.MailMessage message = new MailMessage(addressFrom, addressTo);

                //message.Sender = new MailAddress(from);
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Subject = title;
                message.Body = body;
                client.Send(message);
            }
            catch (Exception ex)
            {
                Log.Default.Error(string.Format("邮件发送失败，from:{0},to:{1},smtpserve:{2},原因：{3}", from, to, smtpSever, ex));
            }
        }
    }
}
