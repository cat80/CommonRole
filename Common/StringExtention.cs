using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Common
{
    public static class StringExtention
    {
        /// <summary>
        /// 将字符串转换成任意类型的数据,转换失败则返回默认值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="str">数据源</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T TryParse<T>(this string str, T defaultValue)
        {
            object obj;
            if (typeof(T).Name.ToLower() == "int32")
            {
                int v = 0;
                if (!int.TryParse(str, out v))
                    return defaultValue;
                obj = v;
            }
            else if (typeof(T).Name.ToLower() == "datetime")
            {
                DateTime v = DateTime.Now;
                if (!DateTime.TryParse(str, out v))
                    return defaultValue;
                obj = v;
            }
            else if (typeof(T).Name.ToLower() == "decimal")
            {
                decimal v = 0;
                if (!decimal.TryParse(str, out v))
                    return defaultValue;
                obj = v;
            }
            else if (typeof(T).Name.ToLower() == "char")
            {
                char v = char.MinValue;
                if (!char.TryParse(str, out v))
                    return defaultValue;
                obj = v;
            }
            else if (typeof(T).Name.ToLower() == "boolean")
            {
                bool v = true;
                if (!bool.TryParse(str, out v))
                    return defaultValue;
                obj = v;
            }
            else
            {
                obj = defaultValue;
            }
            return (T)obj;
        }

        /// <summary>
        /// 尝试将字符串转换为int,转换失败则返回默认值
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static int TryParseInt(this string str, int defaultvalue)
        {
            if (string.IsNullOrEmpty(str))
                return defaultvalue;
            return TryParse(str, defaultvalue);
        }

        /// <summary>
        /// 尝试将字符串转换为int,转换失败则返回0
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns></returns>
        public static int TryParseInt(this string str)
        {
            return TryParseInt(str, 0);
        }

        /// <summary>
        /// 尝试将字符串转换为decimal,转换失败则返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <param name="decimals">舍入指定的小数位数</param>
        /// <returns></returns>
        public static decimal TryParseDecimal(this string str, decimal defaultvalue, int decimals)
        {
            if (string.IsNullOrEmpty(str))
                return defaultvalue;
            return decimal.Round(TryParse<decimal>(str, defaultvalue), decimals);
        }
        /// <summary>
        /// 尝试将字符串转换为decimal,转换失败则返回0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal TryParseDecimal(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;
            return TryParseDecimal(str, 0, 0);
        }


        /// <summary>
        /// 尝试将字符串转换为DateTime,转换失败则返回默认值
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static DateTime TryParseDateTime(this string str, DateTime defaultvalue)
        {
            if (string.IsNullOrEmpty(str))
                return defaultvalue;
            return TryParse<DateTime>(str, defaultvalue);
        }
        /// <summary>
        /// 尝试将字符串转换为DateTime,转换失败则返回默认值
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static DateTime TryParseDateTime(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return DateTime.MinValue;
            return TryParse<DateTime>(str, DateTime.MinValue);
        }

        public static char TryParseChar(this string str, char defaultvalue)
        {
            if (string.IsNullOrEmpty(str))
                return defaultvalue;
            return TryParse<char>(str, defaultvalue);
        }
        public static char TryParseChar(this string str)
        {
            return TryParseChar(str, ',');
        }
        /// <summary>
        /// 尝试将字符串转换为bool,转换失败则返回默认值
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static bool TryParseBool(this string str, bool defaultvalue)
        {
            if (string.IsNullOrEmpty(str))
                return defaultvalue;
            return TryParse(str, defaultvalue);
        }
        /// <summary>
        /// 尝试将字符串转换为bool,转换失败则返回默认值
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <returns></returns>
        public static bool TryParseBool(this string str)
        {
            return TryParseBool(str, true);
        }

        public static string[] Split(this string str, string sp)
        {
            if (string.IsNullOrEmpty(str))
                return new string[] { };
            return str.Split(sp.TryParseChar());
        }
        public static bool CanSplit(this string str, string sp)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            string[] strs = str.Split(sp);
            return (strs != null && str.Length > 0);
        }
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrEmpty(this string str, out string output)
        {
            output = "";
            if (!IsNullOrEmpty(str))
            {
                output = str;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 取得传入的汉字的首个拼音字母
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string GetChineseFirstSpell(this string str)
        {
            char[] chars = str.ToCharArray();
            string result = "";
            if (chars.Length > 1)
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    result += getSpell(chars[i].ToString());
                }
            }
            else if (chars.Length == 1)
            {
                result = getSpell(str);
            }
            return result.ToLower();
        }

        /// <summary>
        /// 是否中文字符
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        public static bool WordsIScn(this string words)
        {
            string TmmP;

            for (int i = 0; i < words.Length; i++)
            {
                TmmP = words.Substring(i, 1);

                byte[] sarr = System.Text.Encoding.GetEncoding("gb2312").GetBytes(TmmP);

                if (sarr.Length == 2)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsMobile(this string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1][3-5]\d{9}$");
        } 
        private static string getSpell(string cn)
        {
            #region
            byte[] arrCN = Encoding.Default.GetBytes(cn);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return cn;
            }
            else return cn;
            #endregion
        }
        public static string WipeHtml(this string str)
        {
            return DataTypeHelper.WipeHtml(str);
        }
        public static string WipeStyle(this string str)
        {
            return DataTypeHelper.WipeStyle(str);
        }

        /// <summary>
        /// 过滤字符串
        /// </summary>
        /// <param name="ParamerName">字符参数</param>
        /// <returns></returns>
        public static string GetFilterStr(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return DataTypeHelper.StringFiltrate(HttpContext.Current.Server.HtmlDecode(HttpContext.Current.Server.UrlDecode(str)));
            }
            return "";
        }
    }
}