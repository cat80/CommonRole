using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Common
{
    /// <summary>
    /// json helper类，主要对JavaScriptSerializer的简单封装
    /// </summary>
    public class JSON
    {
        /// <summary>
        /// 获取对象的JSON
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <returns></returns>
        public static string GetJson(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        /// <summary>
        /// 获取对象的JSON
        /// </summary>
        /// <param name="obj">序列化对象</param>
        /// <param name="converters">自定义转换器</param>
        /// <returns></returns>
        public static string GetJson(object obj, IEnumerable<JavaScriptConverter> converters)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(converters);
            return jss.Serialize(obj);
        }

        /// <summary>
        /// 从json中反序列化对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">json</param>
        /// <returns></returns>
        public static T GetObject<T>(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        /// <summary>
        /// 从json中反序列化对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="json">json</param>
        /// <param name="converters">转换器</param>
        /// <returns></returns>
        public static T GetObject<T>(string json, IEnumerable<JavaScriptConverter> converters)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.RegisterConverters(converters);
            return jss.Deserialize<T>(json);
        }
    }
}
