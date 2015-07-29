#region 文件描述

// 描述：DataRow Json序列化器
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

namespace Common
{
    /// <summary>
    ///DataRowConvert 的摘要说明
    /// </summary>
    public class DataRowConverter : JavaScriptConverter
    {
        public DataRowConverter()
        {
            
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataRow row = obj as DataRow;

            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (DataColumn col in row.Table.Columns)
            {
                object value = row[col.ColumnName];
                if (row[col.ColumnName].GetType() == typeof(DateTime))
                {
                    value = Convert.ToDateTime(row[col.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss");
                }
                result.Add(col.ColumnName.ToLower(), value);
            }
            return result;
            //  throw new NotImplementedException();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get { yield return typeof(DataRow); }
        }
    }
}
