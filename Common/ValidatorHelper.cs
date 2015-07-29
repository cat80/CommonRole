#region 文件描述

// 作者：author
// 时间：2014/2/17 16:29:02
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
using System.Text.RegularExpressions;

namespace Common
{
    /// <summary>
    /// 验证辅助类
    /// </summary>
    public static class ValidatorHelper
    {


        #region 正则表达式
        //------------------------------------------------------------------------------------------------------------
        private static Regex phoneRegex = new Regex(@"^1\d{10}$");
        //------------------------------------------------------------------------------------------------------------
        #endregion


        /// <summary>
        /// 验证单个电话号码
        /// <para>目前只验证1开头和11位</para>
        /// </summary>
        /// <param name="phoneNumber">电话号码</param>
        /// <returns></returns>
        public static bool MobliePhoneNumber(string phoneNumber)
        {
            return phoneRegex.Match(phoneNumber).Success;
        }

        /// <summary>
        /// 验证是否为多个电话号码
        /// </summary>
        /// <param name="phoneNumber">电话号码</param>
        /// <param name="splitChar">分隔字符串</param>
        /// <returns></returns>
        public static bool MobliePhoneNumber(string phoneNumber, char splitChar)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }
            string[] arr = phoneNumber.Split(',');
            foreach (var item in arr)
            {
                if (!MobliePhoneNumber(item))
                {
                    return false;
                }
            }
            return true;
        }

        #region 如果IsNotChinese为FALSE就是汉字
        /// <summary>
        /// 如果IsNotChinese为FALSE就是汉字
        /// </summary>
        /// <param name="s">值</param>
        /// <returns></returns>
        public static bool IsNotChinese(string s)
        {
            Regex objReg = new Regex(@"^[^\u4E00-\u9FA0]+$");
            return objReg.IsMatch(s.Trim());
        }
        /// <summary>
        /// 如果IsNotChinese为FALSE就是汉字
        /// </summary>
        /// <param name="s">值</param>
        /// <returns></returns>
        public static bool IsNotChinese(char c)
        {
            Regex objReg = new Regex(@"^[^\u4E00-\u9FA0]+$");
            return objReg.IsMatch(c.ToString());
        }
        #endregion
    }
}
