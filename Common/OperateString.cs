using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Common
{
    public class OperateString
    {
        #region 属性构造
        private static OperateString _Current = new OperateString();

        public static OperateString Current
        {
            get { return _Current; }
        }
        #endregion

        #region 删除标签
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public string DeleteTag(string strHtml)
        {
            if (String.IsNullOrEmpty(strHtml))
            {
                return strHtml;
            }
            else
            {
                string[] aryReg = { @"<script[^>]*？>.*？</script>", @"<！——。*\n(——>)？", @"<(\/\s*)？(。|\n)*？(\/\s*)？>", @"<(\w|\s|""|'| |=|\\|\.|\/|#)*", @"([\r\n|\s])*", @"&(quot|#34);", @"&(amp|#38);", @"&(lt|#60);", @"&(gt|#62);", @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);", @"&(copy|#169);", @"&#(\d+);" };

                string newReg = aryReg[0]; string strOutput = strHtml.Replace("&nbsp;", " ");
                for (int i = 0; i < aryReg.Length; i++)
                {
                    Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase); strOutput = regex.Replace(strOutput, "");
                }
                //strOutput.Replace("<", "&gt;");
                //strOutput.Replace(">", "&lt;");
                //return strOutput.Replace(" ", "&nbsp;");    
                strOutput = strOutput.Replace("<", " ");
                strOutput = strOutput.Replace(">", " ");
                return strOutput = strOutput.Replace(" ", "").Trim();
            }
        }
        #endregion
    }
}
