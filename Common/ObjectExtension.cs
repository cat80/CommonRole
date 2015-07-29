using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
namespace Common
{
    public static class ObjectExtension
    {
        public static int TryParseInt(this object obj)
        {
            return TryParseInt(obj, 0);
        }
        public static int TryParseInt(this object obj,int defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString().TryParseInt(defaultValue);
        }
        public static string ToJsonString(this object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            return js.Serialize(obj);
        }
        public static DateTime TryParseDateTime(this object obj)
        {
            return TryParseDateTime(obj, DateTime.MinValue);
        }
        public static DateTime TryParseDateTime(this object obj, DateTime defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString().TryParseDateTime(defaultValue);
        }

        public static string TryParseString(this object obj)
        {
            return TryParseString(obj, "");
        }
        public static string TryParseString(this object obj, string defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }

        public static decimal TryParseDecimal(this object obj)
        {
            return TryParseDecimal(obj, 0, 2);
        }
        public static decimal TryParseDecimal(this object obj,int decimals)
        {
            return TryParseDecimal(obj, 0, decimals);
        }
        public static decimal TryParseDecimal(this object obj, decimal defaultValue, int decimals)
        {
            return obj == null ? defaultValue : obj.ToString().TryParseDecimal(defaultValue, decimals);
        }
        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null)
                return true;
            return string.IsNullOrEmpty(obj.ToString());
        }
        public static List<T> GetEntityListByDT<T>(this object srcDT, Hashtable relation)
        {
            return EntityHelper.GetEntityListByDT<T>((DataTable)srcDT, relation);
        }
        public static T GetEntityByDT<T>(this object srcDT, Hashtable relation)
        {
            return EntityHelper.GetEntityByDT<T>((DataTable)srcDT, relation);
        }
        public static T GetEntityByDT<T>(this object srcDT)
        {
            return EntityHelper.GetEntityByDT<T>((DataTable)srcDT, null);
        }

    }
}
