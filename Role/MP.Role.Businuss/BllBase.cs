#region 文件描述

// 描述：业务逻辑层基类
// 作者：刘永明
// 时间：2010-4-6

#endregion

#region 类修改记录 : 每次修改一组描述

// 修改描述：
// 修 改 人：
// 修改时间：

#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using Common;
using MP.Role.Entity;

namespace MP.Role.Businuss
{
    /// <summary>
    /// 业务逻辑层基类
    /// </summary>
    public class BllBase
    {
        #region public 数据成员
        // ----------------------------------------------------------------------------------------
        /// <summary>
        /// 错误日志存储路径
        /// </summary>
        private static string _APPLICATION_ERROR_PATH = String.Empty;

        /// <summary>
        /// 错误日志存储路径
        /// </summary>
        private static string APPLICATION_ERROR_PATH
        {
            get
            {
                if (String.IsNullOrEmpty(_APPLICATION_ERROR_PATH))
                {
                    lock (_APPLICATION_ERROR_PATH)
                    {
                        if (String.IsNullOrEmpty(ConfigurationManager.AppSettings["ApplicationErrorPath"]))
                        {
                            _APPLICATION_ERROR_PATH = AppDomain.CurrentDomain.BaseDirectory + "/Error/";
                        }
                        else
                        {

                            _APPLICATION_ERROR_PATH = ConfigurationManager.AppSettings["ApplicationErrorPath"];
                            if (!(_APPLICATION_ERROR_PATH.EndsWith(@"\") || _APPLICATION_ERROR_PATH.EndsWith(@"/")))
                            {
                                _APPLICATION_ERROR_PATH += @"\";
                            }
                        }

                        if (!Directory.Exists(_APPLICATION_ERROR_PATH))
                        {
                            Directory.CreateDirectory(_APPLICATION_ERROR_PATH);
                        }
                    }
                }

                return _APPLICATION_ERROR_PATH;
            }
        }
        // ----------------------------------------------------------------------------------------
        #endregion

        public static string PageLastLoginIP()
        {
            return string.Empty;
        }

        #region  protected void DealError(Exception e) //处理错误
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="e"></param>
        protected void DealError(Exception e)
        {
            //Log log = new Log(typeof(BllBase));
            Log.Default.Error(e);
            //DateTime dt = DateTime.Now;
            //string errorLogName = String.Format("{0}{1}_Error.txt", APPLICATION_ERROR_PATH, dt.ToString("yyyyMMdd"));

            //using (StreamWriter sw = new StreamWriter(errorLogName, true, Encoding.GetEncoding("gb2312")))
            //{
            //    sw.WriteLine(String.Format("[{0}]{1}\r\n"
            //        , dt.ToString("HH:mm:hh")
            //        , e.ToString()));
            //}
        }
        //-----------------------------------------------------------------------------------------
        #endregion
    }
}
