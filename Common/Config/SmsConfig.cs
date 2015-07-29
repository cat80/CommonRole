using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Config
{
    /// <summary>
    /// 短信发送配置
    /// </summary>
    public class SmsConfig
    {
        /// <summary>
        /// 短信类别
        /// </summary>
        public string SmsType { get; set; }

        /// <summary>
        /// 发送账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 发送密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 邮箱SMPT服务器
        /// </summary>
        public string Smpt { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 重载的ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("smstype:{0},account:{1},password:{2},status:{3}", SmsType, Account, Password, Enabled);
            //return base.ToString();
        }
    }
}
