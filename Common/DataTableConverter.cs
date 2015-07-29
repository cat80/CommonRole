#region 文件描述

// 描述：DataTable Json序列化器
// 作者：cat80
// 时间：2012-06-13 11:40

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
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;

namespace Common
{
    /// <summary>
    ///DataRowConvert 的摘要说明
    /// </summary>
    public class DataTableConverter : JavaScriptConverter
    {
        public DataTableConverter()
        {

    
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null || type != typeof(DataTable))
            {
                return null;
            }
            string tableName = string.Empty;
            ICollection<string> coll = dictionary.Keys as ICollection<string>;
            foreach (string item in coll)
            {
                tableName = item;
            }
            ArrayList array = dictionary[tableName] as ArrayList;
            if (array == null || array.Count == 0)
            {
                return null;
            }

            DataTable table = new DataTable();
            table.TableName = tableName;
            foreach (var item in array[0] as IDictionary<string, object>)
            {
                table.Columns.Add(item.Key.ToString());
            }

            foreach (IDictionary<string, object> rows in array)
            {
                DataRow row = table.NewRow();
                foreach (var item in rows)
                {
                    row[item.Key.ToString()] = item.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataTable table = obj as DataTable;

            Dictionary<string, object> result = new Dictionary<string, object>();

            List<object> list = new List<object>();
            foreach (DataRow item in table.Rows)
            {
                list.Add(item);
            }
            result["Rows"] = list;
            return result;
            //  throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(DataTable); }
        }
    }
}
