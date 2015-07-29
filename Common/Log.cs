#region 文件描述

// 描述：日志
// 作者：cat80
// 时间：2012-06-26 11:40

#endregion

#region 类修改记录 : 每次修改一组描述

// 修改描述：
// 修 改 人：
// 修改时间：

#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;
using log4net.Config;
using System.Web;

namespace Common
{
    public class Log
    {
        private static Log _Default = new Log(typeof(Log));
        public static Log Default
        {
            get
            {
                return _Default;
            }
        }
        // Fields
        private ILog log;

        // Methods
        static Log()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (!baseDirectory.EndsWith(@"\"))
            {
                baseDirectory = baseDirectory + @"\";
            }
            FileInfo configFile = new FileInfo(baseDirectory + "log4net.config");
            XmlConfigurator.Configure(configFile);
        }

        public Log(string name)
        {
            this.log = LogManager.GetLogger(name);
        }

        public Log(Type type)
        {
            this.log = LogManager.GetLogger(type);
        }

        public void Debug(object message)
        {
            this.log.Debug(message);


        }

        public void Debug(object message, Exception exception)
        {
            this.log.Debug(message, exception);


        }

        public void Debug(object message, params object[] args)
        {
            this.Debug(string.Format(message.ToString(), args));


        }

        public void Error(object message)
        {
            this.log.Error(message);


        }
        private void LogInfo()
        {
            if (HttpContext.Current != null)
            {
                this.log.Info(string.Format(@"当前请求页:{0}", HttpContext.Current.Request.Url.AbsoluteUri));
                this.log.Info(string.Format(@"请求IP来源:{0}", HttpContext.Current.Request.UserHostAddress));
            }
        }
        public void Error(object message, params object[] args)
        {
            this.Error(string.Format(message.ToString(), args));


        }

        public void Error(object message, Exception exception)
        {
            this.log.Error(message, exception);

        }

        public void Fatal(object message)
        {
            this.log.Fatal(message);


        }

        public void Fatal(object message, Exception exception)
        {
            this.log.Fatal(message, exception);


        }

        public void Fatal(object message, params object[] args)
        {
            this.Fatal(string.Format(message.ToString(), args));


        }

        public void Info(object message)
        {
            this.log.Info(message);


        }

        public void Info(object message, Exception exception)
        {
            this.log.Info(message, exception);


        }

        public void Info(object message, params object[] args)
        {
            this.Info(string.Format(message.ToString(), args));


        }

        public void Warn(object message)
        {
            this.log.Warn(message);


        }

        public void Warn(object message, Exception exception)
        {
            this.log.Warn(message, exception);


        }

        public void Warn(object message, params object[] args)
        {
            this.Warn(string.Format(message.ToString(), args));


        }

        // Properties
        public bool IsDebugEnabled
        {
            get
            {
                return this.log.IsDebugEnabled;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                return this.log.IsErrorEnabled;
            }
        }

        public bool IsFatalEnabled
        {
            get
            {
                return this.log.IsFatalEnabled;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                return this.log.IsInfoEnabled;
            }
        }

        public bool IsWarnEnabled
        {
            get
            {
                return this.log.IsWarnEnabled;
            }
        }

        public void WebPageInfo()
        {
            if (HttpContext.Current != null)
            {
                log.Info("当前请求IP:" + HttpContext.Current.Request.UserHostAddress);
                log.Info("当前请求页:" + HttpContext.Current.Request.Url.AbsoluteUri);
            }
        }
    }



}
