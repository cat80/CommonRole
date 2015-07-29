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
using  Common;

namespace Common
{
    /// <summary>
    /// 业务逻辑层基类
    /// </summary>
    public class BllBase
    { 

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
            Log log = new Log(typeof(BllBase));
            log.Error(e);
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
