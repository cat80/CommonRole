using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace Common
{
    /// <summary>
    /// 高级模板样式
    /// </summary>
    public class AdvancedTemplate
    {
        /// <summary>
        /// css列表容器
        /// </summary>
        private static Dictionary<int, string> cssCodeDic = new Dictionary<int, string>();
        static AdvancedTemplate()
        {
            try
            {

                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/advtempcss.xml");
                if (!File.Exists(filePath))
                {
                    Log.Default.Debug("初始华css高级模版失败，失败原因:" + filePath + "文件不存在！");
                    return;
                }
                XDocument doc = XDocument.Load(filePath);

                var eleList = doc.Document.Element("root").Elements("advtemp");

                foreach (var item in eleList)
                {
                    int tempid, advtempid;
                    string[] tempArr = item.Element("tempid").Value.Split(',');
                    string[] advTempArr = item.Element("advtempid").Value.Split(',');
                    string cssCode = item.Element("csscode").Value;
                    foreach (var tempidStr in tempArr)
                    {
                        foreach (var advtempidStr in advTempArr)
                        {
                            int.TryParse(tempidStr, out tempid);
                            int.TryParse(advtempidStr, out advtempid);
                            int tempIndex = tempid << 16 | advtempid;
                            if (!cssCodeDic.ContainsKey(tempIndex) && tempIndex > 0)
                            {
                                cssCodeDic[tempIndex] = cssCode;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Default.Error("初始华css高级模版失败，失败原因:" + ex);
            }

        }

        public static string GetCssCode(int tempid, int advtempid)
        {
            string cssCode = string.Empty;
            int tempIndex = tempid << 16 | advtempid;
            if (cssCodeDic.ContainsKey(tempIndex))
            {
                return cssCodeDic[tempIndex];
            }
            return string.Empty;
        }
    }
}
