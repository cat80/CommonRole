using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Types
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 把枚举类型转变成key：value的json
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ToJson(Type type)
        {
            return ToJson(type, true);
        }

        public static string ToSelectOptions(Type type)
        {
            return ToSelectOptions(type, null);
        }


        public static string ToSelectOptions(Type type, string selectedvalue)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in GetTypeDictionary(type, true))
            {
                if (item.Value == selectedvalue)
                {
                    sb.AppendFormat("<option selected=\"selected\" value=\"{0}\">{1}</option>", item.Value, item.Key);
                }
                else
                {
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", item.Value, item.Key);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取枚举字典
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="iskeyvalue">ture,以key:value方式返回，false则以value:key方式返回</param>
        /// <returns></returns>
        private static Dictionary<string, string> GetTypeDictionary(Type type, bool iskeyvalue)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (type == null ||
         !type.IsEnum)
            {
                return dic;
                // throw new ArgumentException("参数必须为枚举类型！");
            }
            Array arry = Enum.GetValues(type);

            string json = string.Empty;
            //string formatText = iskeyvalue ? @"""{0}"":""{1}""  ," : @"""{1}"":""{0}""  ,";
            foreach (var item in arry)
            {
                string key = Enum.GetName(type, item);
                string val = Convert.ToInt32(item).ToString();
                if (iskeyvalue)
                {
                    dic.Add(key, val);
                }
                else
                {
                    dic.Add(val, key);
                }
                //json += string.Format(formatText, Enum.GetName(type, item), Convert.ToInt32(item));

            }
            return dic;
        }
        /// <summary>
        ///     把枚举类型转变成JSON
        /// </summary>
        /// <param name="type">要转换的类型</param>
        /// <param name="iskeyvalue">ture,以key:value方式返回，false则以value:key方式返回</param>
        /// <returns></returns>
        public static string ToJson(Type type, bool iskeyvalue)
        {
            return JSON.GetJson(GetTypeDictionary(type, iskeyvalue));
        }
    }
}
