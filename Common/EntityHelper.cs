using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace Common
{
    /// <summary>
    /// 数据实体转换类
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// 根据数据表生成相应的实体对象列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="srcDT">数据</param>
        /// <param name="relation">数据库表列名与对象属性名对应关系；如果列名与实体对象属性名相同，该参数可为空</param>
        /// <returns>对象列表</returns>
        public static List<T> GetEntityListByDT<T>(DataTable srcDT, Hashtable relation)
        {
            List<T> list = new List<T>();
            T destObj = default(T);

            if (srcDT != null && srcDT.Rows.Count > 0)
            {
                foreach (DataRow row in srcDT.Rows)
                {
                    destObj = GetEntityByDR<T>(row, relation);
                    list.Add(destObj);
                }
            }

            return list;
        }
        public static IList<T> GetEntityListByDT<T>(this DataTable srcDT)
        {
            List<T> list = null;
            T destObj = default(T);

            if (srcDT != null && srcDT.Rows.Count > 0)
            {

                list = new List<T>();
                foreach (DataRow row in srcDT.Rows)
                {
                    destObj = GetEntityByDR<T>(row, null);
                    list.Add(destObj);
                }
            }
            return list;
        }
        /// <summary>
        /// 根据DataRow生成相应的实体对象列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="srcDT">数据</param>
        /// <param name="relation">数据库表列名与对象属性名对应关系；如果列名与实体对象属性名相同，该参数可为空</param>
        /// <returns>对象列表</returns>
        public static List<T> GetEntityListByDR<T>(DataRow[] drs, Hashtable relation)
        {
            List<T> list = null;
            T destObj = default(T);

            if (drs != null && drs.Length > 0)
            {

                list = new List<T>();
                foreach (DataRow row in drs)
                {
                    destObj = GetEntityByDR<T>(row, relation);
                    list.Add(destObj);
                }
            }

            return list;
        }

        /// <summary>
        ///  将SqlDataReader转换成数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static T GetEntityByDataReader<T>(SqlDataReader reader, Hashtable relation)
        {
            Type type = typeof(T);
            T destObj = default(T);
            if (reader.HasRows)
            {
                destObj = Activator.CreateInstance<T>();
            }
            PropertyInfo temp = null;
            foreach (PropertyInfo prop in type.GetProperties())
            {
                try
                {
                    if (reader[prop.Name] != null && reader[prop.Name] != DBNull.Value)
                    {
                        SetPropertyValue(prop, destObj, reader[prop.Name]);
                    }
                }
                catch { }
            }

            if (relation != null)
            {

                foreach (string name in relation.Keys)
                {
                    temp = type.GetProperty(relation[name].ToString());
                    if (temp != null &&
                        reader[name] != DBNull.Value)
                    {
                        SetPropertyValue(temp, destObj, reader[name]);
                    }
                }
            }
            return destObj;
        }

        /// <summary>
        ///  将SqlDataReader转换成数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static T GetEntityByDataReader<T>(MySqlDataReader reader, Hashtable relation)
        {
            Type type = typeof(T);
            T destObj = default(T);
            if (reader.HasRows)
            {
                destObj = Activator.CreateInstance<T>();
            }
            PropertyInfo temp = null;
            foreach (PropertyInfo prop in type.GetProperties())
            {
                try
                {
                    if (reader[prop.Name] != null && reader[prop.Name] != DBNull.Value)
                    {
                        SetPropertyValue(prop, destObj, reader[prop.Name]);
                    }
                }
                catch { }
            }

            if (relation != null)
            {

                foreach (string name in relation.Keys)
                {
                    temp = type.GetProperty(relation[name].ToString());
                    if (temp != null &&
                        reader[name] != DBNull.Value)
                    {
                        SetPropertyValue(temp, destObj, reader[name]);
                    }
                }
            }
            return destObj;
        }

        /// <summary>
        /// 根据数据表生成相应的实体对象列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="table">数据</param>
        /// <param name="relation">数据库表列名与对象属性名对应关系；如果列名与实体对象属性名相同，该参数可为空</param>
        /// <returns>对象列表</returns>
        public static List<T> GetEntityListByDataReader<T>(SqlDataReader reader, Hashtable relation)
        {
            List<T> list = null;
            T destObj = default(T);
            if (reader.HasRows)
            {
                list = new List<T>();
            }
            while (reader.Read())
            {
                destObj = GetEntityByDataReader<T>(reader, relation);
                list.Add(destObj);
            }

            return list;
        }
        public static T GetEntityByDT<T>(DataTable dt, Hashtable relation)
        {
            if (dt != null && dt.Rows.Count > 0)
                return GetEntityByDR<T>(dt.Rows[0], relation);
            return default(T);
        }
        /// <summary>
        ///  将数据行转换成数据实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static T GetEntityByDR<T>(DataRow row, Hashtable relation)
        {
            Type type = typeof(T);
            T destObj = Activator.CreateInstance<T>();
            PropertyInfo temp = null;
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (row.Table.Columns.Contains(prop.Name) &&
                    row[prop.Name] != DBNull.Value)
                {
                    SetPropertyValue(prop, destObj, row[prop.Name]);
                }
            }

            if (relation != null)
            {

                foreach (string name in relation.Keys)
                {
                    temp = type.GetProperty(relation[name].ToString());
                    if (temp != null &&
                        row[name] != DBNull.Value)
                    {
                        SetPropertyValue(temp, destObj, row[name]);
                    }
                }
            }

            return destObj;
        }

        /// <summary>
        /// 为对象的属性赋值
        /// </summary>
        /// <param name="prop">属性</param>
        /// <param name="destObj">目标对象</param>
        /// <param name="value">源值</param>
        private static void SetPropertyValue(PropertyInfo prop, object destObj, object value)
        {
            //object temp = ChangeType(prop.PropertyType, value);
            //var accessor = FastReflectionCaches.PropertyAccessorCache.Get(prop);
            //accessor.SetValue(destObj, temp);

            object temp = ChangeType(prop.PropertyType, value);
            prop.SetValue(destObj, temp, null);
        }

        /// <summary>
        /// 用于类型数据的赋值
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="value">原值</param>
        /// <returns></returns>
        private static object ChangeType(Type type, object value)
        {
            int temp = 0;
            if ((value == null) && type.IsGenericType)
            {
                return Activator.CreateInstance(type);
            }
            if (value == null)
            {
                return null;
            }
            if (type == value.GetType())
            {
                return value;
            }
            if (type.IsEnum)
            {
                if (value is string)
                {
                    return Enum.Parse(type, value as string);
                }
                return Enum.ToObject(type, value);
            }

            if (type == typeof(bool) && typeof(int).IsInstanceOfType(value))
            {
                temp = int.Parse(value.ToString());
                return temp != 0;
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type type1 = type.GetGenericArguments()[0];
                object obj1 = ChangeType(type1, value);
                return Activator.CreateInstance(type, new object[] { obj1 });
            }
            if ((value is string) && (type == typeof(Guid)))
            {
                return new Guid(value as string);
            }
            if ((value is string) && (type == typeof(Version)))
            {
                return new Version(value as string);
            }
            if (!(value is IConvertible))
            {
                return value;
            }
            return Convert.ChangeType(value, type);
        }
    }
}
