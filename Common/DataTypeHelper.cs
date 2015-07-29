using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Data;
using System.Text;

namespace Common
{
    public static class DataTypeHelper
    {
        /// <summary>
        /// 判断是否字母和汉字
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static bool IsLetterWithChinese(string str)
        {
            string pattern = @"[a-zA-Z\u4e00-\u9fa5]+$";
            return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 正则表达式验证手机
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMobile(string str)
        {
            string pattern = @"^(13[0-9]|15[0-9]|18[0-9])\d{8}$";
            return Regex.IsMatch(str, pattern);
        }
        /// <summary>
        /// 去除所有的html格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string WipeHtml(string str)
        {
            string pattern = @"<[\s\S]*?>";
            str = Regex.Replace(str, pattern, "", RegexOptions.IgnoreCase);
            return str.Replace("&nbsp;", "");
        }

        /// <summary>
        /// 去除样式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string WipeStyle(string str)
        {
            string pattern = @"style=""[\s\S]*?""";
            str = Regex.Replace(str, pattern, "", RegexOptions.IgnoreCase);
            pattern = @"class=""[\s\S]*?""";
            str = Regex.Replace(str, pattern, "", RegexOptions.IgnoreCase);
            return str;
        }



        public static int GetQueryValue(string key, int defaultValue)
        {
            string ss = System.Web.HttpContext.Current.Request[key];
            return System.Web.HttpContext.Current.Request[key].TryParseInt(defaultValue);
        }
        public static string GetQueryValue(string key, string defaultValue)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;
            if (string.IsNullOrEmpty(System.Web.HttpContext.Current.Request[key]))
                return defaultValue;
            return HttpContext.Current.Request[key].GetFilterStr();
        }
        /// <summary>
        /// 取得网站根目录的物理路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            if (HttpCurrent != null)
            {
                AppPath = HttpCurrent.Server.MapPath("~");
            }
            else
            {
                AppPath = AppDomain.CurrentDomain.BaseDirectory;
                if (Regex.Match(AppPath, @"\\$", RegexOptions.Compiled).Success)
                    AppPath = AppPath.Substring(0, AppPath.Length - 1);
            }
            return AppPath;
        }
        #region 过滤字符串
        /// <summary>
        /// 过滤字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        public static string StringFiltrate(string input)
        {
            if (input != null || input != String.Empty)
            {
                StringBuilder sb = new StringBuilder(input);
                sb.Replace("\\", "");
                sb.Replace("..", "");
                sb.Replace("and ", "");
                sb.Replace("exec ", "");
                sb.Replace("or ", "");
                sb.Replace("='", "");
                //sb.Replace(")", "");
                //sb.Replace("(", "");
                sb.Replace("--", " ");
                sb.Replace("*", " ");
                sb.Replace("'", " ");
                sb.Replace("[", " ");
                sb.Replace("]", " ");
                sb.Replace("'", " ");
                sb.Replace("\"", "");
                sb.Replace("script", "");
                sb.Replace("ifarme", "");
                sb.Replace("farme", "");
                sb.Replace("alert", "");
                sb.Replace("confirm", "");
                sb.Replace("<", "");
                sb.Replace(">", "");
                sb.Replace("|", "");
                sb.Replace("||", "");
                //sb.Replace(" ","");
                string result = sb.ToString();
                result = result.Replace("\\", "");
                result = result.Replace("..", "");
                System.Text.RegularExpressions.Regex badCharReplace = new System.Text.RegularExpressions.Regex(@"^([<>%;()&])$");
                result = badCharReplace.Replace(result, "");

                result = System.Text.RegularExpressions.Regex.Replace(result, @"\bor\b|\band\b", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                return result.Trim();
            }
            else
            {
                return input;
            }
        }
        #endregion

        #region 截取字符串
        /// <summary>
        /// 避免字符串过长，超过指定位数后面的用"..."代替
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="length">最大字符数</param>
        /// <param name="AddPoints">是否在截取后的字符串末加上省略号</param>
        /// <returns>截取后的字符串</returns>
        public static string CutString(string input, int length, bool AddPoints)
        {
            if (input == null || input.Length <= length || length < 0)
            {
                return input;
            }
            else
            {
                return String.Format("{0}{1}", input.Substring(0, length), AddPoints ? "..." : String.Empty);
            }
        }
        #endregion

        #region 获取数字长度
        /// <summary>
        /// 获取数字长度
        /// </summary>
        /// <param name="input">输入数字</param>        
        public static int GetLength(Int64 input)
        {
            return input.ToString().Length;
        }

        /// <summary>
        /// 获取数字长度
        /// </summary>
        /// <param name="input">输入数字</param>        
        public static int GetLength(int input)
        {
            return input.ToString().Length;
        }

        public static bool Exists(string[] strs, string pattern)
        {
            if (strs == null)
                return false;
            foreach (string str in strs)
            {
                if (!Regex.IsMatch(str, pattern))
                    return false;
            }
            return true;
        }
        #endregion
    }
}